///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Luciano Cornelio (http://youbiquitous.net)
//

using System.Collections.Generic;
using System.Threading.Tasks;
using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Repeater
{
    [HtmlTargetElement("Repeater")]
    public class RepeaterTagHelper : TagHelper
    {
        public IList<object> Items { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var repeaterContext = new RepeaterContext
            {
                ToRepeat = Items
            };
            context.Items[typeof(RepeaterContext)] = repeaterContext;

            var css = output.Attributes["class"]?.Value.ToString();

            output.TagName = "div";
            output.Attributes.SetAttribute("class", css);
            var internalItems = (await output.GetChildContentAsync()).GetContent();
            if (!internalItems.IsNullOrWhitespace())
                output.Content.AppendHtml(internalItems);
        }
    }
}
