///////////////////////////////////////////////////////////////////
//
// Live-R Corinto: Tournament manager app
// Copyright (c) Crionet 2020-2026
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Common;
using Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Dropdown;
using Crionet.LiveR.Corinto.Shared.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Progress
{
    /// <summary>
    /// Razor tag helper for element BAR (progressbar)
    /// </summary>
    [HtmlTargetElement("bar")]
    public class ProgressTagHelper : TagHelper
    {
        public ProgressTagHelper()
        {
            Value = 0;
            Status = ElementStatus.Active;
            Height = 5;
            LabelPosition = TitlePosition.Default;
            LabelClass = "tiny py-1";
            BackgroundStatus = ElementStatus.None;
        }

        /// <summary>
        /// Determines the value to show
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Determines the style of the bar
        /// </summary>
        public ElementStatus Status { get; set; }

        /// <summary>
        /// Determines the style of the background of the bar
        /// </summary>
        public ElementStatus BackgroundStatus { get; set; }

        /// <summary>
        /// Where to place an optional label
        /// </summary>
        public TitlePosition LabelPosition { get; set; }

        /// <summary>
        /// Determines the height in pixels
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Text within the bar: Use {0} to indicate the percentage completed and [0] to indicate remaining part (100-X)
        /// </summary>
        public string Label { get; set; }
        
        /// <summary>
        /// Style of the label when top/bottom positioned
        /// </summary>
        public string LabelClass { get; set; }


        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var actual = Value.Fit(0, 100);
            var actualLabel = string.Format(Label, actual);
            if (Label.Contains("[0]"))
            {
                Label = Label.Replace("[0]", "{0}");
                actualLabel = string.Format(Label, 100-actual);
            }
            
            var foreBgClass = BootstrapUtils.BgFrom(Status);
            var backBgClass = BootstrapUtils.BgFrom(BackgroundStatus);
            var embeddedText = LabelPosition == TitlePosition.Default ? actualLabel : "";
            var blocks = GetLabelBlocks(actualLabel);

            var body = $"{blocks.Item1}" +
                       $"<div class='progress {backBgClass}' style='height:{Height}px;'>" +
                       $"<div class='progress-bar {foreBgClass}' role='progressbar' style='width: {actual}%;'" +
                       $"aria-valuenow='{actual}' aria-valuemin='0' aria-valuemax='100'>{embeddedText}</div></div>" +
                       $"{blocks.Item2}";

            // Build 
            output.TagName = "div";
            output.Content.Clear();
            output.Content.AppendHtml(body);
        }

        private (string, string) GetLabelBlocks(string text)
        {
            if (LabelPosition == TitlePosition.Top)
                return ($"<div class='{LabelClass}'>{text}</div>", "");
            if (LabelPosition == TitlePosition.Bottom)
                return ("", $"<div class='{LabelClass}'>{text}</div>");
            return ("", "");
        }
    }
}

