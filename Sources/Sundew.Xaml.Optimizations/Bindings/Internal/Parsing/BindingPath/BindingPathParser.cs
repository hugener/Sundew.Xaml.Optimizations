// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingPathParser.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.BindingPath
{
    using System;
    using System.Collections.Generic;
    using Sundew.Base;
    using Sundew.Base.Computation;
    using Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.BindingPath.LexicalAnalysis;
    using Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.Xaml;

    internal class BindingPathParser
    {
        private readonly BindingPathLexicalAnalyzer bindingPathLexicalAnalyzer;

        public BindingPathParser(BindingPathLexicalAnalyzer bindingPathLexicalAnalyzer)
        {
            this.bindingPathLexicalAnalyzer = bindingPathLexicalAnalyzer;
        }

        public Result<IBindingPathExpression, BindingPathError<ˍ>> Parse(string bindingPath)
        {
            try
            {
                var lexemesResult = this.bindingPathLexicalAnalyzer.Analyze(bindingPath);
                var lexemes = lexemesResult.Value;
                var bindingPathExpression = this.TryBindingPath(lexemes);
                if (lexemes.AcceptTokenType(TokenInfo.End))
                {
                    if (bindingPathExpression == null)
                    {
                        bindingPathExpression = new DataContextSource();
                    }

                    return Result.Success(bindingPathExpression);
                }

                return Result.Error(new BindingPathError<ˍ>(BindingPathError.EndMissing, lexemes.Current));
            }
            catch (BindingPathParserException e)
            {
                return Result.Error(new BindingPathError<ˍ>(e.BindingPathError, e.Lexeme));
            }
        }

        private static Exception CreateParseException(BindingPathError bindingPathError, Lexeme<ˍ> lexeme)
        {
            throw new BindingPathParserException(bindingPathError, lexeme);
        }

        private IBindingPathExpression TryBindingPath(Lexemes<ˍ> lexemes)
        {
            var lhs = this.PrimaryExpression(lexemes);
            return this.BindingPath(lexemes, lhs);
        }

        private IBindingPathExpression BindingPath(Lexemes<ˍ> lexemes, IBindingPathExpression lhs)
        {
            lhs = this.IndexerAccessor(lexemes, lhs);
            lhs = this.PropertyAccessor(lexemes, lhs);

            return lhs;
        }

        private IBindingPathExpression PrimaryExpression(Lexemes<ˍ> lexemes)
        {
            if (lexemes.AcceptToken("."))
            {
                return new DataContextSource();
            }

            var indexerResult = this.Indexer(lexemes);
            if (indexerResult != null)
            {
                return indexerResult;
            }

            var dependencyPropertyResult = this.AttachedDependencyProperty(lexemes);
            if (dependencyPropertyResult != null)
            {
                return dependencyPropertyResult;
            }

            return this.Property(lexemes, false);
        }

        private IBindingPathExpression PropertyAccessor(Lexemes<ˍ> lexemes, IBindingPathExpression lhs)
        {
            if (lexemes.AcceptToken("."))
            {
                var result = this.AttachedDependencyProperty(lexemes);
                if (result == null)
                {
                    result = this.Property(lexemes, true);
                }

                return this.BindingPath(lexemes, new PropertyAccessor(lhs, result));
            }

            return lhs;
        }

        private IPropertyExpression AttachedDependencyProperty(Lexemes<ˍ> lexemes)
        {
            if (lexemes.AcceptToken("("))
            {
                var xamlType = this.XamlType(lexemes);
                lexemes.AcceptToken(".");
                lexemes.AcceptTokenType(ˍ._, out var propertyName);
                if (lexemes.AcceptToken(")"))
                {
                    return new AttachedDependencyProperty(xamlType, propertyName);
                }
            }

            return null;
        }

        private XamlType XamlType(Lexemes<ˍ> lexemes)
        {
            var namespacePrefix = string.Empty;
            lexemes.AcceptTokenType(ˍ._, out var identifier);
            if (lexemes.AcceptToken(":"))
            {
                namespacePrefix = identifier;
                lexemes.AcceptTokenType(ˍ._, out identifier);
            }

            return new XamlType(namespacePrefix, identifier);
        }

        private IBindingPathExpression IndexerAccessor(Lexemes<ˍ> lexemes, IBindingPathExpression lhs)
        {
            var indexerResult = this.Indexer(lexemes);
            if (indexerResult != null)
            {
                return new IndexerAccessor(lhs, indexerResult);
            }

            return lhs;
        }

        private IIndexerExpression Indexer(Lexemes<ˍ> lexemes)
        {
            if (lexemes.AcceptToken("["))
            {
                var literalList = new List<Literal>();
                this.LiteralList(lexemes, literalList);
                if (lexemes.AcceptToken("]"))
                {
                    if (lexemes.AcceptTokenType(TokenInfo.End))
                    {
                        lexemes.MoveToPrevious();
                        return new Indexer(literalList);
                    }

                    return new IndexerPart(literalList);
                }

                throw CreateParseException(BindingPathError.RightAngleBracketMissing, lexemes.Current);
            }

            return null;
        }

        private void LiteralList(Lexemes<ˍ> lexemes, List<Literal> literalList)
        {
            XamlType castXamlType = default;
            if (lexemes.AcceptToken("("))
            {
                castXamlType = this.XamlType(lexemes);
                if (!lexemes.AcceptToken(")"))
                {
                    throw CreateParseException(BindingPathError.RightParenthesisMissing, lexemes.Current);
                }
            }

            if (!lexemes.AcceptTokenType(ˍ._, true, out var value))
            {
                throw CreateParseException(BindingPathError.ValueMissing, lexemes.Current);
            }

            literalList.Add(new Literal(castXamlType, value.Trim()));
            if (lexemes.AcceptToken(","))
            {
                this.LiteralList(lexemes, literalList);
            }
        }

        private IPropertyExpression Property(Lexemes<ˍ> lexemes, bool isRequired)
        {
            if (lexemes.AcceptTokenType(ˍ._, out var value))
            {
                if (lexemes.AcceptTokenType(TokenInfo.End))
                {
                    lexemes.MoveToPrevious();
                    return new Property(value);
                }

                return new PropertyPart(value);
            }

            if (isRequired)
            {
                throw CreateParseException(BindingPathError.PropertyNameMissing, lexemes.Current);
            }

            return null;
        }
    }
}