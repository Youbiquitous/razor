///////////////////////////////////////////////////////////////////
//
// ISPIRO: app starter template ASP.NET CORE 3.1
// Copyright (c) Youbiquitous srl 2020
//
// Author: Dino Esposito (http://youbiquitous.net)
//

using Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Modal;

namespace Crionet.LiveR.Corinto.App.Common.Razor.TagHelpers.Common
{
    /// <summary>
    /// Internal context class to pass information around the MODAL custom markup tree
    /// </summary>
    public class BootstrapUtils
    {
        public static string BgFrom(ElementStatus status)
        {
            switch (status)
            {
                case ElementStatus.Active:
                    return "bg-primary";
                case ElementStatus.Danger:
                    return "bg-danger";
                case ElementStatus.Success:
                    return "bg-success";
                case ElementStatus.Info:
                    return "bg-info";
                case ElementStatus.Disabled:
                    return "bg-light";
                case ElementStatus.Warning:
                    return "bg-warning";
                case ElementStatus.InProgress:
                    return "bg-primary-outline";
                default:
                    return "";
            }
        }

        public static string KeyFrom(ElementStatus status)
        {
            switch (status)
            {
                case ElementStatus.Active:
                    return "primary";
                case ElementStatus.Danger:
                    return "danger";
                case ElementStatus.Success:
                    return "success";
                case ElementStatus.Info:
                    return "info";
                case ElementStatus.Disabled:
                    return "light";
                case ElementStatus.Warning:
                    return "warning";
                case ElementStatus.InProgress:
                    return "primary-outline";
                default:
                    return "";
            }
        }

        public static string SizeFrom(ElementSize size)
        {
            switch (size)
            {
                case ElementSize.Small:
                    return "sm";
                case ElementSize.Large:
                    return "lg";
                case ElementSize.Extra:
                    return "xl";
                default:
                    return "";
            }
        }
    }
}