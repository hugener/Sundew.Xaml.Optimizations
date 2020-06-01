// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyPropertyListener.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings
{
    using System;
    using Windows.UI.Xaml;

    /// <summary>Dependency property listener.</summary>
    public class DependencyPropertyListener
    {
        private readonly long token;
        private DependencyObject dependencyObject;
        private DependencyProperty dependencyProperty;
        private EventHandler<EventArgs> eventHandler;

        private DependencyPropertyListener(DependencyObject dependencyObject, DependencyProperty dependencyProperty, EventHandler<EventArgs> eventHandler)
        {
            this.dependencyObject = dependencyObject;
            this.dependencyProperty = dependencyProperty;
            this.eventHandler = eventHandler;
            this.token = this.dependencyObject.RegisterPropertyChangedCallback(dependencyProperty, this.OnDependencyPropertyChanged);
        }

        /// <summary>Subscribes the specified dependency object.</summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyProperty">The dependency property.</param>
        /// <param name="eventHandler">The event handler.</param>
        /// <returns>A new dependency property listener.</returns>
        public static DependencyPropertyListener Subscribe(DependencyObject dependencyObject, DependencyProperty dependencyProperty, EventHandler<EventArgs> eventHandler)
        {
            return new DependencyPropertyListener(dependencyObject, dependencyProperty, eventHandler);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            this.dependencyObject.UnregisterPropertyChangedCallback(this.dependencyProperty, this.token);
            this.dependencyObject = null;
            this.dependencyProperty = null;
            this.eventHandler = null;
        }

        private void OnDependencyPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            this.eventHandler(sender, EventArgs.Empty);
        }
    }
}