// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingPathParserException.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.BindingPath
{
    using System;
    using Sundew.Base;
    using Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.BindingPath.LexicalAnalysis;

    internal class BindingPathParserException : Exception
    {
        public BindingPathParserException(BindingPathError bindingPathError, Lexeme<ˍ> lexeme)
        : base($"Error: {bindingPathError} when parsing {lexeme}")
        {
            this.BindingPathError = bindingPathError;
            this.Lexeme = lexeme;
        }

        public BindingPathError BindingPathError { get; }

        public Lexeme<ˍ> Lexeme { get; }
    }
}