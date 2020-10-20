using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperTest.Common.TagHelpers.InputText
{
    /// <summary>
    /// Razor tag helper for element INPUT TEXT
    /// </summary>
    [HtmlTargetElement("inputText")]
    public class InputTextTagHelper : TagHelper
    {
        public InputTextTagHelper()
        {
            Placeholder = "Insert text";
            Prepend = null;
            Append = null;
            Id = "input-text";
        }

        /// <summary>
        /// Placeholder to show
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Identificator for script
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of input text
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of input text
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Text (or icon) to prepend to input box
        /// </summary>
        public string Prepend { get; set; }

        /// <summary>
        /// Text (or icon) to append to input box
        /// </summary>
        public string Append { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string prepend = Prepend.IsNullOrWhitespace() ? null : $"<div class=\"input-group-prepend\"><span class=\"input-group-text\">{Prepend}</span></div>";
            string append = Append.IsNullOrWhitespace() ? null : $"<div class=\"input-group-append\"><span class=\"input-group-text\">{Append}</span></div>";
            string box = $"<input type=\"text\" class=\"form-control\" placeholder=\"{Placeholder}\" id=\"{Id}\" name=\"{Name}\" value=\"{Value}\">";
            output.Content.AppendHtml($"<div class=\"input-group mb-3\">{prepend}{box}{append}</div>");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
