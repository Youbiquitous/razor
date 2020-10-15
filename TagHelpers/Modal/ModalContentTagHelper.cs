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

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Modal
{
    [HtmlTargetElement("content", ParentTag = "modal")]
    public class ModalContentTagHelper : TagHelper
    {
        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override async Task ProcessAsync(
            TagHelperContext context, TagHelperOutput output)
        {
            // Evaluate the Razor content of the element's body 
            var body = (await output.GetChildContentAsync()).GetContent();
            body = body.Trim();

            // Replace <toggle> with <button> 
            output.TagName = "div";
            var modalContext = context.Items[typeof(ModalContext)] as ModalContext;
            if (modalContext == null)
                throw new ArgumentException();

            // Prepare output
            var modalCss = $"modal {(modalContext.Fade ?"fade" :"")}";
            output.Attributes.SetAttribute("id", modalContext.Id);
            output.Attributes.SetAttribute("class", modalCss);
            output.Attributes.SetAttribute("tabindex", "-1");
            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("aria-labelledby", $"{modalContext.Id}-header");
            output.Attributes.SetAttribute("aria-hidden", "true");
            output.Content.AppendHtml("<div class='modal-dialog' role='document'><div class='modal-content'>");
            output.Content.AppendHtml(body);
            output.Content.AppendHtml("</div></div>");
        }
    }
}

