// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptimizationMode.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.ResourceDictionary.Internal
{
    /// <summary>
    /// Determines how resource dictionaries should be optimized.
    /// </summary>
    internal enum OptimizationMode
    {
        /// <summary>
        /// Indicates no optimization should be applied.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that shared resource dictionary optimization should be applied.
        /// </summary>
        Shared,
    }
}