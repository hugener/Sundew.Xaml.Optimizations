// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToStringVisitor.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.BindingPath.Visitors
{
    using System.Text;
    using Sundew.Base;
    using Sundew.Base.Visiting;

    internal class ToStringVisitor : IBindingPathWalker<ˍ, StringBuilder, string>
    {
        public string Visit(IBindingPathExpression bindingPathExpression, ˍ parameter, StringBuilder stringBuilder = null)
        {
            stringBuilder ??= new StringBuilder();
            bindingPathExpression.Visit(this, parameter, stringBuilder);
            return stringBuilder.ToString();
        }

        public void VisitUnknown(IBindingPathExpression bindingPathExpression, ˍ parameter, StringBuilder stringBuilder)
        {
            throw VisitException.Create(bindingPathExpression, parameter, stringBuilder);
        }

        public void VisitAttachedDependencyPropertyPart(AttachedDependencyPropertyPart attachedDependencyPropertyPart, ˍ parameter, in StringBuilder stringBuilder)
        {
            stringBuilder.Append(attachedDependencyPropertyPart);
        }

        public void VisitAttachedDependencyProperty(AttachedDependencyProperty attachedDependencyProperty, ˍ parameter, in StringBuilder stringBuilder)
        {
            stringBuilder.Append(attachedDependencyProperty);
        }

        public void VisitIndexerAccessor(IndexerAccessor indexerAccessor, ˍ parameter, in StringBuilder stringBuilder)
        {
            indexerAccessor.Source.Visit(this, parameter, stringBuilder);
            stringBuilder.Append(indexerAccessor.Operator);
            indexerAccessor.Indexer.Visit(this, parameter, stringBuilder);
        }

        public void VisitIndexerPart(IndexerPart indexerPart, ˍ parameter, in StringBuilder stringBuilder)
        {
            stringBuilder.Append(indexerPart);
        }

        public void VisitIndexer(Indexer indexer, ˍ parameter, in StringBuilder stringBuilder)
        {
             stringBuilder.Append(indexer);
        }

        public void VisitPropertyAccessor(PropertyAccessor propertyAccessor, ˍ parameter, in StringBuilder stringBuilder)
        {
            propertyAccessor.Source.Visit(this, parameter,  stringBuilder);
            stringBuilder.Append(propertyAccessor.Operator);
            propertyAccessor.Property.Visit(this, parameter,  stringBuilder);
        }

        public void VisitPropertyPart(PropertyPart propertyPart, ˍ parameter, in StringBuilder stringBuilder)
        {
             stringBuilder.Append(propertyPart);
        }

        public void VisitProperty(Property property, ˍ parameter, in StringBuilder stringBuilder)
        {
             stringBuilder.Append(property);
        }

        public void VisitDataContextSource(DataContextSource dataContextSource, ˍ parameter, in StringBuilder stringBuilder)
        {
            stringBuilder.Append(dataContextSource);
        }
    }
}