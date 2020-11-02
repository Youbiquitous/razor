///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Toast
{
    /// <summary>
    /// Razor tag helper for element TOAST
    /// </summary>
    [HtmlTargetElement("toast")]
    public class ToastTagHelper : TagHelper
    {
        public ToastTagHelper()
        {
            AutoHide = true;
            Delay = 3000;
            PositionTop = "260px";
            PositionLeft = "200px";
            TitleClass = "text-primary";
            SubtitleClass = "text-muted";
        }

        public string Id { get; set; }

        /// <summary>
        /// Whether the toast shoudl automatically disappear after a fixed delay
        /// </summary>
        public bool AutoHide { get; set; }

        /// <summary>
        /// Milliseconds to wait before hiding the toast
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        /// Absolute position of the toast
        /// </summary>
        public string PositionTop { get; set; }
        public string PositionLeft { get; set; }
        public string PositionRight { get; set; }
        public string PositionBottom { get; set; }

        /// <summary>
        /// Title of the window
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Class for the title of the window
        /// </summary>
        public string TitleClass { get; set; }

        /// <summary>
        /// Small text on the right (ie. 11 min ago)
        /// </summary>
        public string Subtitle { get; set; }
        
        /// <summary>
        /// Class for the subtitle of the window
        /// </summary>
        public string SubtitleClass { get; set; }

        /// <summary>
        /// Body of the message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Class for the message 
        /// </summary>
        public string MessageClass { get; set; }



        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Replace <notifyicon> with <DIV> 
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            var style = output.Attributes["style"]?.Value.ToString();

            output.Attributes.SetAttribute("style", style);
            output.Attributes.SetAttribute("class", "toast");
            output.Attributes.SetAttribute("id", Id);
            output.Attributes.SetAttribute("data-autohide", AutoHide ?"true" :"false");
            output.Attributes.SetAttribute("data-delay", Delay);

            var top = !PositionTop.IsNullOrWhitespace() ? $"top: {PositionTop};" : "";
            var left = !PositionLeft.IsNullOrWhitespace() ? $"left: {PositionLeft};" : "";
            var right = !PositionRight.IsNullOrWhitespace() ? $"right: {PositionRight};" : "";
            var bottom = !PositionBottom.IsNullOrWhitespace() ? $"left: {PositionBottom};" : "";

            var position = $"position: absolute; {top} {left} {right} {bottom}".Trim();
            output.Attributes.SetAttribute("style", position);

            output.Content.AppendHtml("<div class='toast-header'>");

            var title = $"<strong class='mr-auto {TitleClass}' id='{Id}-title'>{Title}</strong>";  
            var subTitle = $"<small class='{SubtitleClass}' id='{Id}-subtitle'>{Subtitle}</small>";  
            output.Content.AppendHtml($"{title} {subTitle}");
            output.Content.AppendHtml("<button type='button' class='ml-2 mb-1 close' data-dismiss='toast'>&times;</button>");
            output.Content.AppendHtml($"</div><div class='toast-body {MessageClass}' id='{Id}-message'>{Message}</div>");
        }
    }
}

