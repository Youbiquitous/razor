///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using System.Threading.Tasks;
using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Dropdown
{
    /// <summary>
    /// Razor tag helper for element DROPDOWN
    /// </summary>
    [HtmlTargetElement("dropdown")]
    public class DropdownTagHelper : TagHelper
    {
        public DropdownTagHelper()
        {
            Btn = "";
            Id = "";
            Text = "";
        }

        /// <summary>
        /// ID of the element (toggle)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Text (HTML) of the toggle item (if any)
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Determines the CSS classes to be added to the A toggle item  
        /// </summary>
        public string Btn { get; set; }

        /// <summary>
        /// Menu on the right
        /// </summary>
        public bool Rightmost { get; set; }


        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var style = output.Attributes["style"]?.Value.ToString();
            var css = output.Attributes["class"]?.Value.ToString();
            var btn = Btn.ContainsAny("btn ", " btn", " btn ") ? Btn : $"btn {Btn}";
            var rightmost = Rightmost ? "dropdown-menu-right" : "";

            // Replace <dropdown> with <div> (maintains class/id)
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "dropdown");
            if (!Text.IsNullOrWhitespace())
            {
                var a = $"<a class='dropdown-toggle {btn}' href='#' role='button' id='{Id}' data-toggle='dropdown' aria-haspopup='true'>";
                output.Content.AppendHtml(a);
                output.Content.AppendHtml(Text);
                output.Content.AppendHtml("</a>");
            }

            // Begin of menu rendering
            var refId = $"{Id}-menu";
            var menuDiv = $"<div class='dropdown-menu py-0 {css} {rightmost}' id='{refId}' aria-labelledby='{refId}' style='{style}'>";
            output.Content.AppendHtml(menuDiv);

            if (!css.IsNullOrWhitespace())
                output.Attributes.Remove(context.AllAttributes["class"]);
            if (!style.IsNullOrWhitespace())
                output.Attributes.Remove(context.AllAttributes["style"]);

            // Evaluate the Razor content of the element body causing MARKUP/ITEMS to be evaluated
            var internalMarkup = (await output.GetChildContentAsync()).GetContent();
            if (!internalMarkup.IsNullOrWhitespace())
                output.Content.AppendHtml(internalMarkup);
            output.Content.AppendHtml("</div>");
        }
    }
}

