using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficialsTypeAhead.Common.TagHelpers
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

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string src = output.Attributes["src"]?.Value.ToString();
            string onerror = "";
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
