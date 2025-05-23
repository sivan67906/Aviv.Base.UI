﻿using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Aviv.Base.UI.Models.FormTool
{
    /// <summary>
    /// Represents a form model for the dynamic form builder
    /// </summary>
    public class FormModel
    {
        public string FormId { get; set; } = string.Empty;
        public string PageHeader { get; set; } = string.Empty;
        public List<BreadcrumbInfo> BreadcrumbItems { get; set; } = [];
        public List<CardModel> Cards { get; set; } = [];
        public List<ButtonModel> Buttons { get; set; } = []; // New property for buttons

        // Property to store the form layout preference (vertical or horizontal)
        public string FormLayout { get; set; } = "vertical";
    }

    /// <summary>
    /// Represents a card/section in a form
    /// </summary>
    public class CardModel
    {
        public string CardTitle { get; set; } = string.Empty;
        public int CardSize { get; set; } = 12;

        [JsonIgnore]
        public bool IsEditMode { get; set; } = false;

        // Use ObservableCollection for dynamic Blazor UI updates
        public ObservableCollection<FieldModel> Fields { get; set; } = [];
    }

    /// <summary>
    /// Represents a field in a form card
    /// </summary>
    public class FieldModel
    {
        public string LabelName { get; set; } = string.Empty;
        public string InputType { get; set; } = string.Empty;
        public int ColumnSize { get; set; } = 6;
        public string Options { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public List<string> MultiValue { get; set; } = [];
        public bool IsRequired { get; set; } = false;
    }

    /// <summary>
    /// Represents a button in a form
    /// </summary>
    public class ButtonModel
    {
        public string ButtonName { get; set; } = string.Empty;
        public string ButtonClass { get; set; } = "btn-primary";
        public string ButtonPosition { get; set; } = "right"; // "left" or "right"
        public string ButtonIcon { get; set; } = string.Empty; // Optional icon class
    }

    /// <summary>
    /// Represents a breadcrumb navigation item for serialization
    /// </summary>
    public class BreadcrumbInfo
    {
        public string Text { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }

    /// <summary>
    /// Model for form submission
    /// </summary>
    public class FormPublishModel
    {
        public string FormId { get; set; } = string.Empty;
        public string PageHeader { get; set; } = string.Empty;
        public List<BreadcrumbInfo> BreadcrumbItems { get; set; } = [];
        public List<CardModel> Cards { get; set; } = [];
        public string FormLayout { get; set; } = "vertical";
    }

    /// <summary>
    /// Form view model for Blazor EditForm
    /// </summary>
    public class FormViewModel
    {
        public string PageHeader { get; set; } = string.Empty;
    }
}