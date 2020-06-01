// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TargetValueCodeGenerator.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.CodeGenerators.BindingPath
{
    using System.Xml.Linq;
    using Sundew.Xaml.Optimizations.Bindings.Internal.CodeAnalysis;
    using Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.MarkupExtension;
    using Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.Xaml;

    internal readonly struct TargetValueCodeGenerator
    {
        private TargetValueCodeGenerator(QualifiedType targetType, QualifiedProperty targetProperty, bool isAttached)
        {
            this.TargetType = targetType;
            this.TargetProperty = targetProperty;
            this.IsAttached = isAttached;
        }

        public QualifiedType TargetType { get; }

        public QualifiedProperty TargetProperty { get; }

        public bool IsAttached { get; }

        public static TargetValueCodeGenerator GetTarget(QualifiedType elementType, BindingAssignment bindingAssignment, XamlTypeResolver xamlTypeResolver, TypeResolver typeResolver)
        {
            var property = bindingAssignment.TargetProperty.Name.ToString();
            var lastDotIndex = property.LastIndexOf('.');
            if (lastDotIndex > -1)
            {
                var propertyName = property.Substring(lastDotIndex + 1);
                XName xamlTypeName = property.Substring(0, lastDotIndex);
                var ownerType = xamlTypeResolver.Parse(xamlTypeName);
                return new TargetValueCodeGenerator(ownerType, typeResolver.GetAttachedDependencyProperty(ownerType, propertyName), true);
            }

            return new TargetValueCodeGenerator(elementType, typeResolver.GetProperty(elementType, bindingAssignment.TargetProperty.Name.LocalName), false);
        }

        public string GetPropertyGetter()
        {
            if (this.IsAttached)
            {
                return $"{this.TargetType.ToAliasQualifiedType()}.Get{this.TargetProperty.Name}(t)";
            }

            return $"t.{this.TargetProperty.Name}";
        }

        public string GetDependencyProperty()
        {
            return $"{this.TargetType.ToAliasQualifiedType()}.{this.TargetProperty.Name}Property";
        }
    }
}