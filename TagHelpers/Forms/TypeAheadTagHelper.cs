///////////////////////////////////////////////////////////////////
//
// Live-R Corinto: Tournament manager app
// Copyright (c) Crionet 2020-2026
//
// Author: Dino Esposito (http://youbiquitous.net)
//

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
        /// controller/method?o=%QUERY
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// True ---> second input type="text" ----- False ---> second input type = "hidden"
        /// </summary>
        public bool Debug { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string css = output.Attributes["class"]?.Value.ToString();
            string id = output.Attributes["id"]?.Value.ToString();
            string name = output.Attributes["name"]?.Value.ToString();
            string placeholder = output.Attributes["placeholder"]?.Value.ToString();
            string debug = Debug == true ? "text" : "hidden";
            string firstInput = $"<input type=\"text\" class=\"{css}\" id=\"{id}\" name=\"{name}\" placeholder=\"{placeholder}\">";
            string secondInput = $"<input type=\"{debug}\" id=\"{id}-id\" name=\"{name}ID\" />";
            string script = "<script> new TypeAheadContainer(" + "{" + $"targetSelector: \"#{id}\",buddySelector: \"#{id}-id\",maxNumberOfHints: {MaxHints},hintUrl: Ybq.fromServer(\"{Url}\")" + "}" + ").attach();</script>";
            output.TagName = "div";
            output.Content.AppendHtml(firstInput);
            output.Content.AppendHtml(secondInput);
            output.Content.AppendHtml(script);
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Clear();
        }
    }
}
