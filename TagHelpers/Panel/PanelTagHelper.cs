///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Panel
{
    [HtmlTargetElement("panel")]
    public class PanelTagHelper : TagHelper
    {
        public PanelTagHelper()
        {
            Collapsable = false;
            CollapseStatus = CollapseStatus.Show;
        }

        /// <summary>
        /// Whether the panel has collapsible content
        /// </summary>
        public bool Collapsable { get; set; }

        /// <summary>
        /// Initial status of the content
        /// </summary>
        public CollapseStatus CollapseStatus { get; set; }

        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override void Process(
            TagHelperContext context, TagHelperOutput output)
        {
            // Grab ID attribute
            var id = output.Attributes["id"]?.Value.ToString() ?? "panel1";

            // Create the context for child elements
            var panelContext = new PanelContext
            {
                Id = id,
                Collapsable = Collapsable,
                CollapseStatus = CollapseStatus
            };
            context.Items[typeof(PanelContext)] = panelContext;


            // Replace <modal> with <div> 
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "card shadow mb-4");
        }
    }
}

