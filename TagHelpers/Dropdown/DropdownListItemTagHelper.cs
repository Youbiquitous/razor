///////////////////////////////////////////////////////////////////
//
// Live-R Corinto: Tournament manager app
// Copyright (c) Crionet 2020-2026
//
// Author: Dino Esposito (http://youbiquitous.net)
//

using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Dropdown
{
    [HtmlTargetElement("listitem", ParentTag = "dropdown")]
    [HtmlTargetElement("listitem", ParentTag = "menu")]
    public class DropdownListItemTagHelper : TagHelper
    {
        public DropdownListItemTagHelper()
        {
            Type = DropdownListItemType.Link;
        }

        /// <summary>
        /// Type of the list item
        /// </summary>
        public DropdownListItemType Type { get; set; }

        /// <summary>
        /// Full FA icon styles 
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Label of the list item
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// URL to jump to
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Whether the list item is disabled
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Whether the list item should be marked as highlighted
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Text for the right-edge bubble text
        /// </summary>
        public string Badge { get; set; }

        /// <summary>
        /// Class for the bubble text
        /// </summary>
        public string BadgeClass { get; set; }

        /// <summary>
        /// How to jump to the linked URL
        /// </summary>
        public string Target { get; set; }

        // HTML placeholder for an empty icon
        private string IconPlaceholder = "<div style='border: solid 1px transparent; width: 1.55em; display: inline-block'>&nbsp;</div>";
    
        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var style = output.Attributes["style"]?.Value.ToString();
            style = style.IsNullOrWhitespace() ? "" : style;
            var css = output.Attributes["class"]?.Value.ToString();
            css = css.IsNullOrWhitespace() ? "" : css;
            var disabled = Disabled ? "disabled" : "";
            var active = Active ? "active" : "";
            var icon = Icon.IsNullOrWhitespace() ? IconPlaceholder : $"<i class='mr-2 {Icon}'></i>";
            var url = Url.IsNullOrWhitespace() ? "#" : Url;

            if (Type == DropdownListItemType.Link)
            {
                output.TagName = "a";
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Attributes.SetAttribute("style", style);
                output.Attributes.SetAttribute("target", Target);
                output.Attributes.SetAttribute("class", $"dropdown-item {css} {disabled} {active}");
                output.Attributes.SetAttribute("href", url);
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Content.Clear();
                var badge = "";
                var badgeClass = BadgeClass ?? "badge-primary";
                if (!Badge.IsNullOrWhitespace())
                {
                    badge = $"<div><span class=\"badge {badgeClass}\">{Badge}</span></div>";
                }
                var markup = $"<div class='d-flex justify-content-between'><div>{icon}{Label}</div>{badge}</div>";
                output.Content.AppendHtml(markup);
            } else if (Type == DropdownListItemType.Divider)
            {
                output.TagName = "div";
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Attributes.SetAttribute("style", style);
                output.Attributes.SetAttribute("class", $"dropdown-divider {css}");
            } else if (Type == DropdownListItemType.Header)
            {
                output.TagName = "h6";
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Attributes.SetAttribute("style", style);
                output.Attributes.SetAttribute("class", $"dropdown-header {css}");
                output.Content.AppendHtml(Label);
            }
          
        }
    }

}

