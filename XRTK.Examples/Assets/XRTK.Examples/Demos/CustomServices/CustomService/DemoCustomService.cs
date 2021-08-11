// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using XRTK.Interfaces;
using XRTK.Services;

namespace XRTK.Examples.Demos.CustomServices
{
    [System.Runtime.InteropServices.Guid("07eed32a-3eaf-4700-ad6b-31e761074a74")]
    public class DemoCustomService : BaseServiceWithConstructor, IMixedRealityService
    {
        /// <summary>
        /// The implementation of your <see cref="IDemoCustomService"/>
        /// </summary>
        public DemoCustomService(string name, uint priority, DemoCustomServiceProfile profile)
            : base(name, priority)
        {
        }

        #region IDemoCustomService Implementation

        /// <inheritdoc />
        public Action MyCustomEvent { get; set; }

        private string myCustomData;

        /// <inheritdoc />
        public string MyCustomData
        {
            get => myCustomData;
            set
            {
                // Validate any value set to the property.
                if (string.IsNullOrWhiteSpace(value)) { return; }

                myCustomData = value;
                Debug.Log($"Set value of {nameof(MyCustomData)} to '{myCustomData}'");
            }
        }

        /// <inheritdoc />
        public void MyCustomMethod()
        {
            Debug.Log($"Called {nameof(MyCustomMethod)}");
        }

        #endregion IDemoCustomService Implementation
    }
}
