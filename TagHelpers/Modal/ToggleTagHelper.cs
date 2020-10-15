///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//

using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Modal
{
    [HtmlTargetElement("toggle", ParentTag = "modal")]
    public class ToggleTagHelper : TagHelper
    {
        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override void Process(
            TagHelperContext context, TagHelperOutput output)
        {
            // Replace <toggle> with <button> 
            output.TagName = "button";
            var modalContext = context.Items[typeof(ModalContext)] as ModalContext;
            if (modalContext == null)
                throw new ArgumentException();
           
            // Prepare output
            output.Attributes.SetAttribute("data-toggle", "modal");
            output.Attributes.SetAttribute("data-target", "#" + modalContext.Id);
        }
    }
}

