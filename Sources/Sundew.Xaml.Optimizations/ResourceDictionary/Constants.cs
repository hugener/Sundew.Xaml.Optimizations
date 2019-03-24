// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Sundew.Xaml.Optimizations.ResourceDictionary.Internal
{
    internal static class Constants
    {
        public const string SourceText = "Source";
        public const string SystemResourceDictionaryMergedDictionariesSystemResourceDictionaryXPath = "//system:ResourceDictionary/system:ResourceDictionary.MergedDictionaries/system:ResourceDictionary";
        public const string System = "system";
        public const int MaxAttributePosition = 3;
        public const string UnsharedWpfText = "UnsharedWpf";
        public const string ThemeWpfText = "ThemeWpf";
        public const string ResourceDictionary = "ResourceDictionary";
        public static readonly Regex UriRegex = new Regex("^.+;component/(?:(?<ThemeWpf>.*(?:T|t)heme.+)|(?<UnsharedWpf>.*Unshared.+)|(?:.+))$");
        public static readonly string XAttributeName = (XNamespace.Xmlns + "x").ToString();
    }
}
