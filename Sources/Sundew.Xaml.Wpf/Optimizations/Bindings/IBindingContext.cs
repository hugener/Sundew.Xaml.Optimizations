// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBindingContext.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings
{
    /// <summary>Interface for implementing a data context.</summary>
    /// <typeparam name="TRoot">The type of the root.</typeparam>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    public interface IBindingContext<out TRoot, out TSource> : ISourceContext<TSource>
    {
        /// <summary>Gets the root.</summary>
        /// <value>The root.</value>
        TRoot Root { get; }
    }
}