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
    /// Razor tag helper for upload file in input form
    /// </summary>
    [HtmlTargetElement("fileupload")]
    public class FileUploadTagHelper : TagHelper
    {
        public FileUploadTagHelper()
        {
            Accept = "image/jpeg, image/png";
            SizeErrorMsg = "Image too big";
            MaxHeight = "100px";
            RemoverClass = "btn btn-xs btn-danger";
            RemoverIcon = "fa fa-times";
            Placeholder = "<i class='fal fa-2x text-muted fa-file-image'></i>";
        }

        /// <summary>
        /// The initializer image url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// FontAwesome icon class for remove file button
        /// </summary>
        public string RemoverIcon { get; set; }

        /// <summary>
        /// Style of the remover button 
        /// </summary>
        public string RemoverClass { get; set; }

        /// <summary>
        /// Message if image's size is not accepted
        /// </summary>
        public string SizeErrorMsg { get; set; }

        /// <summary>
        /// Max height in pixel for images to preview (100px)
        /// </summary>
        public string MaxHeight { get; set; }

        /// <summary>
        /// Image formats accepted
        /// </summary>
        public string Accept { get; set; }

        /// <summary>
        /// Placeholder when no image is selected
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// HTML factory
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var id = output.Attributes["id"]?.Value.ToString();
            var isdefined = Url.IsNullOrWhitespace() ? "false" : "true";
            var firstInput = $"<input type='file' class='form-control' data-size-error=\"{SizeErrorMsg} <b> >2MB</b>.<br><span class='text-danger'>($)</span>\" accept='{Accept}' id='{id}' name='{id}'>";
            var secondInput = $"<input type='hidden' id='{id}-isdefined' name='{id}isdefined' value='{isdefined}' />";
            var img = $"<img id='{id}-preview' style='max-height: {MaxHeight}' src='{Url}' onerror='__imgLoadError(this)' />";
            var placeholder = $"<div class='ybq-inputfile-placeholder' id='{id}-placeholder'>{Placeholder}</div>";
            var thirdblock = $"<div class='float-left'>{img}{placeholder}</div>";
            var fourthblock = $"<div class='float-left ybq-inputfile-toolbar'><button type='button' class='{RemoverClass}' id='{id}-remover'><i class='{RemoverIcon}'></i></button></div><div class='clearfix'></div>";
            output.Content.AppendHtml("<div class='ybq-inputfile'>");
            output.Content.AppendHtml(firstInput);
            output.Content.AppendHtml(secondInput);
            output.Content.AppendHtml(thirdblock);
            output.Content.AppendHtml(fourthblock);
            output.Content.AppendHtml("</div>");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
