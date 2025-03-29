using Microsoft.JSInterop;

namespace Aviv.Base.UI.Services
{
    /// <summary>
    /// Service for displaying toast and alert notifications in Blazor components
    /// </summary>
    public class NotificationCustomService
    {
        private readonly IJSRuntime _jsRuntime;
        private bool _isInitialized = false;
        private bool _isInitializing = false;
        private readonly List<Func<Task>> _pendingNotifications = [];

        public NotificationCustomService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Check if the service can perform JavaScript interop
        /// </summary>
        private bool CanUseJsInterop => _jsRuntime is IJSInProcessRuntime || _isInitialized;

        /// <summary>
        /// Initialize the notification system JavaScript
        /// Designed to be called from OnAfterRenderAsync with firstRender=true
        /// </summary>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task InitializeAsync()
        {
            if (_isInitialized || _isInitializing)
                return;

            try
            {
                _isInitializing = true;
                await _jsRuntime.InvokeVoidAsync("eval", GetNotificationJsCode());
                _isInitialized = true;
                _isInitializing = false;

                // Process any pending notifications
                foreach (Func<Task> notification in _pendingNotifications)
                {
                    await notification();
                }
                _pendingNotifications.Clear();
            }
            catch (Exception)
            {
                _isInitializing = false;
                // Initialization failed, likely due to being called during prerendering
                // We'll try again later
            }
        }

        /// <summary>
        /// Queue a notification to be shown once the service is initialized
        /// </summary>
        private async Task QueueOrExecuteNotification(Func<Task> notificationAction)
        {
            if (!_isInitialized)
            {
                _pendingNotifications.Add(notificationAction);

                // Try to initialize if not already initializing
                if (!_isInitializing)
                {
                    try
                    {
                        await InitializeAsync();
                    }
                    catch
                    {
                        // Swallow exceptions during initialization attempt
                        // It will be tried again later in OnAfterRenderAsync
                    }
                }
            }
            else
            {
                await notificationAction();
            }
        }

        /// <summary>
        /// Show a line toast notification for loading, success, error, or info states
        /// </summary>
        /// <param name="type">Type of notification ("success", "error", "warning", "info")</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task ShowLineToastAsync(string type)
        {
            await QueueOrExecuteNotification(async () =>
            {
                if (CanUseJsInterop)
                {
                    await _jsRuntime.InvokeVoidAsync("showNotification", type);
                }
            });
        }

        /// <summary>
        /// Show a toast notification with an optional alert box
        /// </summary>
        /// <param name="type">Type of notification ("success", "error", "warning", "info")</param>
        /// <param name="message">Message to display in the alert</param>
        /// <param name="showAlert">Whether to show the alert box below the toast</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task ShowNotificationAsync(string type, string message, bool showAlert = false)
        {
            await QueueOrExecuteNotification(async () =>
            {
                if (CanUseJsInterop)
                {
                    await _jsRuntime.InvokeVoidAsync("showNotification", type, message, showAlert);
                }
            });
        }

        /// <summary>
        /// Show a success notification
        /// </summary>
        /// <param name="message">Optional message for alert</param>
        /// <param name="showAlert">Whether to show alert box</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task ShowSuccessAsync(string message = "", bool showAlert = false)
        {
            await QueueOrExecuteNotification(async () =>
            {
                if (CanUseJsInterop)
                {
                    await _jsRuntime.InvokeVoidAsync("showSuccess", message, showAlert);
                }
            });
        }

        /// <summary>
        /// Show an error notification
        /// </summary>
        /// <param name="message">Optional message for alert</param>
        /// <param name="showAlert">Whether to show alert box</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task ShowErrorAsync(string message = "", bool showAlert = false)
        {
            await QueueOrExecuteNotification(async () =>
            {
                if (CanUseJsInterop)
                {
                    await _jsRuntime.InvokeVoidAsync("showError", message, showAlert);
                }
            });
        }

        /// <summary>
        /// Show a warning notification
        /// </summary>
        /// <param name="message">Optional message for alert</param>
        /// <param name="showAlert">Whether to show alert box</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task ShowWarningAsync(string message = "", bool showAlert = false)
        {
            await QueueOrExecuteNotification(async () =>
            {
                if (CanUseJsInterop)
                {
                    await _jsRuntime.InvokeVoidAsync("showWarning", message, showAlert);
                }
            });
        }

        /// <summary>
        /// Show an info notification
        /// </summary>
        /// <param name="message">Optional message for alert</param>
        /// <param name="showAlert">Whether to show alert box</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task ShowInfoAsync(string message = "", bool showAlert = false)
        {
            await QueueOrExecuteNotification(async () =>
            {
                if (CanUseJsInterop)
                {
                    await _jsRuntime.InvokeVoidAsync("showInfo", message, showAlert);
                }
            });
        }

