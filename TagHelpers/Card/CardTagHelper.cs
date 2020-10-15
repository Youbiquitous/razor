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

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Card
{
    /// <summary>
    /// Razor tag helper for element CARD
    /// </summary>
    [HtmlTargetElement("card")]
    public class CardTagHelper : TagHelper
    {
        public CardTagHelper()
        {
            Value = 0;
            Text = "";
            Icon = "";
            Title = "";
            Url = "";
            Subtext = "";
            BorderSize = BorderSize.Normal;
            Type = ElementStatus.Active;
        }

        /// <summary>
        /// Determines the visual style according to Bootstrap states (primary|warning|etc)
        /// </summary>
        public ElementStatus Type { get; set; }

        /// <summary>
        /// Size of the left border
        /// </summary>
        public BorderSize BorderSize { get; set; }

        /// <summary>
        /// Title of the card
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Full FA icon to show (fa fa-xxx ...)
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Img (max 50x50) to show (takes precedence over icon)
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// CSS class for images and icons
        /// </summary>
        public string ImgClass { get; set; }

        /// <summary>
        /// Text to display (takes precedence over Value)
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Secondary text to display 
        /// </summary>
        public string Subtext { get; set; }

        /// <summary>
        /// If specified, shows a progress bar
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// URL to go if clicked
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Target of the clicked URL
        /// </summary>
        public string Target { get; set; }


        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var cardTypeClass = "";
            var titleTypeClass = "";
            var bgTypeClass = "";
            var outerDivClass = "col-xl-3 col-md-6 mb-4 plain-cursor";
            var actualText = "";

            if (Type == ElementStatus.Active)
            {
                cardTypeClass = "border-left-primary";
                titleTypeClass = "text-primary";
            }
            else if (Type == ElementStatus.Success)
            {
                cardTypeClass = "border-left-success";
                titleTypeClass = "text-success";
                bgTypeClass = "bg-success";
            }
            else if (Type == ElementStatus.Info)
            {
                cardTypeClass = "border-left-info";
                titleTypeClass = "text-info";
                bgTypeClass = "bg-info";
            }
            else if (Type == ElementStatus.Danger)
            {
                cardTypeClass = "border-left-danger";
                titleTypeClass = "text-danger";
                bgTypeClass = "bg-danger";
            }
            else if (Type == ElementStatus.Warning)
            {
                cardTypeClass = "border-left-warning";
                titleTypeClass = "text-warning";
                bgTypeClass = "bg-warning";
            }

            // Adjust some input attributes
            if (!Url.IsNullOrWhitespace())
                outerDivClass += " card-hover";

            if (Text.IsNullOrWhitespace() && Value >= 0 && Value <= 100)
            {
                actualText = $"{Value}";
            }
            else
            {
                actualText = Text;
                Value = -1;
            }

            if (Value < 0)
            {
                output.TagName = "div";
                output.Attributes.SetAttribute("class", outerDivClass);
                output.Content.AppendHtml($"<div class='card {cardTypeClass} {GetBorderCssClass(BorderSize)} shadow h-100 py-2'>");
                output.Content.AppendHtml("<div class='card-body'>");
                if (!Url.IsNullOrWhitespace())
                    output.Content.AppendHtml($"<a href='{Url}' target='{Target}' class='no-decoration'>");
                output.Content.AppendHtml("<div class='row no-gutters align-items-center'>");
                output.Content.AppendHtml("<div class='col mr-2'>");
                output.Content.AppendHtml("<div class='text-xs font-weight-bold " + titleTypeClass + " text-uppercase mb-1'>" + Title + "</div>");
                output.Content.AppendHtml("<div class='h5 mb-0 font-weight-bold text-gray-800'>" + actualText + 
                                          "<span class='small ml-3'>" + Subtext + "</span></div>");
                output.Content.AppendHtml("</div>");
                output.Content.AppendHtml("<div class='col-auto'>");
                if (Img.IsNullOrWhitespace())
                    output.Content.AppendHtml($"<i class='{Icon} {ImgClass} text-gray-300'></i>");
                else
                    output.Content.AppendHtml($"<img src='{Img}' class='{ImgClass}' style='width: 45px;'>");
                output.Content.AppendHtml("</div></div>");
                if (!Url.IsNullOrWhitespace())
                    output.Content.AppendHtml("</a>");
                output.Content.AppendHtml("</div></div>");
                output.TagMode = TagMode.StartTagAndEndTag;
            }
            else 
            {
                output.TagName = "div";
                output.Attributes.SetAttribute("class", "col-xl-3 col-md-6 mb-4");
                output.Content.AppendHtml($"<div class='card {cardTypeClass} {GetBorderCssClass(BorderSize)} shadow h-100 py-2'>");
                output.Content.AppendHtml("<div class='card-body'>");
                output.Content.AppendHtml("<div class='row no-gutters align-items-center'>");
                output.Content.AppendHtml("<div class='col mr-2'>");
                output.Content.AppendHtml($"<div class='text-xs font-weight-bold {titleTypeClass} text-uppercase mb-1'>{Title}</div>");
                output.Content.AppendHtml("<div class='row no-gutters align-items-center'>");
                output.Content.AppendHtml("<div class='col-auto'>");
                output.Content.AppendHtml($"<div class='h5 mb-0 font-weight-bold text-gray-800 mr-3'>{actualText}</div>");
                output.Content.AppendHtml("</div>");
                output.Content.AppendHtml("<div class='col'>");
                output.Content.AppendHtml("<div class='progress progress-sm mr-2'>");
                output.Content.AppendHtml($"<div class='progress-bar {bgTypeClass}' role='progressbar' style='width: {actualText}%' aria-valuenow='{actualText}' aria-valuemin='0' aria-valuemax='100'></div>");
                output.Content.AppendHtml("</div></div></div></div>");
                output.Content.AppendHtml("<div class=\"col-auto\">");
                if (Img.IsNullOrWhitespace())
                    output.Content.AppendHtml($"<i class='{Icon} {ImgClass} text-gray-300'></i>");
                else
                    output.Content.AppendHtml($"<img src='{Img}' class='{ImgClass}' style='width: 45px;'>");
                output.Content.AppendHtml("</div></div></div></div>");
                output.TagMode = TagMode.StartTagAndEndTag;
            }
        }

        private string GetBorderCssClass(BorderSize bs)
        {
            switch (bs)
            {
                case BorderSize.None: return "card-no-border";
                case BorderSize.Thick: return "card-thick-border";
                default: return "";
            }
        }
    }

    /// <summary>
    /// Status for the list item
    /// </summary>
    public enum BorderSize
    {
        None = 0,
        Normal = 1,
        Thick = 2
    }
}
