// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBindingSource.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.Xaml
{
    using System.Collections.Generic;

    internal interface IBindingSource : IBinding
    {
        IReadOnlyList<IBinding> Bindings { get; }
    }
}