// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingDispatcher.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings.Internals
{
    using System;
    using Windows.UI.Core;

    internal class BindingDispatcher : IBindingDispatcher
    {
        private readonly CoreDispatcher coreDispatcher;

        public BindingDispatcher(CoreDispatcher coreDispatcher)
        {
            this.coreDispatcher = coreDispatcher;
        }

        public bool HasAccess => this.coreDispatcher.HasThreadAccess;

        public async void InvokeAsync(Action action)
        {
            await this.coreDispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
        }
    }
}
