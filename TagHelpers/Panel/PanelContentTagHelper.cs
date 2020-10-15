///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Panel
{
    [HtmlTargetElement("content", ParentTag = "panel")]
    public class PanelContentTagHelper : TagHelper
    {
        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override async Task ProcessAsync(
            TagHelperContext context, TagHelperOutput output)
        {
            // Retrieve the panel context
            var panelContext = context.Items[typeof(PanelContext)] as PanelContext;
            if (panelContext == null)
                throw new ArgumentException();

            // Evaluate the Razor content of the element's body 
            var body = (await output.GetChildContentAsync()).GetContent();
            body = body.Trim();

            if (panelContext.Collapsable)
            {
                var status = panelContext.CollapseStatus == CollapseStatus.Show ? "show" : "noshow";
                output.TagName = "div";
                output.Attributes.SetAttribute("class", $"collapse {status}");
                output.Attributes.SetAttribute("id", $"{panelContext.Id}-collapse");
                output.Content.Clear();
                output.Content.AppendHtml("<div class=\"card-body\">");
                output.Content.AppendHtml(body);
                output.Content.AppendHtml("</div>");
            }
            else
            {
                // Replace tag name with <div> 
                output.TagName = "div";
                output.Attributes.SetAttribute("class", "card-body");

                output.Content.Clear();
                output.Content.AppendHtml(body);
            }
        }
    }
}

