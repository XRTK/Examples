// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using XRTK.Services;

namespace XRTK.Examples.Demos.CustomServices
{
    /// <summary>
    /// The implementation of your <see cref="IDemoCustomSystem"/>
    /// </summary>
    [System.Runtime.InteropServices.Guid("debb0828-15a9-44e8-a884-f05b2d875675")]
    public class DemoCustomService : BaseService, IDemoCustomService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public DemoCustomService()
            : base()
        { }

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
