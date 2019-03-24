// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.ResourceDictionary.Internal
{
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    internal static class Constants
    {
        public const string SourceText = "Source";
        public const string SystemResourceDictionaryMergedDictionariesSystemResourceDictionaryXPath = "//system:ResourceDictionary/system:ResourceDictionary.MergedDictionaries/system:ResourceDictionary";
        public const int MaxAttributePosition = 3;
        public const string UnsharedWpfText = "UnsharedWpf";
        public const string ResourceDictionary = "ResourceDictionary";
        public const string Sxo = "sxo";
        public static readonly Regex UriRegex = new Regex("^.+;component/(?:(?<UnsharedWpf>.*Unshared.+)|(?:.+))$");
        public static readonly string XAttributeName = (XNamespace.Xmlns + "x").ToString();
        public static readonly XName SxoAttributeName = XNamespace.Xmlns + Sxo;
    }
}
