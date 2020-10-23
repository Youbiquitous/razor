///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Modal
{
    [HtmlTargetElement("modal")]
    public class ModalTagHelper : TagHelper
    {
        public ModalTagHelper()
        {
            Ok = false;
            Fade = false;
            Size = ElementSize.Auto;
            Scrollable = true;
            Centered = false;
            Dismissable = true;
        }

        /// <summary>
        /// Whether it can be dismissed (if false, no X button on top)
        /// </summary>
        public bool Dismissable { get; set; }

        /// <summary>
        /// Whether to add an OK button
        /// </summary>
        public bool Ok { get; set; }

        /// <summary>
        /// Whether to apply the fade animation
        /// </summary>
        public bool Fade { get; set; }

        /// <summary>
        /// Whether the modal should be centered in the screen
        /// </summary>
        public bool Centered { get; set; }

        /// <summary>
        /// Whether the modal should include a scrollbar
        /// </summary>
        public bool Scrollable { get; set; }

        /// <summary>
        /// Desired size of the modal
        /// </summary>
        public ElementSize Size { get; set; }

        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override void Process(
            TagHelperContext context, TagHelperOutput output)
        {
            // Create the context for child elements
            var modalContext = new ModalContext
            {
                Id = output.Attributes["id"]?.Value.ToString(),
                OkButton = Ok,
                Fade = Fade,
                Centered = Centered,
                Scrollable = Scrollable,
                Size = Size
            };
            context.Items[typeof(ModalContext)] = modalContext;

            // Replace <modal> with <div> 
            output.TagName = "div";
            if (output.Attributes.ContainsName("id"))
                output.Attributes.Remove(context.AllAttributes["id"]);
            //if (output.Attributes.ContainsName("ok"))
            //    output.Attributes.Remove(context.AllAttributes["ok"]);
            //if (output.Attributes.ContainsName("fade"))
            //    output.Attributes.Remove(context.AllAttributes["fade"]);
        }
    }
}

