///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.NotifyIcon
{
    /// <summary>
    /// Razor tag helper for element NOTIFYICON
    /// </summary>
    [HtmlTargetElement("notifyicon")]
    public class NotifyIconTagHelper : TagHelper
    {
        public NotifyIconTagHelper()
        {
            BadgeClass = "badge-primary";
            Icon = "fa-bell";
            Url = "";
        }

        /// <summary>
        /// Determines the FA icon to use
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Determines the text to show in the badge
        /// </summary>
        public string Badge { get; set; }

        /// <summary>
        /// Determines the CSS style of the badge 
        /// </summary>
        public string BadgeClass { get; set; }
        
        /// <summary>
        /// URL to go (URL wins over dropdown)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Target for when jumping to the URL (if any)
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Replace <notifyicon> with <a> 
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;

            // Prepare output
            var href = Url ?? "#";

            output.Attributes.SetAttribute("href", href);
            if (!Url.IsNullOrWhitespace())
                output.Attributes.SetAttribute("target", Target);
            output.Attributes.SetAttribute("class", "nav-link dropdown-toggle");
            output.Attributes.SetAttribute("role", "button");
            if (Url.IsNullOrWhitespace())
            {
                output.Attributes.SetAttribute("data-toggle", "dropdown");
                output.Attributes.SetAttribute("aria-haspopup", "true");
                output.Attributes.SetAttribute("aria-expanded", "false");
            }
            
            output.Content.Clear();
            var icon = $"<i class=\"{Icon}\"></i>";
            var badge = $"<span class=\"badge {BadgeClass} badge-counter\">{Badge}</span>";
            output.Content.AppendHtml(icon);
            if (!Badge.IsNullOrWhitespace())
                output.Content.AppendHtml(badge);
        }
    }
}

