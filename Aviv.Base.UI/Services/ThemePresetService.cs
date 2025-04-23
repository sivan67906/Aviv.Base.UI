using Microsoft.JSInterop;

namespace Aviv.Base.UI.Services
{
    /// <summary>
    /// Represents a theme configuration preset with visual settings and menu association
    /// </summary>
    public class ThemePreset
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public AppState ThemeSettings { get; set; } = new AppState();
        public string? MenuId { get; set; }

        public ThemePreset(string id, string name, AppState settings, string? menuId = null)
        {
            Id = id;
            Name = name;
            ThemeSettings = settings;
            MenuId = menuId;
        }
    }

    /// <summary>
    /// Service for managing and switching between different theme presets
    /// </summary>
    public class ThemePresetService
    {
        private readonly Dictionary<string, ThemePreset> _themePresets = [];
        private readonly ILogger<ThemePresetService> _logger;
        private string _activePresetId = "default";

        public event Action? OnPresetChanged;

        public string ActivePresetId => _activePresetId;

        public ThemePresetService(ILogger<ThemePresetService> logger)
        {
            _logger = logger;
            InitializePresets();
        }

        private void InitializePresets()
        {
            // Default theme preset
            ThemePreset defaultPreset = new ThemePreset(
                "default",
                "Default Theme",
                new AppState
                {
                    ColorTheme = "light",
                    Direction = "ltr",
                    NavigationStyles = "vertical",
                    MenuStyles = "",
                    LayoutStyles = "default-menu",
                    PageStyles = "regular",
                    WidthStyles = "fullwidth",
                    MenuPosition = "fixed",
                    HeaderPosition = "fixed",
                    MenuColor = "dark",
                    HeaderColor = "light",
                    ThemePrimary = "",
                    ThemeBackground = "",
                    ThemeBackground1 = "",
                    BackgroundImage = ""
                },
                "default"
            );
            _themePresets.Add(defaultPreset.Id, defaultPreset);

            // Product Manager theme preset
            ThemePreset productManagerPreset = new ThemePreset(
                "product_manager",
                "Product Manager Theme",
                new AppState
                {
                    ColorTheme = "light",
                    Direction = "ltr",
                    NavigationStyles = "vertical",
                    MenuStyles = "",
                    LayoutStyles = "icon-overlay",
                    PageStyles = "regular",
                    WidthStyles = "fullwidth",
                    MenuPosition = "fixed",
                    HeaderPosition = "fixed",
                    MenuColor = "light",
                    HeaderColor = "light",
                    ThemePrimary = "78, 172, 76", // Green theme
                    ThemeBackground = "",
                    ThemeBackground1 = "",
                    BackgroundImage = ""
                },
                "product_manager"
            );
            _themePresets.Add(productManagerPreset.Id, productManagerPreset);

            // Support theme preset
            ThemePreset supportPreset = new ThemePreset(
                "support",
                "Support Theme",
                new AppState
                {
                    ColorTheme = "dark",
                    Direction = "ltr",
                    NavigationStyles = "horizontal",
                    MenuStyles = "menu-click",
                    LayoutStyles = "",
                    PageStyles = "modern",
                    WidthStyles = "fullwidth",
                    MenuPosition = "fixed",
                    HeaderPosition = "fixed",
                    MenuColor = "dark",
                    HeaderColor = "dark",
                    ThemePrimary = "92, 144, 163", // Blue-gray theme
                    ThemeBackground = "45, 55, 72",
                    ThemeBackground1 = "55, 65, 81",
                    BackgroundImage = ""
                },
                "support"
            );
            _themePresets.Add(supportPreset.Id, supportPreset);

            ThemePreset publishedFormPreset = new ThemePreset(
    "published_form",
    "Published Form Theme",
    new AppState
    {
        ColorTheme = "light",
        Direction = "ltr",
        NavigationStyles = "horizontal", // Horizontal layout as requested
        MenuStyles = "menu-click",
        LayoutStyles = "",
        PageStyles = "regular",
        WidthStyles = "fullwidth",
        MenuPosition = "fixed",
        HeaderPosition = "fixed",
        MenuColor = "light",
        HeaderColor = "light",
        ThemePrimary = "143, 105, 225", // Purple theme (#8F69E1)
        ThemeBackground = "",
        ThemeBackground1 = "",
        BackgroundImage = ""
    },
    "default" // Use the default menu items
);
            _themePresets.Add(publishedFormPreset.Id, publishedFormPreset);
        }

        public ThemePreset? GetPreset(string presetId)
        {
            if (_themePresets.TryGetValue(presetId, out ThemePreset? preset))
            {
                return preset;
            }

            _logger.LogWarning("Theme preset with ID {PresetId} not found", presetId);
            return null;
        }

        public ThemePreset? GetActivePreset()
        {
            return GetPreset(_activePresetId);
        }

        public bool SetActivePreset(string presetId)
        {
            if (_themePresets.ContainsKey(presetId))
            {
                if (_activePresetId != presetId)
                {
                    _activePresetId = presetId;
                    _logger.LogInformation("Active theme preset changed to {PresetId}", presetId);
                    OnPresetChanged?.Invoke();
                }
                return true;
            }

            _logger.LogWarning("Failed to set active theme preset. Preset {PresetId} not found", presetId);
            return false;
        }

        public void RegisterPreset(ThemePreset preset)
        {
            if (string.IsNullOrEmpty(preset.Id))
            {
                _logger.LogError("Cannot register theme preset with empty ID");
                return;
            }

            _themePresets[preset.Id] = preset;
            _logger.LogInformation("Theme preset {PresetId} registered", preset.Id);
        }

        public IEnumerable<ThemePreset> GetAllPresets()
        {
            return _themePresets.Values;
        }

        public async Task ApplyThemePresetAttributes(IJSRuntime jsRuntime)
        {
            ThemePreset? preset = GetActivePreset();
            if (preset != null)
            {
                await jsRuntime.InvokeVoidAsync("interop.addAttributeToHtml", "data-theme-preset", preset.Id);
            }
            else
            {
                await jsRuntime.InvokeVoidAsync("interop.removeAttributeFromHtml", "data-theme-preset");
            }
        }
    }
}