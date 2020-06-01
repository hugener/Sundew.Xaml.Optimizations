// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CastingConverter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Converters
{
    using System;

    /// <summary>A value converter that casts.</summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TTarget">The type of the target.</typeparam>
    /// <seealso cref="IValueConverter{TSourceValue,TTargetValue}" />
    public class CastingConverter<TSource, TTarget> : ValueConverter<TSource, TTarget>
    {
        /// <summary>Converts the specified source.</summary>
        /// <param name="source">The source.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>The target value.</returns>
        public override TTarget Convert(TSource source, Type targetType, object parameter, string language)
        {
            object value = source;
            return (TTarget)value;
        }

        /// <summary>Converts the back.</summary>
        /// <param name="target">The target.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>The source value.</returns>
        public override TSource ConvertBack(TTarget target, Type targetType, object parameter, string language)
        {
            object value = target;
            return (TSource)value;
        }

        /// <summary>When implemented in a derived class, returns an object that is provided as the value of the target property for this markup extension.</summary>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
        protected override object ProvideValue()
        {
            return this;
        }
    }
}