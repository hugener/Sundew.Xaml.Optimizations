// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeValueConverter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Converters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Threading;

    /// <summary>Value converter that uses <see cref="TypeDescriptor"/>.</summary>
    /// <typeparam name="TSourceValue">The type of the source value.</typeparam>
    /// <typeparam name="TTargetValue">The type of the target value.</typeparam>
    /// <seealso cref="ValueConverter{TSource,TTarget}" />
    public class TypeValueConverter<TSourceValue, TTargetValue> : ValueConverter<TSourceValue, TTargetValue>
    {
        internal static readonly Lazy<TypeValueConverter<TSourceValue, TTargetValue>> Converter = new Lazy<TypeValueConverter<TSourceValue, TTargetValue>>(
            () => ConversionProvider.GetConverter<TSourceValue, TTargetValue>(typeof(TSourceValue)), LazyThreadSafetyMode.ExecutionAndPublication);

        private readonly Func<TSourceValue, Type, object, CultureInfo, TTargetValue> convert;
        private readonly Func<TTargetValue, Type, object, CultureInfo, TSourceValue> convertBack;

        /// <summary>Initializes a new instance of the <see cref="TypeValueConverter{TSourceValue, TTargetValue}"/> class.</summary>
        internal TypeValueConverter(Func<TSourceValue, Type, object, CultureInfo, TTargetValue> convert, Func<TTargetValue, Type, object, CultureInfo, TSourceValue> convertBack)
        {
            this.convert = convert;
            this.convertBack = convertBack;
        }

        /// <summary>Gets an instance of the type value converter.</summary>
        public static TypeValueConverter<TSourceValue, TTargetValue> Instance => Converter.Value;

        /// <summary>Converts the specified source.</summary>
        /// <param name="source">The source.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>The target value.</returns>
        public override TTargetValue Convert(TSourceValue source, Type targetType, object parameter, string language)
        {
            return this.convert(source, targetType, parameter, new CultureInfo(language));
        }

        /// <summary>Converts the back.</summary>
        /// <param name="target">The target.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>The source value.</returns>
        public override TSourceValue ConvertBack(TTargetValue target, Type targetType, object parameter, string language)
        {
            return this.convertBack(target, targetType, parameter, new CultureInfo(language));
        }

        /// <summary>
        /// When implemented in a derived class, returns an object that is provided as the value of the target property for this markup extension.
        /// </summary>
        /// <returns>
        /// The object value to set on the property where the extension is applied.
        /// </returns>
        protected override object ProvideValue()
        {
            return this;
        }
    }
}