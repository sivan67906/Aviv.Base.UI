using System.Text.Json;
using Aviv.Base.UI.Models.FormTool;
using Microsoft.JSInterop;

namespace Aviv.Base.UI.Services
{
    /// <summary>
    /// Service for managing form operations
    /// </summary>
    public class CustomFormService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly Dictionary<string, FormModel> _formCache = [];

        public CustomFormService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Publishes a form and returns the URL
        /// </summary>
        public async Task<string> PublishFormAsync(FormModel form, string baseUri)
        {
            // Generate a unique ID if not already set
            if (string.IsNullOrEmpty(form.FormId))
            {
                form.FormId = Guid.NewGuid().ToString();
            }

            // Store in cache
            _formCache[form.FormId] = form;

            // Serialize the form to JSON
            string formJson = JsonSerializer.Serialize(form);

            // Store in local storage (for demo purposes)
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", $"form_{form.FormId}", formJson);

            // Generate the URL to view this form
            return $"{baseUri}view-form/{form.FormId}";
        }

        /// <summary>
        /// Retrieves a form by ID
        /// </summary>
        public async Task<FormModel?> GetFormAsync(string formId)
        {
            // Check cache first
            if (_formCache.TryGetValue(formId, out FormModel cachedForm))
            {
                return cachedForm;
            }

            // If not in cache, try to retrieve from local storage
            string formJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", $"form_{formId}");

            if (string.IsNullOrEmpty(formJson))
            {
                return null;
            }

            // Deserialize and cache the form
            FormModel form = JsonSerializer.Deserialize<FormModel>(formJson);
            _formCache[formId] = form;

            return form;
        }

        /// <summary>
        /// Saves a form to local storage
        /// </summary>
        public async Task SaveFormAsync(FormModel form)
        {
            // Update cache
            _formCache[form.FormId] = form;

            // Serialize to JSON
            string formJson = JsonSerializer.Serialize(form);

            // Store in local storage
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", $"form_{form.FormId}", formJson);
        }

        /// <summary>
        /// Copies text to clipboard
        /// </summary>
        public async Task CopyToClipboardAsync(string text)
        {
            await _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
    }
}