///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Luciano Cornelio (http://youbiquitous.net)
//

using System.Collections.Generic;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Repeater
{
    public class RepeaterContext
    {
        public IList<object> ToRepeat{ get; set; }
    }
}
