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
            var globalModalCss = $"modal {(modalContext.Fade ?"fade" :"")}";
            output.Attributes.SetAttribute("id", modalContext.Id);
            output.Attributes.SetAttribute("class", globalModalCss);
            output.Attributes.SetAttribute("tabindex", "-1");
            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("aria-labelledby", $"{modalContext.Id}-header");
            output.Attributes.SetAttribute("aria-hidden", "true");
            if (!modalContext.Dismissable)
            {
                output.Attributes.SetAttribute("data-keyboard", "false");
                //output.Attributes.SetAttribute("data-backdrop", "static");
            }

            var centered = modalContext.Centered ?"modal-dialog-centered" :"";
            var scrollable = modalContext.Scrollable ? "modal-dialog-scrollable" : "";
            var size = modalContext.Size == ElementSize.Auto ?"" : $"modal-{BootstrapUtils.SizeFrom(modalContext.Size)}";

            // Additional modal styles
            var extraModalCss = $"{centered} {scrollable} {size}".Trim();
            output.Content.AppendHtml($"<div class='modal-dialog {extraModalCss}' role='document'><div class='modal-content'>");
            output.Content.AppendHtml(body);
            output.Content.AppendHtml("</div></div>");
        }
    }
}

