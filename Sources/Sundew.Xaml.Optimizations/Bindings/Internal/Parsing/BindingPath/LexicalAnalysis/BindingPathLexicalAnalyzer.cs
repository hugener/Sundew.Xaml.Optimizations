// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingPathLexicalAnalyzer.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.BindingPath.LexicalAnalysis
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Sundew.Base;
    using Sundew.Base.Collections;
    using Sundew.Base.Computation;

    internal class BindingPathLexicalAnalyzer
    {
        private const string Tokens = "Tokens";
        private static readonly Regex Tokenizer = new Regex(@"(?<Tokens>[\w\ ]+|\.|\(|\)|\[|\]|\:|\,)*");

        public Result.IfSuccess<Lexemes<ˍ>> Analyze(string input)
        {
            var match = Tokenizer.Match(input);
            if (match.Success)
            {
                var lexemes = match.Groups[Tokens].Captures.SelectFromNonGeneric<Lexeme<ˍ>>(x =>
                {
                    var capture = (Capture)x;
                    return new Lexeme<ˍ>(capture.Value, ˍ._, capture.Index);
                }).ToList();
                lexemes.Add(new Lexeme<ˍ>(string.Empty, TokenInfo.End, input.Length));
                return Result.Success(new Lexemes<ˍ>(lexemes));
            }

            return Result.Error();
        }
    }
}