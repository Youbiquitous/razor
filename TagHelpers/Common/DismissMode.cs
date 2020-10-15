///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//

using System;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Common
{
    /// <summary>
    /// How to dismiss dialog boxes
    /// </summary>
    [Flags]
    public enum DismissMode
    {
        None = 0,
        Button = 1,
        ClickInside = 2,
    }
}