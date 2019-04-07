﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FreezeResourceSettings.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Freezing
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Newtonsoft.Json;

    /// <summary>Settings for <see cref="FreezeResourceOptimizer"/>.</summary>
    public class FreezeResourceSettings
    {
        /// <summary>Initializes a new instance of the <see cref="FreezeResourceSettings"/> class.</summary>
        /// <param name="includeFrameworkTypes">if set to <c>true</c> [include framework types].</param>
        /// <param name="includedTypes">The included types.</param>
        /// <param name="excludedTypes">The excluded types.</param>
        public FreezeResourceSettings(bool includeFrameworkTypes = true, IReadOnlyList<string> includedTypes = null, IReadOnlyList<string> excludedTypes = null)
        {
            this.IncludeFrameworkTypes = includeFrameworkTypes;
            this.IncludedTypes = includedTypes ?? new List<string>();
            this.ExcludedTypes = excludedTypes ?? new List<string>();
        }

        /// <summary>Gets a value indicating whether framework types are included per default.</summary>
        /// <value>
        ///   <c>true</c> if [include framework types]; otherwise, <c>false</c>.</value>
        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool IncludeFrameworkTypes { get; }

        /// <summary>Gets the included classes.</summary>
        /// <value>The included.</value>
        public IReadOnlyList<string> IncludedTypes { get; }

        /// <summary>Gets the excluded classes.</summary>
        /// <value>The exclude.</value>
        public IReadOnlyList<string> ExcludedTypes { get; }
    }
}