// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingConnectorController.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings
{
    using Windows.UI.Core;
    using Windows.UI.Xaml;

    /// <summary>Controller for <see cref="IBindingConnector"/>.</summary>
    public class BindingConnectorController
    {
        private readonly IBindingConnector bindingConnector;

        /// <summary>Initializes a new instance of the <see cref="BindingConnectorController"/> class.</summary>
        /// <param name="bindingConnector">The binding connector.</param>
        public BindingConnectorController(IBindingConnector bindingConnector)
        {
            this.bindingConnector = bindingConnector;
        }

        /// <summary>Initializes the specified framework element.</summary>
        /// <param name="dependencyObject">The dependency object.</param>
        public void Initialize(DependencyObject dependencyObject)
        {
            if (dependencyObject is FrameworkElement frameworkElement)
            {
                frameworkElement.DataContextChanged += this.OnFrameworkElementDataContextChanged;
                frameworkElement.Unloaded += this.OnFrameworkElementUnloaded;
            }

            CoreWindow.GetForCurrentThread().Closed += this.OnClosed;
        }

        /// <summary>Uns the initialize.</summary>
        /// <param name="dependencyObject">The dependency object.</param>
        public void UnInitialize(DependencyObject dependencyObject)
        {
            if (dependencyObject is FrameworkElement frameworkElement)
            {
                frameworkElement.DataContextChanged -= this.OnFrameworkElementDataContextChanged;
                frameworkElement.Unloaded -= this.OnFrameworkElementUnloaded;
            }

            CoreWindow.GetForCurrentThread().Closed -= this.OnClosed;
        }

        private void OnFrameworkElementDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            this.bindingConnector.Refresh();
        }

        private void OnClosed(CoreWindow sender, CoreWindowEventArgs args)
        {
            this.bindingConnector.Dispose();
        }

        private void OnFrameworkElementUnloaded(object sender, RoutedEventArgs e)
        {
            this.bindingConnector.Disconnect();
            if (this.bindingConnector.Root is FrameworkElement frameworkElement)
            {
                frameworkElement.Loaded += this.OnFrameworkElementLoaded;
            }
        }

        private void OnFrameworkElementLoaded(object sender, RoutedEventArgs e)
        {
            if (this.bindingConnector.Root is FrameworkElement frameworkElement)
            {
                frameworkElement.Loaded -= this.OnFrameworkElementLoaded;
            }

            this.bindingConnector.Reconnect();
        }
    }
}