///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using System.Threading.Tasks;
using Crionet.LiveR.Corinto.DomainModel.Utils.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Markup
{
    /// <summary>
    /// Razor tag helper for element UPPER
    /// </summary>
    [HtmlTargetElement("upper")]
    public class UpperTagHelper : TagHelper
    {
        public UpperTagHelper()
        {
            Inline = true;
        }

        /// <summary>
        /// ID/name of the tag
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Value to show
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Whether inline or block
        /// </summary>
        public bool Inline { get; set; }

        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // Replace <upper> with <SPAN> or <DIV> 
            output.TagName = Inline ?"span" :"div";
            output.TagMode = TagMode.StartTagAndEndTag;

            var content = (await output.GetChildContentAsync()).GetContent();
            if (content.IsNullOrWhitespace())
                content = Value;

            //var style = output.Attributes["style"]?.Value.ToString();
            var css = output.Attributes["class"]?.Value.ToString();

            output.Attributes.SetAttribute("class", $"text-uppercase {css}");
            output.Attributes.SetAttribute("id", Id);
            if (Value.IsNullOrWhitespace())
                output.Content.AppendHtml(content);
        }
    }
}

