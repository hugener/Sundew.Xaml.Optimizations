// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingContainerXamlModificationCollector.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.XamlModification.BindingContainer
{
    using System.Collections.Generic;
    using System.Linq;
    using Sundew.Base.Computation;
    using Sundew.Base.Visiting;
    using Sundew.Xaml.Optimizations.Bindings.Internal.CodeAnalysis;
    using Sundew.Xaml.Optimizations.Bindings.Internal.Parsing.Xaml;

    internal class BindingContainerXamlModificationCollector : IBindingVisitor<Parameters, Context, bool, Result.IfSuccess<XamlModificationCollection>>
    {
        private const string BindingContainer = "BindingContainer";

        public Result.IfSuccess<XamlModificationCollection> Visit(IBindingNode bindingNode, Parameters parameters, Context context)
        {
            var result = bindingNode.Visit(this, parameters, context);
            if (result)
            {
                return Result.Success(new XamlModificationCollection(context.XamlModificationInfos, context.BindingRootTypes));
            }

            return Result.Error();
        }

        public void VisitUnknown(IBindingNode bindingNode, Parameters parameters, Context context)
        {
            throw VisitException.Create(bindingNode, parameters, context);
        }

        public bool BindingTree(BindingTree bindingTree, Parameters parameters, Context context)
        {
            var result = false;
            foreach (var bindingRoot in bindingTree.BindingRoots)
            {
                result |= bindingRoot.Visit(this, parameters, context);
            }

            return result;
        }

        public bool BindingRoot(BindingRootNode bindingRootNode, Parameters parameters, Context context)
        {
            if (bindingRootNode.Name != null)
            {
                var xamlModificationTracker = new XamlModificationTracker { ModificationsRootElement = bindingRootNode.XElement };
                if (this.VisitChildBindings(bindingRootNode.Bindings, parameters, new Context(xamlModificationTracker, context)))
                {
                    var bindingContainerType = new QualifiedType(QualifiedType.GlobalAlias, parameters.AssemblyName, parameters.Namespace, bindingRootNode.Name + BindingContainer);
                    context.BindingRootTypes.Add(bindingRootNode, bindingContainerType);
                    context.XamlModificationInfos.Add(new XamlModificationInfo(bindingContainerType, xamlModificationTracker.ModificationsRootElement, xamlModificationTracker.XamlModifications));
                    context.XamlElementNameProvider.Reset();
                    return true;
                }
            }

            context.XamlElementNameProvider.Reset();
            return false;
        }

        public bool DataContextTargetBinding(DataContextTargetBindingNode dataContextTargetBindingNode, Parameters parameters, Context context)
        {
            var result = this.VisitDefiniteBinding(dataContextTargetBindingNode);
            this.VisitChildBindings(dataContextTargetBindingNode.Bindings, parameters, context);
            if (result)
            {
                context.XamlModificationTracker.Add(dataContextTargetBindingNode.TargetElement, new BindingXamlModification(dataContextTargetBindingNode.Id, dataContextTargetBindingNode.BindingAssignment, context.XamlElementNameProvider));

                return true;
            }

            return false;
        }

        public bool CastDataContextBindingSource(CastDataContextBindingSourceNode castSourceBinding, Parameters parameters, Context context)
        {
            return this.VisitChildBindings(castSourceBinding.Bindings, parameters, context);
        }

        public bool ControlTemplateCastDataContextBindingSource(
            ControlTemplateCastDataContextBindingSourceNode controlTemplateCastDataContextBindingSourceNode,
            Parameters parameters,
            Context context)
        {
            context.XamlModificationTracker.ModificationsRootElement = controlTemplateCastDataContextBindingSourceNode.ContentElement;
            return false;
        }

        public bool DataTemplateCastDataContextBindingSource(
            DataTemplateCastDataContextBindingSourceNode dataTemplateCastDataContextBindingSourceNode,
            Parameters parameters,
            Context context)
        {
            if (this.VisitChildBindings(dataTemplateCastDataContextBindingSourceNode.Bindings, parameters, context))
            {
                context.XamlModificationTracker.ModificationsRootElement = dataTemplateCastDataContextBindingSourceNode.ContentElement;
                return true;
            }

            return false;
        }

        public bool ElementBindingSource(ElementBindingSourceNode elementBindingSourceNode, Parameters parameters, Context context)
        {
            return this.VisitChildBindings(elementBindingSourceNode.Bindings, parameters, context);
        }

        public bool Binding(BindingNode bindingNode, Parameters parameters, Context context)
        {
            var result = this.VisitDefiniteBinding(bindingNode);
            if (result)
            {
                context.XamlModificationTracker.Add(bindingNode.TargetElement, new BindingXamlModification(bindingNode.Id, bindingNode.BindingAssignment, context.XamlElementNameProvider));

                return true;
            }

            return false;
        }

        private bool VisitDefiniteBinding(IDefiniteBinding definiteBinding)
        {
            return definiteBinding.IsEnabled;
        }

        private bool VisitChildBindings(IReadOnlyList<IBinding> bindings, Parameters parameters, Context context)
        {
            var result = false;
            foreach (var binding in bindings)
            {
                result |= binding.Visit(this, parameters, context);
            }

            return result;
        }
    }
}