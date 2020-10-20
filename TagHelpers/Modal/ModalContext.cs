///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Modal
{
    /// <summary>
    /// Internal context class to pass information around the MODAL custom markup tree
    /// </summary>
    public class ModalContext
    {
        public const string HeaderTag = "title";
        public const string BodyTag = "info";
        public const string FooterTag = "footer";

        public string Id { get; set; }
        public bool OkButton { get; set; }
        public bool Fade { get; set; }
        public bool Centered { get; set; }
        public bool Scrollable { get; set; }
        public ElementSize Size { get; set; }
    }
}