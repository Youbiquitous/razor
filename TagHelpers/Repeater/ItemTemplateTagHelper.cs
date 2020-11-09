///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Luciano Cornelio (http://youbiquitous.net)
//

using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Repeater
{
    [HtmlTargetElement("itemtemplate", ParentTag = "Repeater")]
    public class ItemTemplateTagHelper : TagHelper 
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var repeaterContext = context.Items[typeof(RepeaterContext)] as RepeaterContext;
            var code = (await output.GetChildContentAsync()).GetContent();

            // Get each substring between {{ and }}
            var matches = EverythingBetween(code, "{{", "}}");

            output.Content.AppendHtml("<div>");
            foreach (var item in repeaterContext.ToRepeat)
            {
                var outputCode = code;
                foreach (var match in matches)
                {
                    var obj = item.GetType().GetProperty(match).GetValue(item, null);
                    outputCode = outputCode.Replace("{{" + match + "}}", obj.ToString());
                }
                output.Content.AppendHtml(outputCode);
            }
            output.Content.AppendHtml("</div>");
        }

        public static List<string> EverythingBetween(string source, string start, string end)
        {
            var matches = new List<string>();

            var pattern = $"{Regex.Escape(start)}({".+?"}){Regex.Escape(end)}";

            foreach (Match m in Regex.Matches(source, pattern))
            {
                matches.Add(m.Groups[1].Value);
            }

            return matches;
        }
    }
}
