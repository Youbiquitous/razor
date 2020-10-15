///////////////////////////////////////////////////////////////////
//
// Live-R Corinto: Tournament manager app
// Copyright (c) Crionet 2020-2026
//
// Author: Dino Esposito (http://youbiquitous.net)
//

using Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Common;
using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Dropdown
{
    [HtmlTargetElement("listitem2", ParentTag = "dropdown")]
    public class DropdownList2ItemTagHelper : TagHelper
    {
        public DropdownList2ItemTagHelper()
        {
            TitlePosition = TitlePosition.Top;
        }

        /// <summary>
        /// Full FA icon styles for the icon on the left side
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// URL to the image to show on the left side of the list item
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// How to go to the specified link
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// First line of text of the two-rows list item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Second line of the two-rows list item
        /// </summary>
        public string Subtitle { get; set; }

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
        /// Status to be shown on the list-item (small circle if images are used, background circle if icons are used)
        /// </summary>
        public ElementStatus Status { get; set; }

        /// <summary>
        /// How to position first and second line (where the Title line goes)
        /// </summary>
        public TitlePosition TitlePosition { get; set; }

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
            var url = Url.IsNullOrWhitespace() ? "#" : Url;
            var showIcon = Image.IsNullOrWhitespace();
            
            output.TagName = "a";
            output.Attributes.SetAttribute("style", style);
            output.Attributes.SetAttribute("target", Target);
            output.Attributes.SetAttribute("class", $"dropdown-item d-flex align-items-center {css} {disabled} {active}");
            output.Attributes.SetAttribute("href", url);
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.Clear();

            var bsClass = GetBootstrapClassForStatus(Status);
            var txtIcon = GetBootstrapClassForIconTextAndStatus(Status);
            var pic = showIcon
                ? $"<div class='icon-circle {bsClass}'><i class='{Icon} {txtIcon}'></i></div>"
                : $"<img class='rounded-circle' src='{Image}' alt=''>";
            var status = "";
            if (Status != ElementStatus.None && !showIcon)
            {
                status = $"<div class=\"status-indicator {bsClass}\"></div>";
            }

            // Rendering HTML
            var line1 = $"<div class=\"font-weight-bold\"><div class=\"text-truncate\">{Title}</div></div>";
            var line2 = $"<div class=\"small text-gray-500\">{Subtitle}</div>";
            if (TitlePosition == TitlePosition.Bottom)
            {
                var temp = line1;
                line1 = line2;
                line2 = temp;
            }

            var html = "<div class=\"dropdown-list-image mr-3\">" +
                       pic +
                       status +
                       "</div><div>" +
                       line1 +
                       line2 +
                       "</div>";
            output.Content.AppendHtml(html);
        }

        private static string GetBootstrapClassForStatus(ElementStatus status)
        {
            switch (status)
            {
                case ElementStatus.Disabled:
                    return "bg-dark";
                case ElementStatus.Active:
                    return "bg-primary";
                case ElementStatus.Success:
                    return "bg-success";
                case ElementStatus.Warning:
                    return "bg-warning";
                case ElementStatus.Danger:
                    return "bg-danger";
                default:
                    return "";
            }
        }

        private static string GetBootstrapClassForIconTextAndStatus(ElementStatus status)
        {
            switch (status)
            {
                case ElementStatus.Disabled:
                    return "text-muted";
                case ElementStatus.Active:
                    return "text-white";
                case ElementStatus.Success:
                    return "text-white";
                case ElementStatus.Warning:
                    return "text-white";
                case ElementStatus.Danger:
                    return "text-white";
                default:
                    return "text-primary";
            }
        }
    }
}

