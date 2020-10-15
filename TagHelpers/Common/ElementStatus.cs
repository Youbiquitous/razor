///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Common
{
    /// <summary>
    /// Status for the list item
    /// </summary>
    public enum ElementStatus
    {
        None = 0,
        Success = 1,
        Warning = 2,
        Danger = 3,
        Active = 4,
        Info = 5,
        Disabled = 6,
        InProgress = 7
    }
}