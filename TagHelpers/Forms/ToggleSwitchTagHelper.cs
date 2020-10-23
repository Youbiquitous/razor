using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Forms
{
    /// <summary>
    /// Razor tag helper for element TOGGLE CHECKBOX
    /// </summary>
    [HtmlTargetElement("toggle")]
    public class ToggleSwitchTagHelper : TagHelper
    {
        public ToggleSwitchTagHelper()
        {
            Text = "Toggle this switch element";
            Id = "customSwitch1";
        }
        /// <summary>
        /// Text for toggle
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Id for script
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name for script
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Checked by default or not
        /// </summary>
        public bool Checked { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string check = Checked == true ? "checked" : null;
            output.Content.AppendHtml($"<div class=\"custom-control custom-switch\"><input type=\"checkbox\" class=\"custom-control-input\" id=\"{Id}\" name=\"{Name}\" {check}><label class=\"custom-control-label\" for=\"{Id}\">{Text}</label></div>");
            output.TagMode = TagMode.StartTagAndEndTag;
        }            
    }
}
