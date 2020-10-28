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

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.MiniCard
{
    /// <summary>
    /// Razor tag helper for element MINICARD
    /// </summary>
    [HtmlTargetElement("minicard")]
    public class MiniCardTagHelper : TagHelper
    {
        public MiniCardTagHelper()
        {
            Text1 = "";
            Text2 = "";
            Img = "";
            Icon = "";
            Align = Alignment.Middle;
        }

        /// <summary>
        /// First line of the card
        /// </summary>
        public string Text1 { get; set; }

        /// <summary>
        /// CSS for  for images and icons
        /// </summary>
        public string Text1Class { get; set; }

        /// <summary>
        /// Text to display 
        /// </summary>
        public string Text2 { get; set; }

        /// <summary>
        /// CSS class for images and icons
        /// </summary>
        public string Text2Class { get; set; }


        /// <summary>
        /// Alignment of content
        /// </summary>
        public Alignment Align { get; set; }

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
        /// How to position first and second line (where the Title line goes)
        /// </summary>
        //public TitlePosition TitlePosition { get; set; }

        /// <summary>
        /// Justification of image (RTL on the right)
        /// </summary>
        public bool Rtl { get; set; }

        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var css = output.Attributes["class"]?.Value.ToString();
            var id = output.Attributes["id"]?.Value.ToString();

            var align = GetAlignClass();
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", $"d-flex flex-row {align} {css}");

            // Left part
            var visual = $"<img src='{Img}' class='{ImgClass}' />";
            if (Img.IsNullOrWhitespace())
                visual = $"<i class='{Icon}'></i>";

            var textAlign = Rtl ? "text-right" : "";
            var textMargin = Rtl ? "mr-3" : "ml-3";
            var left = $"<div class='{textAlign}'>{visual}</div>";
            var right = $"<div class='{textAlign} {textMargin}'>" + 
                        $"<div class='{Text1Class}' id='{id}-text1'>{Text1}</div>" + 
                        $"<div class='{Text2Class}' id='{id}-text2'>{Text2}</div></div>";
            if (Rtl)
            {
                output.Content.AppendHtml(right);
                output.Content.AppendHtml(left);
            }
            else
            {
                output.Content.AppendHtml(left);
                output.Content.AppendHtml(right);
            }
        }

        private string GetAlignClass()
        {
            return Align switch
            {
                Alignment.Top => "",
                Alignment.Bottom => "align-items-end",
                _ => "align-items-center"
            };
        }
    }

    /// <summary>
    /// Content alignment
    /// </summary>
    public enum Alignment
    {
        Top = 0,
        Middle = 1,
        Bottom = 2
    }

}
