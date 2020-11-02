///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Markup
{
    /// <summary>
    /// Razor tag helper for image
    /// </summary>
    [HtmlTargetElement("img")]
    public class ImgTagHelper : TagHelper
    {
        public ImgTagHelper()
        {
            HideOnError = true;
            Fallback = "";
        }

        /// <summary>
        /// Second image's url
        /// </summary>
        public string Fallback { get; set; }

        /// <summary>
        /// Set "false" if  you want to see the error on display
        /// </summary>
        public bool HideOnError { get; set; }

        /// <summary>
        /// Internal HTML factory
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var src = output.Attributes["src"]?.Value.ToString();
            var onerror = "";
            if (!string.IsNullOrEmpty(Fallback))
            {
                var varText = "this.style=\"display:none\";";
                onerror = $"this.onerror = function(){{{varText}}}; this.src=\"{Fallback}\";";
            }
            else if (HideOnError)
            {
                onerror = "this.style=\"display:none\";";
            }
            else
            {
                onerror = "";
            }
            output.TagName = "img";
            output.Attributes.SetAttribute("src", src);
            output.Attributes.SetAttribute("onerror", onerror);
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
