///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using System;
using System.Drawing;
using System.Threading.Tasks;
using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Panel
{
    [HtmlTargetElement("header", ParentTag = "panel")]
    [HtmlTargetElement("title", ParentTag = "header")]
    [HtmlTargetElement("menu", ParentTag = "header")]
    public class PanelHeaderTagHelper : TagHelper
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


            if (context.TagName.EqualsAny("header"))
            {
                if (panelContext.Collapsable)
                {
                    var collapseId = $"{panelContext.Id}-collapse";
                    output.TagName = "a";
                    output.Attributes.SetAttribute("class", "d-block card-header py-3");
                    output.Attributes.SetAttribute("href", $"#{collapseId}");
                    output.Attributes.SetAttribute("role", "button");
                    output.Attributes.SetAttribute("data-toggle", "collapse");
                }
                else
                {
                    output.TagName = "div";
                    output.Attributes.SetAttribute("class", "card-header py-3 d-flex flex-row align-items-center justify-content-between");
                }
            }

            if (context.TagName.EqualsAny("title"))
            {
                // Evaluate the Razor content of the element's body 
                var body = (await output.GetChildContentAsync()).GetContent();
                body = body.Trim();

                output.TagName = "h6";
                output.Attributes.SetAttribute("class", "p-0 m-0 font-weight-bold text-primary");
                output.Content.Clear();
                if (!panelContext.Icon.IsNullOrWhitespace())
                    body = $"<i class='{panelContext.Icon} mr-2'></i>" + body;
                output.Content.AppendHtml(body); 
            }

            if (context.TagName.EqualsAny("menu") && !panelContext.Collapsable)
            {
                // Evaluate the Razor content of the element's body 
                var body = (await output.GetChildContentAsync()).GetContent();
                body = body.Trim();
                var id = panelContext.Id;
                output.TagName = "div";
                output.Attributes.SetAttribute("class", "dropdown no-arrow");
                output.Content.Clear();
                output.Content.AppendHtml($"<a class=\"dropdown-toggle\" href=\"#\" role=\"button\" id=\"{id}\" data-toggle=\"dropdown\">");
                output.Content.AppendHtml("<i class=\"fas fa-ellipsis-v fa-sm fa-fw text-gray-400\"></i>");
                output.Content.AppendHtml("</a>");
                output.Content.AppendHtml($"<div class=\"dropdown-menu dropdown-menu-right shadow animated--fade-in\" aria-labelledby=\"{id}\">");
                output.Content.AppendHtml(body); 
                output.Content.AppendHtml("</div>");
            }
        }
    }
}

