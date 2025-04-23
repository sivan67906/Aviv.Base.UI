namespace Aviv.Base.UI.Models.FormTool
{
    /// <summary>
    /// Represents a dynamic page with form components
    /// </summary>
    public class DynamicPage
    {
        public string PageHeader { get; set; } = string.Empty;
        public List<DynamicCardRow> Rows { get; set; } = [];
    }

    /// <summary>
    /// Represents a row of fields in a dynamic form
    /// </summary>
    public class DynamicCardRow
    {
        public List<DynamicFieldForm> Fields { get; set; } = [];
    }

    /// <summary>
    /// Represents a field in a dynamic form
    /// </summary>
    public class DynamicFieldForm
    {
        public string Label { get; set; } = string.Empty;
        public string InputType { get; set; } = string.Empty;
        public int ColSize { get; set; } = 6;
        public bool IsRequired { get; set; } = false;
        public string OptionsRaw { get; set; } = string.Empty;
        public List<string> Options { get; set; } = [];
        public string Value { get; set; } = string.Empty;
        public List<string> MultiValue { get; set; } = [];
        public Dictionary<string, bool> CheckboxValues { get; set; } = [];
    }
}