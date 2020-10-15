///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using System;
using System.Threading.Tasks;
using Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Common;
using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Alert
{
    /// <summary>
    /// Razor tag helper for element ALERT
    /// </summary>
    [HtmlTargetElement("alert")]
    public class AlertTagHelper : TagHelper
    {
        public AlertTagHelper()
        {
            Type = ElementStatus.Info;
            Dismiss = DismissMode.None;
            Padding = PaddingStyle.Default;
        }

        /// <summary>
        /// Bold title to show, when no HTML content is specified
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Plain text to show, when no HTML content is specified
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// How to position first and second line (where the Title line goes)
        /// </summary>
        public TitlePosition TitlePosition { get; set; }  

        /// <summary>
        /// Determines the visual style according to Bootstrap states (primary|warning|etc)
        /// </summary>
        public ElementStatus Type { get; set; }

        /// <summary>
        /// Whether the alert is dismissible (and how)
        /// </summary>
        public DismissMode Dismiss { get; set; }

        /// <summary>
        /// Whether fading should be applied to appearance
        /// </summary>
        public bool Fade { get; set; }

        /// <summary>
        /// Icon to add
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Padding within the alert box
        /// </summary>
        public PaddingStyle Padding { get; set; }


        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // Evaluate the Razor content of the email's element body 
            var content = (await output.GetChildContentAsync()).GetContent();
            if (content.IsNullOrWhitespace())
                content = ComposeTitleAndText();

            if (Padding == PaddingStyle.Thin)
                Dismiss = Dismiss.HasFlag(DismissMode.Button)
                    ? DismissMode.ClickInside
                    : Dismiss;
            var css = output.Attributes["class"]?.Value.ToString();
            var alertTypeClass = $"alert-{BootstrapUtils.KeyFrom(Type)}";
            //var style = output.Attributes["style"]?.Value.ToString();
            var paddingStyle = AdjustPaddingStyle();
            var fade = Fade ? "fade show" : "";
            var dismissible = IsDismissible() ? "alert-dismissible" : "";
            var dismissibleButton = NeedsDismissButton()
                ? $"<button type='button' class='close' data-dismiss='alert'>&times;</span></button>"
                : "";
            var dismissibleClickHandler = NeedsDismissClickHandler()
                ? $"this.style.display = 'none';"
                : "";
            

            // Write out
            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"alert {alertTypeClass} {dismissible} {fade} {paddingStyle} {css}".TrimEnd());
            output.Attributes.SetAttribute("onclick", dismissibleClickHandler);
            output.Attributes.SetAttribute("role", "alert");
            output.Content.AppendHtml($"{content}{dismissibleButton}");
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        /// <summary>
        /// Format plain text if any
        /// </summary>
        /// <returns></returns>
        private string ComposeTitleAndText()
        {
            var title = Title.IsNullOrWhitespace() ? "" : $"<strong class='mr-2'>{Title}</strong>";

            // Side by side
            if (TitlePosition == TitlePosition.Default)
                return $"{title}{Text}";

            // Title on top
            if (TitlePosition == TitlePosition.Top)
                return $"<div>{title}</div><div>{Text}</div>";

            // Title at bottom
            if (TitlePosition == TitlePosition.Bottom)
                return $"<div>{Text}</div><div>{title}</div>";

            return "";
        }

        /// <summary>
        /// Whether the CLOSE button is required
        /// </summary>
        /// <returns></returns>
        private bool NeedsDismissButton()
        {
            return Dismiss.HasFlag(DismissMode.Button);
        }

        /// <summary>
        /// Whether it is dismissible
        /// </summary>
        /// <returns></returns>
        private bool IsDismissible()
        {
            return Dismiss != DismissMode.None; 
        }

        /// <summary>
        /// Whether a click handler is required
        /// </summary>
        /// <returns></returns>
        private bool NeedsDismissClickHandler()
        {
            return Dismiss.HasFlag(DismissMode.ClickInside);
        }

        /// <summary>
        /// Style p- Bootstrap classes for padding
        /// </summary>
        /// <returns></returns>
        private string AdjustPaddingStyle()
        {
            if (Padding == PaddingStyle.Thick)
                return "p-5"; 
            if (Padding == PaddingStyle.Thin)
                return "px-2 py-0"; 

            return "";
        }
    }
}
