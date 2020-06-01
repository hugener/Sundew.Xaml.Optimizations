﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttachedDependencyPropertyPart.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.BindingPath
{
    using Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.Xaml;

    internal class AttachedDependencyPropertyPart : IPropertyExpression
    {
        public AttachedDependencyPropertyPart(XamlType xamlType, string propertyName)
        {
            this.XamlType = xamlType;
            this.Name = propertyName;
        }

        public XamlType XamlType { get; }

        public string Name { get; }

        public TPartialResult Visit<TParameter, TVariable, TPartialResult, TResult>(IBindingPathVisitor<TParameter, TVariable, TPartialResult, TResult> bindingPathVisitor, TParameter parameter, TVariable variable)
        {
            return bindingPathVisitor.VisitAttachedDependencyPropertyPart(this, parameter, variable);
        }

        public void Visit<TParameter, TVariable, TResult>(IBindingPathWalker<TParameter, TVariable, TResult> bindingPathWalker, TParameter parameter, TVariable variable)
        {
            bindingPathWalker.VisitAttachedDependencyPropertyPart(this, parameter, variable);
        }

        public override string ToString()
        {
            return $"({this.XamlType.TypeName}.{this.Name})";
        }
    }
}