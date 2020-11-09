///////////////////////////////////////////////////////////////////
//
// Live-R Corinto: Tournament manager app
// Copyright (c) Crionet 2020-2026
//
// Author: Dino Esposito (http://youbiquitous.net)
//

using Expoware.Youbiquitous.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Forms
{
    /// <summary>
    /// Razor tag helper for Type Ahead in input form
    /// </summary>
    [HtmlTargetElement("typeahead")]
    public class TypeAheadTagHelper : TagHelper
    {
        /// <summary>
        /// Max number of hints
        /// </summary>
        public int MaxHints { get; set; }

        /// <summary>
        /// Value attribute for the hidden field
        /// </summary>
        public string ValueId { get; set; }

        /// <summary>
        /// Name attribute for the hidden field
        /// </summary>
        public string NameId { get; set; }

        /// <summary>
        /// controller/method?o=%QUERY
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// True ---> second input type="text" ----- False ---> second input type = "hidden"
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// Auto-click
        /// </summary>
        public string SubmitSelector { get; set; }

        /// <summary>
        /// HTML factory
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var autoPost = SubmitSelector.IsNullOrWhitespace() ?"" :$"postOnSelection: true, submitSelector:'{SubmitSelector}',";

            var displayValue = output.Attributes["value"]?.Value.ToString();
            var style = output.Attributes["style"]?.Value.ToString();
            var css = output.Attributes["class"]?.Value.ToString();
            var id = output.Attributes["id"]?.Value.ToString();
            var name = output.Attributes["name"]?.Value.ToString();
            var placeholder = output.Attributes["placeholder"]?.Value.ToString();
            var debug = Debug ? "text" : "hidden";
            var firstInput = $"<input type='text' style='{style}' class='{css}' id='{id}' name='{name}' placeholder='{placeholder}' value='{displayValue}'>";
            var secondInput = $"<input type='{debug}' id='{id}-id' name='{NameId}' value='{ValueId}'>";
            var script = "<script> new TypeAheadContainer({" + 
                            $"targetSelector: '#{id}',buddySelector: '#{id}-id',maxNumberOfHints: {MaxHints}, {autoPost} hintUrl: Ybq.fromServer('{Url}')" + 
                            "}).attach();</script>";
            
            output.TagName = "div";
            output.Content.AppendHtml(firstInput);
            output.Content.AppendHtml(secondInput);
            output.Content.AppendHtml(script);
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Clear();
        }
    }
}
