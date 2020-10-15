///////////////////////////////////////////////////////////////////
//
// Live-R Corinto: Tournament manager app
// Copyright (c) Crionet 2020-2026
//
// Author: Dino Esposito (http://youbiquitous.net)
//


using System.Threading.Tasks;
using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Email
{
    /// <summary>
    /// Razor tag helper for element EMAIL
    /// </summary>
    [HtmlTargetElement("email")]
    public class EmailTagHelper : TagHelper
    {
        public EmailTagHelper()
        {
            Subject = "";
            Body = "";
            To = "";
            Css = "";
        }

        /// <summary>
        /// Determines the email address to be set in the mail window
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Determines the text to be set in the subject field of the email window
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Determines the text to be set in the body of the email window
        /// </summary>
        public string Body { get; set; }
        
        /// <summary>
        /// Determines the CSS classes to be added in the MAILTO link anchor element
        /// </summary>
        public string Css { get; set; }


        /// <summary>
        /// Internal HTML factory 
        /// </summary>
        /// <param name="context">Custom markup tree</param>
        /// <param name="output">HTML final tree</param>
        public override async Task ProcessAsync(
            TagHelperContext context, TagHelperOutput output)
        {
            // Evaluate the Razor content of the email's element body 
            var linkText = (await output.GetChildContentAsync()).GetContent();
            if (linkText.IsNullOrWhitespace())
                linkText = "<i class='fas fa-2x fa-envelope'></i>";

            // Replace <email> with <a> 
            output.TagName = "a";

            // Prepare mailto URL
            var mailto = "mailto:" + To;
            if (!string.IsNullOrWhiteSpace(Subject))
                mailto = $"{mailto}?subject={Subject}&body={Body}";

            // Prepare output
            if (output.Attributes.ContainsName("to"))
                output.Attributes.Remove(context.AllAttributes["to"]);
            if (output.Attributes.ContainsName("subject"))
                output.Attributes.Remove(context.AllAttributes["subject"]);
            if (output.Attributes.ContainsName("body"))
                output.Attributes.Remove(context.AllAttributes["body"]);
            output.Attributes.SetAttribute("href", mailto);
            output.Attributes.SetAttribute("class", Css);
            output.Content.Clear();
            output.Content.AppendHtml(linkText);
        }
    }
}

