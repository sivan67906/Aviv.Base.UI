// Helper functions for proper disposal of JS references
window.interop.disposeDotNetReference = function (dotNetReference) {
    try {
        if (dotNetReference) {
            dotNetReference.dispose();
        }
    } catch (error) {
        console.error("Error disposing .NET reference:", error);
    }
};

// Helper to remove event listeners
window.interop.removeEventListener = function (element, eventName, handler) {
    try {
        if (element && typeof element.removeEventListener === 'function') {
            element.removeEventListener(eventName, handler);
        }
    } catch (error) {
        console.error("Error removing event listener:", error);
    }
};

// Debug helper for menu toggling
window.interop.debugMenu = function (menuItem) {
    console.log("Menu item clicked:", menuItem);
    console.log("Active state:", menuItem.active);
    console.log("Has children:", menuItem.children && menuItem.children.length > 0);

    // Check DOM state
    var menuElements = document.querySelectorAll('.slide-menu');
    console.log("Menu elements:", menuElements.length);
    menuElements.forEach(function (el, index) {
        console.log(`Menu ${index} display:`, window.getComputedStyle(el).display);
    });

    return true;
};