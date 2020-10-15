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
    [HtmlTargetElement("title", ParentTag = "content")]
    [HtmlTargetElement("footer", ParentTag = "content")]
    [HtmlTargetElement("info", ParentTag = "content")]
    public class ModalContentItemTagHelper : TagHelper
    {
        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override async Task ProcessAsync(
            TagHelperContext context, TagHelperOutput output)
        {
            var modalContext = context.Items[typeof(ModalContext)] as ModalContext;
            if (modalContext == null)
                throw new ArgumentException();

            // Evaluate the Razor content of the element's body 
            var body = (await output.GetChildContentAsync()).GetContent();
            body = body.Trim();

            // Prepare output
            var originalTagName = output.TagName;
            var className = GetBootstrapClassFromTagName(originalTagName);
            output.TagName = "div";
            output.Attributes.SetAttribute("class", className);

            // Add X button 
            if (string.Equals(originalTagName, ModalContext.HeaderTag, StringComparison.InvariantCultureIgnoreCase))
            {
                var titleId = $"{modalContext.Id}-header";
                var button = "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">&times;</button>";
                output.Content.AppendHtml("<h5 class='modal-title' id='" + titleId + "'>");
                output.Content.AppendHtml(body);
                output.Content.AppendHtml("</h5>");
                output.Content.AppendHtml(button);
            }

            // Add OK button by default
            if (modalContext.OkButton)
            {
                
                if (string.Equals(originalTagName, ModalContext.FooterTag, StringComparison.InvariantCultureIgnoreCase))
                {
                    var button = "<button type=\"button\" class=\"btn btn-primary\" data-dismiss=\"modal\">OK</button>";
                    output.Content.AppendHtml(body);
                    output.Content.AppendHtml(button);
                }
            }
        }

        /// <summary>
        /// Internal method mapping styles to tag names
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        private static string GetBootstrapClassFromTagName(string tagName)
        {
            if (string.Equals(tagName, ModalContext.HeaderTag, StringComparison.InvariantCultureIgnoreCase))
                return "modal-header";
            if (string.Equals(tagName, ModalContext.FooterTag, StringComparison.InvariantCultureIgnoreCase))
                return "modal-footer";
            if (string.Equals(tagName, ModalContext.BodyTag, StringComparison.InvariantCultureIgnoreCase))
                return "modal-body";
            return "";
        }
    }
}

