// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAccessorCodeGenerator.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.CodeGenerators.BindingPath
{
    using Sundew.Base.Computation;
    using Sundew.Xaml.Optimizations.Bindings.Internal.CodeAnalysis;

    internal interface IAccessorCodeGenerator
    {
        string Name { get; }

        QualifiedProperty Accessor { get; }

        Result<BindingSource> GetBindingSource(bool acceptsSharedSource);

        string GetAccessorGetter();

        string GetAccessorSetter();
    }
}