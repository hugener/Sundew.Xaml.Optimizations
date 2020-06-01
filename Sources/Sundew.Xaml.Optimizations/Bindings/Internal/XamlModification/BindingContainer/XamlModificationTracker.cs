// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XamlModificationTracker.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internal.XamlModification.BindingContainer
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    internal class XamlModificationTracker
    {
        private readonly Dictionary<XElement, List<BindingXamlModification>> xamlModifications = new Dictionary<XElement, List<BindingXamlModification>>();

        public IEnumerable<BindingXamlModifications> XamlModifications => this.xamlModifications.Select(x => new BindingXamlModifications(x.Key, x.Value));

        public XElement ModificationsRootElement { get; set; }

        public void Add(XElement targetElement, BindingXamlModification bindingXamlModification)
        {
            if (!this.xamlModifications.TryGetValue(targetElement, out var xamlChanges))
            {
                xamlChanges = new List<BindingXamlModification>();
                this.xamlModifications.Add(targetElement, xamlChanges);
            }

            xamlChanges.Add(bindingXamlModification);
        }
    }
}