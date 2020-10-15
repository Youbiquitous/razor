///////////////////////////////////////////////////////////////////
//
// Live-R Corinto: Tournament manager app
// Copyright (c) Crionet 2020-2026
//
// Author: Dino Esposito (http://youbiquitous.net)
//

using System.Threading.Tasks;
using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Dropdown
{
    [HtmlTargetElement("markup", ParentTag = "dropdown")]
    public class DropdownMarkupTagHelper : TagHelper
    {
        /// <summary>
        /// Optional URL associated  with the present markup chunk
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override async Task ProcessAsync(
            TagHelperContext context, TagHelperOutput output)
        {
            var isAnchor = !Url.IsNullOrWhitespace();

            // Evaluate the Razor content of the element's body 
            var body = (await output.GetChildContentAsync()).GetContent();
            body = body.Trim();
            if (body.IsNullOrWhitespace())
                return;

            // Create a new DIV or A
            output.TagName = isAnchor ? "a" : "div";

            // Prepare output
            var defaultCss = "w-100 p-2 text-center";
            var style = output.Attributes["style"]?.Value ?? "";
            style = string.Concat(style, ";cursor:default;");
            output.Attributes.SetAttribute("style", style);
            var css = output.Attributes["class"]?.Value.ToString();
            css = css.IsNullOrWhitespace() ? defaultCss : $"{defaultCss} {css}";
            output.Attributes.SetAttribute("class", $"{defaultCss} {css}");
            if (isAnchor)
                output.Attributes.SetAttribute("href", Url);
            output.Content.AppendHtml(body);
        }
    }
}

