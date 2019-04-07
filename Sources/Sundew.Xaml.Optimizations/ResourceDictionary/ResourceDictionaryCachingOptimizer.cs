// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceDictionaryCachingOptimizer.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.ResourceDictionary
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Sundew.Base.Computation;
    using Sundew.Xaml.Optimization;
    using Sundew.Xaml.Optimization.Xml;
    using Sundew.Xaml.Optimizations.ResourceDictionary.Internal;

    /// <summary>
    /// Optimizes resource dictionaries using an <see cref="XDocument"/>.
    /// </summary>
    public class ResourceDictionaryCachingOptimizer : IXamlOptimizer
    {
        private readonly XamlPlatformInfo xamlPlatformInfo;
        private readonly XName sundewXamlResourceDictionaryName;

        /// <summary>Initializes a new instance of the <see cref="ResourceDictionaryCachingOptimizer"/> class.</summary>
        /// <param name="xamlPlatformInfo">The framework XML definitions.</param>
        public ResourceDictionaryCachingOptimizer(XamlPlatformInfo xamlPlatformInfo)
        {
            this.xamlPlatformInfo = xamlPlatformInfo;
            this.sundewXamlResourceDictionaryName = xamlPlatformInfo.SundewXamlOptimizationsNamespace + Constants.ResourceDictionaryName;
        }

        /// <summary>Gets the supported platforms.</summary>
        /// <value>The supported platforms.</value>
        public IReadOnlyList<XamlPlatform> SupportedPlatforms { get; } = new List<XamlPlatform> { XamlPlatform.WPF };

        /// <summary>Optimizes the xml document.</summary>
        /// <param name="xDocument">The xml document.</param>
        /// <param name="fileInfo">The file info.</param>
        /// <param name="intermediateDirectory">The intermediate directory.</param>
        /// <param name="assemblyReferences">The assembly references.</param>
        /// <returns>A result with the optimized <see cref="XDocument"/>, if successful.</returns>
        public Result<XamlOptimization> Optimize(XDocument xDocument, FileInfo fileInfo, DirectoryInfo intermediateDirectory, IReadOnlyList<IAssemblyReference> assemblyReferences)
        {
            var mergedResourceDictionaries = xDocument.XPathSelectElements(
                Constants.SystemResourceDictionaryMergedDictionariesSystemResourceDictionaryXPath,
                this.xamlPlatformInfo.XmlNamespaceResolver);
            var hasBeenOptimized = false;
            var hasSxoNamespace = false;
            foreach (var xElement in mergedResourceDictionaries.ToList())
            {
                var optimization = OptimizationProvider.GetOptimizationInfo(
                    xElement,
                    this.xamlPlatformInfo.SystemResourceDictionaryName);
                switch (optimization.OptimizationMode)
                {
                    case OptimizationMode.Shared:
                        if (!hasSxoNamespace)
                        {
                            xDocument.Root.EnsureXmlNamespaceAttribute(
                                this.xamlPlatformInfo.SundewXamlOptimizationsNamespace,
                                Constants.SxPrefix,
                                this.xamlPlatformInfo.DefaultInsertAfterNamespaces);
                            hasSxoNamespace = true;
                        }

                        hasBeenOptimized = true;
                        xElement.ReplaceWith(new XElement(
                            this.sundewXamlResourceDictionaryName,
                            new XAttribute(Constants.SourceText, optimization.Source)));
                        break;
                }
            }

            return Result.From(hasBeenOptimized, new XamlOptimization(xDocument));
        }
    }
}