        /// <summary>
        /// Get the JavaScript code for the notification system
        /// </summary>
        /// <returns>The JavaScript code as a string</returns>
        private string GetNotificationJsCode()
        {
            return @"
// Track if a toast is currently displayed
window.activeToast = null;

// Function to show a line toast notification and optionally show an alert
window.showNotification = function(type, message = '', showAlert = false) {
    // If there's an active toast, remove it first
    if (window.activeToast) {
        clearTimeout(window.activeToast.timeout);
        clearInterval(window.activeToast.pulseAnimation);
        hideLineToast(window.activeToast.element);
    }
    
    // Duration in milliseconds
    const duration = 3000;
    
    // Get the toast element
    const toast = document.querySelector(`.line-toast-${type}`);
    if (!toast) return;
    
    // Remove any previous out animation
    toast.classList.remove('line-toast-out');
    
    // Show the toast
    toast.style.display = 'block';
    
    // Manually animate the width from 0 to 100%
    toast.style.transition = 'width 0.5s ease-out';
    toast.style.width = '0';
    
    // Force reflow to ensure animation plays
    void toast.offsetWidth;
    
    // Animate to full width
    toast.style.width = '100%';
    
    // Set up pulsing using CSS opacity transitions
    const pulseAnimation = setInterval(() => {
        toast.style.opacity = toast.style.opacity === '0.6' ? '0.9' : '0.6';
    }, 1000);
    
    // Set the active toast
    window.activeToast = {
        element: toast,
        pulseAnimation: pulseAnimation,
        timeout: setTimeout(() => {
            clearInterval(pulseAnimation);
            hideLineToast(toast);
            window.activeToast = null;
        }, duration)
    };
    
    // Show the alert notification if requested
    if (showAlert && message) {
        showAlertNotification(type, message);
    }
};

// Show a success notification
window.showSuccess = function(message = '', showAlert = false) {
    window.showNotification('success', message, showAlert);
};

// Show an error notification
window.showError = function(message = '', showAlert = false) {
    window.showNotification('error', message, showAlert);
};

// Show a warning notification
window.showWarning = function(message = '', showAlert = false) {
    window.showNotification('warning', message, showAlert);
};

// Show an info notification
window.showInfo = function(message = '', showAlert = false) {
    window.showNotification('info', message, showAlert);
};

// Legacy function for backward compatibility
window.showLineToast = function(type) {
    window.showNotification(type);
};

function showAlertNotification(type, message) {
    // Map toast types to alert types
    const alertTypeMap = {
        'success': 'success',
        'error': 'danger',
        'warning': 'warning',
        'info': 'primary'
    };
    
    const alertType = alertTypeMap[type] || 'primary';
    
    // Create or get the alert container
    let alertContainer = document.getElementById('top-right-alert-container');
    if (!alertContainer) {
        alertContainer = document.createElement('div');
        alertContainer.id = 'top-right-alert-container';
        alertContainer.className = 'top-right-alert-container';
        document.body.appendChild(alertContainer);
    }
    
    // Create alert element
    const alertEl = document.createElement('div');
    alertEl.className = `alert alert-${alertType} alert-dismissible fade show custom-alert-icon shadow-sm`;
    alertEl.setAttribute('role', 'alert');
    
    // Get the appropriate SVG icon
    const iconSvg = getAlertIcon(alertType);
    
    // Set the content with icon and message
    alertEl.innerHTML = `
        ${iconSvg}
        ${message}
        <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close"">
            <i class=""bi bi-x""></i>
        </button>
    `;
    
    // Add the alert to the container
    alertContainer.appendChild(alertEl);
    
    // Ensure the alert is visible - force reflow
    void alertEl.offsetWidth;
    alertEl.classList.add('show');
    
    // Auto-dismiss after 5 seconds
    setTimeout(() => {
        alertEl.classList.remove('show');
        setTimeout(() => {
            if (alertEl.parentNode) {
                alertEl.parentNode.removeChild(alertEl);
            }
        }, 300);
    }, 5000);
    
    // Add click handler to close button
    const closeButton = alertEl.querySelector('.btn-close');
    if (closeButton) {
        closeButton.addEventListener('click', function() {
            alertEl.classList.remove('show');
            setTimeout(() => {
                if (alertEl.parentNode) {
                    alertEl.parentNode.removeChild(alertEl);
                }
            }, 300);
        });
    }
}

function getAlertIcon(type) {
    let path = '';
    
    switch (type) {
        case 'primary':
        case 'info':
            path = '<path d=""M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-6h2v6zm0-8h-2V7h2v2z""></path>';
            break;
        case 'secondary':
        case 'success':
            path = '<path d=""M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z""></path>';
            break;
        case 'warning':
            path = '<path d=""M1 21h22L12 2 1 21zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z""></path>';
            break;
        case 'danger':
        case 'error':
            path = '<path d=""M15.73 3H8.27L3 8.27v7.46L8.27 21h7.46L21 15.73V8.27L15.73 3zM12 17.3c-.72 0-1.3-.58-1.3-1.3 0-.72.58-1.3 1.3-1.3.72 0 1.3.58 1.3 1.3 0 .72-.58 1.3-1.3 1.3zm1-4.3h-2V7h2v6z""></path>';
            break;
        default:
            path = '<path d=""M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-6h2v6zm0-8h-2V7h2v2z""></path>';
    }
    
    return `<svg class=""svg-${type}"" xmlns=""http://www.w3.org/2000/svg"" height=""1.5rem"" viewBox=""0 0 24 24"" width=""1.5rem"" fill=""#000000"">
        <path d=""M0 0h24v24H0z"" fill=""none""></path>
        ${path}
    </svg>`;
}

function hideLineToast(toast) {
    // Add out animation
    toast.classList.add('line-toast-out');
    
    // Remove after animation
    setTimeout(() => {
        toast.style.display = 'none';
        toast.classList.remove('line-toast-out');
        toast.style.width = '0';
        toast.style.opacity = '0.9';
    }, 500);
}";
        }
    }
}