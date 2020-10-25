// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using XRTK.Extensions;
using XRTK.Services;

namespace XRTK.Examples.Demos.CustomExtensionServices
{
    /// <summary>
    /// The implementation of your <see cref="IDemoCustomExtensionService"/>
    /// </summary>
    [System.Runtime.InteropServices.Guid("C1E2FA5F-A0E2-42E2-8BF6-84AAEE76E91A")]
    public class DemoCustomExtensionService : BaseExtensionService, IDemoCustomExtensionService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="priority"></param>
        /// <param name="profile"></param>
        public DemoCustomExtensionService(string name, uint priority, DemoCustomExtensionServiceProfile profile)
                : base(name, priority, profile)
        {
            // If your service requires the use of a configuration profile, be sure to check it here.
            if (profile.IsNull())
            {
                throw new Exception($"{GetType().Name} expects a {nameof(DemoCustomExtensionServiceProfile)}");
            }

            // In the constructor, you should set any configuration data from your profile here.
            myCustomData = profile.MyCustomStringData;
        }

        #region IMixedRealityService Implementation

        /// <inheritdoc />
        public override void Update()
        {
            base.Update();

            // Invoke our custom event on each update. Subscribe to MyCustomEvent get events.
            MyCustomEvent?.Invoke();
        }

        #endregion IMixedRealityService Implementation

        #region IDemoCustomExtensionService Implementation

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

        #endregion IDemoCustomExtensionService Implementation
    }
}
