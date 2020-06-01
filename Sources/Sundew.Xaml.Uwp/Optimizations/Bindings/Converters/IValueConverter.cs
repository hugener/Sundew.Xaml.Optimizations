﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValueConverter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    /// <summary>Interface for implementing a typed value converter.</summary>
    /// <typeparam name="TSourceValue">The type of the source value.</typeparam>
    /// <typeparam name="TTargetValue">The type of the target value.</typeparam>
    public interface IValueConverter<TSourceValue, TTargetValue> : IValueConverter
    {
        /// <summary>Converts the specified source.</summary>
        /// <param name="source">The source.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>The target value.</returns>
        TTargetValue Convert(TSourceValue source, Type targetType, object parameter, string language);

        /// <summary>Converts the back.</summary>
        /// <param name="target">The target.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>The source value.</returns>
        TSourceValue ConvertBack(TTargetValue target, Type targetType, object parameter, string language);
    }
}