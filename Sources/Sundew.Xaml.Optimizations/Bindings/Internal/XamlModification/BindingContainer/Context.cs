// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Context.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.XamlModification.BindingContainer
{
    using System.Collections.Generic;
    using Sundew.Xaml.Optimizations.Bindings.Internal.CodeAnalysis;
    using Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.Xaml;
    using Sundew.Xaml.Optimizations.Bindings.Internal.Xaml;

    internal readonly struct Context
    {
        public Context(
            XamlElementNameProvider xamlElementNameProvider,
            List<XamlModificationInfo> xamlModificationInfos = null,
            Dictionary<BindingRootNode, QualifiedType> bindingRootTypes = null,
            XamlModificationTracker xamlModificationTracker = null)
        {
            this.XamlElementNameProvider = xamlElementNameProvider;
            this.XamlModificationInfos = xamlModificationInfos ?? new List<XamlModificationInfo>();
            this.BindingRootTypes = bindingRootTypes ?? new Dictionary<BindingRootNode, QualifiedType>();
            this.XamlModificationTracker = xamlModificationTracker;
        }

        public Context(XamlModificationTracker xamlModificationTracker, Context context)
            : this(context.XamlElementNameProvider, context.XamlModificationInfos, context.BindingRootTypes, xamlModificationTracker)
        {
        }

        public List<XamlModificationInfo> XamlModificationInfos { get; }

        public Dictionary<BindingRootNode, QualifiedType> BindingRootTypes { get; }

        public XamlElementNameProvider XamlElementNameProvider { get; }

        public XamlModificationTracker XamlModificationTracker { get; }
    }
}