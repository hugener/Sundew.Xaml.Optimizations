// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.ResourceDictionary.Internal
{
    using System.Text.RegularExpressions;

    internal static class Constants
    {
        public const string SourceText = "Source";
        public const string SystemResourceDictionaryMergedDictionariesSystemResourceDictionaryXPath = "//system:ResourceDictionary/system:ResourceDictionary.MergedDictionaries/system:ResourceDictionary";
        public const string UnsharedWpfText = "UnsharedWpf";
        public const string SxoPrefix = "sxo";
        public const string ResourceDictionaryName = "ResourceDictionary";
        public static readonly Regex UriRegex = new Regex("^.+;component/(?:(?<UnsharedWpf>.*Unshared.+)|(?:.+))$");
    }
}
