// Add this as a new file in your project: wwwroot/js/gridDragDrop.js
// This external JavaScript file will be loaded once and persist throughout the application

window.GridDragDrop = {
    initialized: false,
    dotNetReference: null,

    initialize: function (dotNetRef) {
        console.log("GridDragDrop: Initializing");
        this.dotNetReference = dotNetRef;
        this.initialized = true;

        // Register for Radzen's grid data loaded event
        document.addEventListener('radzen-grid-data-loaded', this.handleGridDataLoaded.bind(this));

        // Initial setup
        this.setupHandlers();

        return true;
    },

    handleGridDataLoaded: function (event) {
        // This event is fired when Radzen grid data loads
        console.log("GridDragDrop: Grid data loaded event detected");
        setTimeout(() => this.setupHandlers(), 200);
    },

    setupHandlers: function () {
        if (!this.initialized || !this.dotNetReference) {
            console.log("GridDragDrop: Not initialized, skipping handler setup");
            return;
        }

        console.log("GridDragDrop: Setting up handlers");
        const rows = document.querySelectorAll('.draggable-grid .rz-grid-table tbody tr');
        console.log(`GridDragDrop: Found ${rows.length} rows to make draggable`);

        rows.forEach((row, index) => {
            // Skip empty message rows
            if (row.classList.contains('rz-datatable-empty-message')) return;

            // Skip already initialized rows
            if (row.getAttribute('data-grid-drag-initialized') === 'true') return;

            // Mark row as initialized
            row.setAttribute('data-grid-drag-initialized', 'true');
            row.setAttribute('data-row-index', index);
            row.setAttribute('draggable', 'true');

            // Clear any existing handlers first (to prevent duplicates)
            this.clearRowHandlers(row);

            // Add drag start handler
            row.addEventListener('dragstart', this.handleDragStart);

            // Add drag end handler
            row.addEventListener('dragend', this.handleDragEnd);

            // Add drag over handler
            row.addEventListener('dragover', this.handleDragOver);

            // Add drag leave handler
            row.addEventListener('dragleave', this.handleDragLeave);

            // Add drop handler with reference to this object
            row.addEventListener('drop', (e) => this.handleDrop(e, this.dotNetReference));

            console.log(`GridDragDrop: Initialized row ${index}`);
        });
    },

    clearRowHandlers: function (row) {
        // Remove existing handlers to prevent duplicates
        row.removeEventListener('dragstart', this.handleDragStart);
        row.removeEventListener('dragend', this.handleDragEnd);
        row.removeEventListener('dragover', this.handleDragOver);
        row.removeEventListener('dragleave', this.handleDragLeave);
        // Can't easily remove drop handler as it has a closure, but it will be replaced
    },

    handleDragStart: function (e) {
        const row = e.currentTarget;
        const index = row.getAttribute('data-row-index');

        // Store the row index in the drag data
        e.dataTransfer.setData('text/plain', index);

        // Add a class for styling
        row.classList.add('dragging');

        // For better visual feedback
        setTimeout(() => {
            row.style.opacity = '0.4';
        }, 0);

        console.log(`GridDragDrop: Started dragging row ${index}`);
    },

    handleDragEnd: function (e) {
        const row = e.currentTarget;

        // Remove the dragging class
        row.classList.remove('dragging');
        row.style.opacity = '1';

        // Clean up any drop target indicators
        document.querySelectorAll('.drop-target-above, .drop-target-below').forEach(el => {
            el.classList.remove('drop-target-above', 'drop-target-below');
        });

        console.log("GridDragDrop: Ended dragging");
    },

    handleDragOver: function (e) {
        // Prevent default to allow drop
        e.preventDefault();

        const row = e.currentTarget;
        const draggingRow = document.querySelector('.dragging');

        // Don't allow dropping onto itself
        if (draggingRow === row) return;

        // Remove existing indicators
        document.querySelectorAll('.drop-target-above, .drop-target-below').forEach(el => {
            el.classList.remove('drop-target-above', 'drop-target-below');
        });

        // Add indicator based on cursor position
        const rect = row.getBoundingClientRect();
        const middle = rect.top + rect.height / 2;

        if (e.clientY < middle) {
            row.classList.add('drop-target-above');
        } else {
            row.classList.add('drop-target-below');
        }
    },

    handleDragLeave: function (e) {
        const row = e.currentTarget;
        row.classList.remove('drop-target-above', 'drop-target-below');
    },

    handleDrop: function (e, dotNetRef) {
        // Prevent default action
        e.preventDefault();

        const row = e.currentTarget;
        const draggingRow = document.querySelector('.dragging');

        // Don't allow dropping onto itself
        if (draggingRow === row) return;

        // Get indices
        const fromIndex = parseInt(e.dataTransfer.getData('text/plain'));
        const toIndex = parseInt(row.getAttribute('data-row-index'));

        // Skip if indices are invalid
        if (isNaN(fromIndex) || isNaN(toIndex)) {
            console.error("GridDragDrop: Invalid indices for reordering", fromIndex, toIndex);
            return;
        }

        // Determine drop position
        const rect = row.getBoundingClientRect();
        const middle = rect.top + rect.height / 2;
        const dropPosition = e.clientY < middle ? 'above' : 'below';

        // Calculate final index
        const finalIndex = dropPosition === 'above' ? toIndex : toIndex + 1;

        console.log(`GridDragDrop: Dropping row ${fromIndex} ${dropPosition} row ${toIndex} (final index: ${finalIndex})`);

        // Clean up styling
        draggingRow.classList.remove('dragging');
        draggingRow.style.opacity = '1';

        document.querySelectorAll('.drop-target-above, .drop-target-below').forEach(el => {
            el.classList.remove('drop-target-above', 'drop-target-below');
        });

        // Call .NET method
        if (dotNetRef) {
            console.log("GridDragDrop: Invoking .NET method ReorderRows");
            dotNetRef.invokeMethodAsync('ReorderRows', fromIndex, finalIndex)
                .then(() => {
                    console.log("GridDragDrop: Successfully reordered rows");
                })
                .catch(error => {
                    console.error("GridDragDrop: Error invoking ReorderRows", error);
                });
        } else {
            console.error("GridDragDrop: No .NET reference available for invocation");
        }
    },

    dispose: function () {
        // Clean up event listeners
        document.removeEventListener('radzen-grid-data-loaded', this.handleGridDataLoaded);
        this.dotNetReference = null;
        this.initialized = false;
        console.log("GridDragDrop: Disposed");
    }
};