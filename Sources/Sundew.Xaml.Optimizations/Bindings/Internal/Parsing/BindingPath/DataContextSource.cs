﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataContextSource.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.BindingPath
{
    internal class DataContextSource : IBindingPathExpression
    {
        private const string Dot = ".";

        public TPartialResult Visit<TParameter, TVariable, TPartialResult, TResult>(IBindingPathVisitor<TParameter, TVariable, TPartialResult, TResult> bindingPathVisitor, TParameter parameter, TVariable variable)
        {
            return bindingPathVisitor.VisitDataContextSource(this, parameter, variable);
        }

        public void Visit<TParameter, TVariable, TResult>(IBindingPathWalker<TParameter, TVariable, TResult> bindingPathWalker, TParameter parameter, TVariable variable)
        {
            bindingPathWalker.VisitDataContextSource(this, parameter, variable);
        }

        public override string ToString()
        {
            return Dot;
        }
    }
}