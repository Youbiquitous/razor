///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.Forms
{
    /// <summary>
    /// Razor tag helper for element SWITCH
    /// </summary>
    [HtmlTargetElement("switch")]
    public class SwitchTagHelper : TagHelper
    {
        public SwitchTagHelper()
        {
            Label = "Switch";
        }

        /// <summary>
        /// Text for toggle
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Checked by default or not
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// HTML factory
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var id = output.Attributes["id"]?.Value.ToString();
            var name = output.Attributes["name"]?.Value.ToString();
            var css = output.Attributes["class"]?.Value.ToString();
            var checkedState = Checked ? "checked" :null;
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", $"custom-control custom-switch {css}");
            var idMarkup = "";
            var forMarkup = "";
            var nameMarkup = name != null ?$"name='{name}'" :"";
            if (!id.IsNullOrWhitespace())
            {
                idMarkup = $"id='{id}'";
                forMarkup = $"id='{id}'";
            }
            var inputMarkup = $"<input type='checkbox' class='custom-control-input' {idMarkup} {nameMarkup} {checkedState}>";
            var labelMarkup = $"<label class='custom-control-label' {forMarkup}'>{Label}</label>";
            output.Content.AppendHtml(inputMarkup);
            output.Content.AppendHtml(labelMarkup);
        }            
    }
}
