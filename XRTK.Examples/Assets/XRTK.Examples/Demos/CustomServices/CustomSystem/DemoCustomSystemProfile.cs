// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.Definitions;
using XRTK.Interfaces;

namespace XRTK.Examples.Demos.CustomServices
{
    /// <summary>
    /// This is the custom configuration profile for your custom system.
    /// In this file you'll want to put as much customizable options into it for the application to consume at runtime.
    /// </summary>
    [CreateAssetMenu(menuName = "Mixed Reality Toolkit/Demos/CustomSystemProfile", fileName = "DemoCustomSystemProfile", order = 99)]
    public class DemoCustomSystemProfile : BaseMixedRealityServiceProfile<IMixedRealityDataProvider>
    {
        [SerializeField]
        [Tooltip("The custom configuration data you want to use at runtime.")]
        private string myCustomStringData = string.Empty;

        /// <summary>
        /// The custom configuration data you want to use at runtime.
        /// </summary>
        public string MyCustomStringData => myCustomStringData;
    }
}
