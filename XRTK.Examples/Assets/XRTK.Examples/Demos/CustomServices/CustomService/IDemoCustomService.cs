// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using XRTK.Interfaces;

namespace XRTK.Examples.Demos.CustomServices
{
    /// <summary>
    /// The custom interface for your custom service.
    /// Only use property accessors in these to better control access to internals.
    /// This interface is the contract for others to use to make their own implementations if shared publicly.
    /// </summary>
    public interface IDemoCustomService : IMixedRealityService
    {
        /// <summary>
        /// A custom event.
        /// </summary>
        Action MyCustomEvent { get; set; }

        /// <summary>
        /// A custom property accessor.
        /// </summary>
        string MyCustomData { get; set; }

        /// <summary>
        /// A custom method call.
        /// </summary>
        void MyCustomMethod();
    }
}
