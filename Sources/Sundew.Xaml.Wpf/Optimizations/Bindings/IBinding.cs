// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBinding.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.Optimizations.Bindings
{
    using System;

    /// <summary>Interface for implementing a binding.</summary>
    /// <seealso cref="System.IDisposable" />
    public interface IBinding : IDisposable
    {
        /// <summary>
        /// Connects this instance.
        /// </summary>
        void Connect();

        /// <summary>Refreshes this instance.</summary>
        void Refresh();

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        void Disconnect();
    }
}