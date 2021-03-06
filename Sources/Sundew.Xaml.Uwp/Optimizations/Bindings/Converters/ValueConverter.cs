﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValueConverter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Converters
{
    using System;
    using Windows.UI.Xaml.Markup;

    /// <summary>Base class for implementing a converter.</summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TTarget">The type of the target.</typeparam>
    /// <seealso cref="IValueConverter{TSourceValue,TTargetValue}" />
    public abstract class ValueConverter<TSource, TTarget> : MarkupExtension, IValueConverter<TSource, TTarget>
    {
        /// <summary>Converts the specified source.</summary>
        /// <param name="source">The source.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>The target value.</returns>
        public abstract TTarget Convert(TSource source, Type targetType, object parameter, string language);

        /// <summary>Converts a value.</summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The language.</param>
        /// <returns>
        /// A converted value. If the method returns <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span>, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            TSource source = value == null ? default : (TSource)value;
            return this.Convert(source, targetType, parameter, language);
        }

        /// <summary>Converts the back.</summary>
        /// <param name="target">The target.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>The source value.</returns>
        public abstract TSource ConvertBack(TTarget target, Type targetType, object parameter, string language);

        /// <summary>Converts a value.</summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The language.</param>
        /// <returns>
        /// A converted value. If the method returns <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span>, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            TTarget target = value == null ? default : (TTarget)value;
            return this.ConvertBack(target, targetType, parameter, language);
        }
    }
}