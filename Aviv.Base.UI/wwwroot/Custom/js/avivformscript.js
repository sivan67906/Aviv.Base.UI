/**
 * Form Validation and Styling Module
 * Handles form validation, focus states, and error styling for various form controls
 */
var FormValidation = (function () {
    // Default configuration
    var defaultConfig = {
        focusColor: '#8F69E1',       // Color for focused elements
        normalColor: '#ced4da',      // Normal border color
        errorColor: '#dc3545',       // Error border color
        formSelector: 'form',        // Form selector
        requiredSelector: '[data-required="true"], [required]' // Selector for required fields
    };

    // Current configuration (will be merged with defaults)
    var config = {};

    /**
     * Initialize the form validation module
     * @param {Object} customConfig - Custom configuration to override defaults
     */
    function init(customConfig) {
        // Merge default config with custom config
        config = Object.assign({}, defaultConfig, customConfig || {});

        // Set up event listeners
        setupEventHandlers();
    }

    /**
     * Set up focus/blur handlers for form elements
     */
    function setupEventHandlers() {
        document.addEventListener('DOMContentLoaded', function () {
            // Handle standard form controls
            setupStandardInputHandlers();

            // Handle Syncfusion dropdowns
            setupSyncfusionDropdownHandlers();

            // Handle checkbox and radio buttons
            setupCheckboxRadioHandlers();

            // Handle MultiSelect controls
            setupMultiSelectHandlers();
        });
    }

    /**
     * Set up handlers for standard HTML input elements
     */
    function setupStandardInputHandlers() {
        // Get all standard input elements, textareas, and selects
        var inputs = document.querySelectorAll('input:not([type="checkbox"]):not([type="radio"]):not([type="hidden"]):not([disabled]):not([readonly]), textarea:not([disabled]), select:not([disabled])');

        inputs.forEach(function (input) {
            // Focus event
            input.addEventListener('focus', function () {
                this.style.borderColor = config.focusColor;
                // Don't remove error class on focus if form is submitted - preserve error state
                var form = this.closest('form');
                if (!form || !form.classList.contains('form-submitted')) {
                    this.classList.remove('error-border');
                }
            });

            // Blur event
            input.addEventListener('blur', function () {
                // Only reset border color if not in error state
                if (!this.classList.contains('error-border')) {
                    this.style.borderColor = config.normalColor;
                }

                // Check if form has been submitted
                var form = this.closest('form');
                var isFormSubmitted = form && form.classList.contains('form-submitted');

                // Validate field on blur if it's required and form has been submitted
                if (isFormSubmitted && this.hasAttribute('data-required')) {
                    var isEmpty = this.value.trim() === '';
                    if (isEmpty) {
                        this.classList.add('error-border');
                        this.style.borderColor = config.errorColor;
                        this.style.backgroundColor = '#fff8f8';

                        // Show error message if it exists
                        var errorTextElement = this.nextElementSibling;
                        if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                            errorTextElement.style.display = 'block';
                        }
                    } else {
                        this.classList.remove('error-border');
                        this.style.borderColor = config.normalColor;
                        this.style.backgroundColor = '';

                        // Hide error message if it exists
                        var errorTextElement = this.nextElementSibling;
                        if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                            errorTextElement.style.display = 'none';
                        }
                    }
                }
            });

            // Input event for real-time validation feedback
            input.addEventListener('input', function () {
                // If field was previously marked as error, check if it's now valid
                if (this.classList.contains('error-border')) {
                    var form = this.closest('form');
                    var isFormSubmitted = form && form.classList.contains('form-submitted');

                    if (isFormSubmitted && this.hasAttribute('data-required')) {
                        var isEmpty = this.value.trim() === '';
                        if (!isEmpty) {
                            this.classList.remove('error-border');
                            this.style.borderColor = config.focusColor; // Use focus color when typing
                            this.style.backgroundColor = '';

                            // Hide error message if it exists
                            var errorTextElement = this.nextElementSibling;
                            if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                                errorTextElement.style.display = 'none';
                            }
                        }
                    }
                }
            });
        });
    }

    /**
     * Set up handlers for Syncfusion dropdown elements
     */
    function setupSyncfusionDropdownHandlers() {
        var dropdowns = document.querySelectorAll('.e-ddl');

        dropdowns.forEach(function (dropdown) {
            var inputGroup = dropdown.querySelector('.e-input-group');
            var input = dropdown.querySelector('input');

            if (input) {
                // Focus event
                input.addEventListener('focus', function () {
                    if (inputGroup) {
                        inputGroup.style.borderColor = config.focusColor;

                        // Don't remove error class on focus if form is submitted - preserve error state
                        var form = dropdown.closest('form');
                        if (!form || !form.classList.contains('form-submitted')) {
                            dropdown.classList.remove('error-border');
                            inputGroup.classList.remove('error-border');
                        }
                    }
                    dropdown.classList.add('focused');
                });

                // Blur event
                input.addEventListener('blur', function () {
                    if (!dropdown.contains(document.activeElement)) {
                        if (inputGroup && !dropdown.classList.contains('error-border') && !inputGroup.classList.contains('error-border')) {
                            inputGroup.style.borderColor = config.normalColor;
                        }
                        dropdown.classList.remove('focused');

                        // Check if form has been submitted
                        var form = dropdown.closest('form');
                        var isFormSubmitted = form && form.classList.contains('form-submitted');

                        // Validate on blur if form has been submitted
                        if (isFormSubmitted && dropdown.hasAttribute('data-required')) {
                            var isEmpty = !this.value ||
                                this.value.trim() === '' ||
                                this.value === '00000000-0000-0000-0000-000000000000' ||
                                this.value === '--';

                            if (isEmpty) {
                                dropdown.classList.add('error-border');
                                if (inputGroup) {
                                    inputGroup.classList.add('error-border');
                                    inputGroup.style.borderColor = config.errorColor;
                                    inputGroup.style.backgroundColor = '#fff8f8';
                                }

                                // Show error message if it exists
                                var errorTextElement = dropdown.nextElementSibling;
                                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                                    errorTextElement.style.display = 'block';
                                }
                            } else {
                                dropdown.classList.remove('error-border');
                                if (inputGroup) {
                                    inputGroup.classList.remove('error-border');
                                    inputGroup.style.borderColor = config.normalColor;
                                    inputGroup.style.backgroundColor = '';
                                }

                                // Hide error message if it exists
                                var errorTextElement = dropdown.nextElementSibling;
                                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                                    errorTextElement.style.display = 'none';
                                }
                            }
                        }
                    }
                });

                // Change event for real-time validation
                input.addEventListener('change', function () {
                    // Check if form has been submitted
                    var form = dropdown.closest('form');
                    var isFormSubmitted = form && form.classList.contains('form-submitted');

                    // Validate on change if form has been submitted
                    if (isFormSubmitted && dropdown.hasAttribute('data-required')) {
                        var isEmpty = !this.value ||
                            this.value.trim() === '' ||
                            this.value === '00000000-0000-0000-0000-000000000000' ||
                            this.value === '--';

                        if (isEmpty) {
                            dropdown.classList.add('error-border');
                            if (inputGroup) {
                                inputGroup.classList.add('error-border');
                                inputGroup.style.borderColor = config.errorColor;
                                inputGroup.style.backgroundColor = '#fff8f8';
                            }

                            // Show error message if it exists
                            var errorTextElement = dropdown.nextElementSibling;
                            if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                                errorTextElement.style.display = 'block';
                            }
                        } else {
                            dropdown.classList.remove('error-border');
                            if (inputGroup) {
                                inputGroup.classList.remove('error-border');
                                inputGroup.style.borderColor = config.normalColor;
                                inputGroup.style.backgroundColor = '';
                            }

                            // Hide error message if it exists
                            var errorTextElement = dropdown.nextElementSibling;
                            if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                                errorTextElement.style.display = 'none';
                            }
                        }
                    }
                });
            }

            // Click event on dropdown
            dropdown.addEventListener('click', function () {
                if (inputGroup) {
                    // Only apply focus color if not in error state
                    if (!inputGroup.classList.contains('error-border')) {
                        inputGroup.style.borderColor = config.focusColor;
                    }
                    if (input) {
                        input.focus();
                    }
                }
                dropdown.classList.add('focused');
            });
        });
    }

    /**
     * Set up handlers for checkbox and radio button elements
     */
    function setupCheckboxRadioHandlers() {
        var checkboxes = document.querySelectorAll('input[type="checkbox"], input[type="radio"]');

        checkboxes.forEach(function (checkbox) {
            // Focus event
            checkbox.addEventListener('focus', function () {
                this.closest('label')?.classList.add('focused');
                var formCheck = this.closest('.form-check');
                if (formCheck) {
                    formCheck.classList.add('focused');

                    // Don't remove error class on focus if form is submitted - preserve error state
                    var form = this.closest('form');
                    if (!form || !form.classList.contains('form-submitted')) {
                        formCheck.classList.remove('error-border');
                    }
                }
            });

            // Blur event
            checkbox.addEventListener('blur', function () {
                this.closest('label')?.classList.remove('focused');
                var formCheck = this.closest('.form-check');
                if (formCheck) {
                    formCheck.classList.remove('focused');

                    // Check if form has been submitted
                    var form = this.closest('form');
                    var isFormSubmitted = form && form.classList.contains('form-submitted');

                    // Validate on blur if form has been submitted
                    if (isFormSubmitted && this.hasAttribute('data-required') && !this.checked) {
                        formCheck.classList.add('error-border');

                        // Show error message if it exists
                        var errorTextElement = formCheck.nextElementSibling;
                        if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                            errorTextElement.style.display = 'block';
                        }
                    }
                }
            });

            // Change event for real-time validation
            checkbox.addEventListener('change', function () {
                var formCheck = this.closest('.form-check');

                // Check if form has been submitted
                var form = this.closest('form');
                var isFormSubmitted = form && form.classList.contains('form-submitted');

                if (formCheck && isFormSubmitted && this.hasAttribute('data-required')) {
                    if (this.checked) {
                        formCheck.classList.remove('error-border');

                        // Hide error message if it exists
                        var errorTextElement = formCheck.nextElementSibling;
                        if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                            errorTextElement.style.display = 'none';
                        }
                    } else {
                        formCheck.classList.add('error-border');

                        // Show error message if it exists
                        var errorTextElement = formCheck.nextElementSibling;
                        if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                            errorTextElement.style.display = 'block';
                        }
                    }
                }
            });
        });
    }

    /**
     * Set up handlers for Syncfusion MultiSelect elements
     */
    function setupMultiSelectHandlers() {
        var multiSelects = document.querySelectorAll('.e-multi-select');

        multiSelects.forEach(function (multiSelect) {
            var inputWrapper = multiSelect.querySelector('.e-input-group');
            var input = multiSelect.querySelector('input');
            var hiddenInput = multiSelect.querySelector('input[type="hidden"]');

            if (input) {
                // Focus event
                input.addEventListener('focus', function () {
                    if (inputWrapper) {
                        inputWrapper.style.borderColor = config.focusColor;

                        // Don't remove error class on focus if form is submitted - preserve error state
                        var form = multiSelect.closest('form');
                        if (!form || !form.classList.contains('form-submitted')) {
                            multiSelect.classList.remove('error-border');
                            inputWrapper.classList.remove('error-border');
                        }
                    }
                    multiSelect.classList.add('focused');
                });

                // Blur event
                input.addEventListener('blur', function () {
                    if (!multiSelect.contains(document.activeElement)) {
                        if (inputWrapper && !multiSelect.classList.contains('error-border') && !inputWrapper.classList.contains('error-border')) {
                            inputWrapper.style.borderColor = config.normalColor;
                        }
                        multiSelect.classList.remove('focused');

                        // Check if form has been submitted
                        var form = multiSelect.closest('form');
                        var isFormSubmitted = form && form.classList.contains('form-submitted');

                        // Validate on blur if form has been submitted
                        if (isFormSubmitted && multiSelect.hasAttribute('data-required')) {
                            var isEmpty = !hiddenInput || !hiddenInput.value || hiddenInput.value.trim() === '';

                            if (isEmpty) {
                                multiSelect.classList.add('error-border');
                                if (inputWrapper) {
                                    inputWrapper.classList.add('error-border');
                                    inputWrapper.style.borderColor = config.errorColor;
                                    inputWrapper.style.backgroundColor = '#fff8f8';
                                }

                                // Show error message if it exists
                                var errorTextElement = multiSelect.nextElementSibling;
                                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                                    errorTextElement.style.display = 'block';
                                }
                            } else {
                                multiSelect.classList.remove('error-border');
                                if (inputWrapper) {
                                    inputWrapper.classList.remove('error-border');
                                    inputWrapper.style.borderColor = config.normalColor;
                                    inputWrapper.style.backgroundColor = '';
                                }

                                // Hide error message if it exists
                                var errorTextElement = multiSelect.nextElementSibling;
                                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                                    errorTextElement.style.display = 'none';
                                }
                            }
                        }
                    }
                });

                // Handle value changes
                if (hiddenInput) {
                    // Use MutationObserver to detect changes to the hidden input's value
                    var observer = new MutationObserver(function (mutations) {
                        mutations.forEach(function (mutation) {
                            if (mutation.type === 'attributes' && mutation.attributeName === 'value') {
                                // Check if form has been submitted
                                var form = multiSelect.closest('form');
                                var isFormSubmitted = form && form.classList.contains('form-submitted');

                                // Validate on change if form has been submitted
                                if (isFormSubmitted && multiSelect.hasAttribute('data-required')) {
                                    var isEmpty = !hiddenInput.value || hiddenInput.value.trim() === '';

                                    if (isEmpty) {
                                        multiSelect.classList.add('error-border');
                                        if (inputWrapper) {
                                            inputWrapper.classList.add('error-border');
                                            inputWrapper.style.borderColor = config.errorColor;
                                            inputWrapper.style.backgroundColor = '#fff8f8';
                                        }

                                        // Show error message if it exists
                                        var errorTextElement = multiSelect.nextElementSibling;
                                        if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                                            errorTextElement.style.display = 'block';
                                        }
                                    } else {
                                        multiSelect.classList.remove('error-border');
                                        if (inputWrapper) {
                                            inputWrapper.classList.remove('error-border');
                                            inputWrapper.style.borderColor = config.normalColor;
                                            inputWrapper.style.backgroundColor = '';
                                        }

                                        // Hide error message if it exists
                                        var errorTextElement = multiSelect.nextElementSibling;
                                        if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                                            errorTextElement.style.display = 'none';
                                        }
                                    }
                                }
                            }
                        });
                    });

                    observer.observe(hiddenInput, { attributes: true });
                }
            }
        });
    }

    /**
     * Validate form with specified required fields
     * @param {string} formId - ID of the form to validate
     * @param {Array} requiredFields - Array of field IDs that are required
     * @returns {boolean} - True if form is valid, false otherwise
     */
    function validateForm(formId, requiredFields) {
        var form = document.getElementById(formId);
        if (!form) return false;

        // Add form-submitted class to enable validation styling
        form.classList.add('form-submitted');

        // Get fields to validate
        var fieldsToValidate = requiredFields || [];
        var isValid = true;

        // Validate each specified field
        fieldsToValidate.forEach(function (fieldId) {
            if (!validateField(fieldId, true)) {
                isValid = false;
            }
        });

        // Ensure all error texts are visible when validation fails
        if (!isValid) {
            document.querySelectorAll('.error-text').forEach(function (errorText) {
                if (errorText.previousElementSibling &&
                    (errorText.previousElementSibling.classList.contains('error-border') ||
                        errorText.previousElementSibling.querySelector('.error-border'))) {
                    errorText.style.display = 'block';
                }
            });

            // Focus the first invalid field
            var firstInvalidField = form.querySelector('.error-border');
            if (firstInvalidField) {
                var inputElement = firstInvalidField.querySelector('input') || firstInvalidField;
                if (inputElement && typeof inputElement.focus === 'function') {
                    setTimeout(function () {
                        inputElement.focus();
                    }, 100);
                }
            }
        }

        return isValid;
    }

    /**
     * Validate a single field and update its styling
     * @param {string} fieldId - ID of the field to validate
     * @param {boolean} forceValidation - Whether to validate even if not in a submitted form
     * @returns {boolean} - True if field is valid, false otherwise
     */
    function validateField(fieldId, forceValidation) {
        var element = document.getElementById(fieldId);
        if (!element) return true; // If element doesn't exist, consider it valid

        var form = element.closest('form');
        var isFormSubmitted = form ? form.classList.contains('form-submitted') : false;

        // Only validate if form is submitted or forced validation
        if (!isFormSubmitted && !forceValidation) return true;

        var isValid = true;
        var value = '';
        var errorTextElement = null;

        // Handle different types of elements
        if (element.classList.contains('e-ddl') || element.closest('.e-ddl')) {
            // Syncfusion dropdown
            var dropdown = element.classList.contains('e-ddl') ? element : element.closest('.e-ddl');
            var dropdownInput = dropdown.querySelector('input');
            var inputGroup = dropdown.querySelector('.e-input-group');
            value = dropdownInput ? dropdownInput.value : '';
            errorTextElement = dropdown.nextElementSibling;

            if (!value || value.trim() === '' || value === '00000000-0000-0000-0000-000000000000' || value === '--') {
                dropdown.classList.add('error-border');
                if (inputGroup) {
                    inputGroup.classList.add('error-border');
                    inputGroup.style.borderColor = config.errorColor;
                    inputGroup.style.backgroundColor = '#fff8f8';
                }
                isValid = false;

                // Show error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'block';
                }
            } else {
                dropdown.classList.remove('error-border');
                if (inputGroup) {
                    inputGroup.classList.remove('error-border');
                    inputGroup.style.borderColor = config.normalColor;
                    inputGroup.style.backgroundColor = '';
                }

                // Hide error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'none';
                }
            }
        } else if (element.classList.contains('e-multi-select')) {
            // Syncfusion MultiSelect
            var multiSelect = element;
            var multiSelectInputGroup = multiSelect.querySelector('.e-input-group');
            var selectedValues = multiSelect.querySelector('input[type="hidden"]')?.value;
            errorTextElement = multiSelect.nextElementSibling;

            if (!selectedValues || selectedValues.trim() === '') {
                multiSelect.classList.add('error-border');
                if (multiSelectInputGroup) {
                    multiSelectInputGroup.classList.add('error-border');
                    multiSelectInputGroup.style.borderColor = config.errorColor;
                    multiSelectInputGroup.style.backgroundColor = '#fff8f8';
                }
                isValid = false;

                // Show error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'block';
                }
            } else {
                multiSelect.classList.remove('error-border');
                if (multiSelectInputGroup) {
                    multiSelectInputGroup.classList.remove('error-border');
                    multiSelectInputGroup.style.borderColor = config.normalColor;
                    multiSelectInputGroup.style.backgroundColor = '';
                }

                // Hide error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'none';
                }
            }
        } else if (element.type === 'checkbox' || element.type === 'radio') {
            // Checkbox or radio
            var isChecked = element.checked;
            var container = element.closest('.form-check') || element.parentNode;
            errorTextElement = container.nextElementSibling;

            if (!isChecked) {
                container.classList.add('error-border');
                isValid = false;

                // Show error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'block';
                }
            } else {
                container.classList.remove('error-border');

                // Hide error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'none';
                }
            }
        } else if (element.tagName === 'SELECT') {
            // Standard select element
            value = element.value;
            errorTextElement = element.nextElementSibling;

            if (!value || value.trim() === '' || value === '00000000-0000-0000-0000-000000000000' || value === '--') {
                element.classList.add('error-border');
                element.style.borderColor = config.errorColor;
                element.style.backgroundColor = '#fff8f8';
                isValid = false;

                // Show error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'block';
                }
            } else {
                element.classList.remove('error-border');
                element.style.borderColor = config.normalColor;
                element.style.backgroundColor = '';

                // Hide error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'none';
                }
            }
        } else {
            // Standard input, textarea
            value = element.value;
            errorTextElement = element.nextElementSibling;

            if (!value || value.trim() === '') {
                element.classList.add('error-border');
                element.style.borderColor = config.errorColor;
                element.style.backgroundColor = '#fff8f8';
                isValid = false;

                // Show error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'block';
                }
            } else {
                element.classList.remove('error-border');
                element.style.borderColor = config.normalColor;
                element.style.backgroundColor = '';

                // Hide error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'none';
                }
            }
        }

        return isValid;
    }

    /**
     * Apply validation styles to a single dropdown
     * @param {string} elementId - ID of the dropdown element to validate
     * @param {boolean} isError - True if the element is in error state
     */
    function applyDropdownValidation(elementId, isError) {
        var element = document.getElementById(elementId);
        if (!element) return;

        var dropdown = element.classList.contains('e-ddl') ? element : element.closest('.e-ddl');
        var inputGroup = dropdown?.querySelector('.e-input-group');
        var errorTextElement = dropdown?.nextElementSibling;

        if (dropdown) {
            if (isError) {
                dropdown.classList.add('error-border');

                // Show error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'block';
                }
            } else {
                dropdown.classList.remove('error-border');

                // Hide error message if it exists
                if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                    errorTextElement.style.display = 'none';
                }
            }
        }

        if (inputGroup) {
            if (isError) {
                inputGroup.classList.add('error-border');
                inputGroup.style.borderColor = config.errorColor;
                inputGroup.style.backgroundColor = '#fff8f8';
            } else {
                inputGroup.classList.remove('error-border');
                inputGroup.style.borderColor = config.normalColor;
                inputGroup.style.backgroundColor = '';
            }
        }
    }

    /**
     * Reset validation styles for a form
     * @param {string} formId - ID of the form to reset
     */
    function resetForm(formId) {
        var form = document.getElementById(formId);
        if (!form) return;

        // Remove the form-submitted class
        form.classList.remove('form-submitted');

        // Reset all input elements
        form.querySelectorAll('input, select, textarea').forEach(function (element) {
            element.classList.remove('error-border');
            element.style.borderColor = config.normalColor;
            element.style.backgroundColor = '';
        });

        // Reset Syncfusion dropdowns
        form.querySelectorAll('.e-ddl').forEach(function (dropdown) {
            dropdown.classList.remove('error-border');
            var inputGroup = dropdown.querySelector('.e-input-group');
            if (inputGroup) {
                inputGroup.classList.remove('error-border');
                inputGroup.style.borderColor = config.normalColor;
                inputGroup.style.backgroundColor = '';
            }

            // Hide error message if it exists
            var errorTextElement = dropdown.nextElementSibling;
            if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                errorTextElement.style.display = 'none';
            }
        });

        // Reset Syncfusion MultiSelect
        form.querySelectorAll('.e-multi-select').forEach(function (multiSelect) {
            multiSelect.classList.remove('error-border');
            var inputGroup = multiSelect.querySelector('.e-input-group');
            if (inputGroup) {
                inputGroup.classList.remove('error-border');
                inputGroup.style.borderColor = config.normalColor;
                inputGroup.style.backgroundColor = '';
            }

            // Hide error message if it exists
            var errorTextElement = multiSelect.nextElementSibling;
            if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                errorTextElement.style.display = 'none';
            }
        });

        // Reset form check containers
        form.querySelectorAll('.form-check').forEach(function (check) {
            check.classList.remove('error-border');

            // Hide error message if it exists
            var errorTextElement = check.nextElementSibling;
            if (errorTextElement && errorTextElement.classList.contains('error-text')) {
                errorTextElement.style.display = 'none';
            }
        });

        // Hide all error messages
        form.querySelectorAll('.error-text').forEach(function (errorText) {
            errorText.style.display = 'none';
        });
    }

    // Public API
    return {
        init: init,
        validateForm: validateForm,
        validateField: validateField,
        applyDropdownValidation: applyDropdownValidation,
        resetForm: resetForm
    };
})();

// Make FormValidation available globally
window.FormValidation = FormValidation;

// Global function that can be called directly from Blazor
window.validateForm = function (formId, requiredFields) {
    if (typeof FormValidation !== 'undefined') {
        return FormValidation.validateForm(formId, requiredFields);
    }
    return false;
};

// Add validate field function globally
window.validateField = function (fieldId, forceValidation) {
    if (typeof FormValidation !== 'undefined') {
        return FormValidation.validateField(fieldId, forceValidation);
    }
    return true;
};

// Add dropdown validation function globally
window.applyDropdownValidation = function (elementId, isError) {
    if (typeof FormValidation !== 'undefined') {
        FormValidation.applyDropdownValidation(elementId, isError);
    }
};