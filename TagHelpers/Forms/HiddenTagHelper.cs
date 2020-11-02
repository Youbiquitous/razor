///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Forms
{
    /// <summary>
    /// Razor tag helper for element HIDDEN
    /// </summary>
    [HtmlTargetElement("hidden")]
    public class HiddenTagHelper : TagHelper
    {
        public HiddenTagHelper()
        {
        }

        /// <summary>
        /// ID/name of the tag
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Value to store
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Replace <notifyicon> with <INPUT> 
            output.TagName = "input";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("type", "hidden");
            output.Attributes.SetAttribute("id", Id);
            output.Attributes.SetAttribute("name", Id);
            output.Attributes.SetAttribute("value", Value);
        }
    }
}

