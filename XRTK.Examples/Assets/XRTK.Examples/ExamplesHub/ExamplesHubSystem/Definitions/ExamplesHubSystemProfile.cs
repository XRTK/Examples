// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;
using XRTK.Attributes;
using XRTK.Definitions;
using XRTK.Definitions.Utilities;
using XRTK.Examples.ExamplesHub.Interfaces;

namespace XRTK.Examples.ExamplesHub.Definitions
{
    [CreateAssetMenu(menuName = "Mixed Reality Toolkit/Examples Hub System Profile", fileName = "ExamplesHubSystemProfile", order = (int)CreateProfileMenuItemIndices.RegisteredServiceProviders)]
    public class ExamplesHubSystemProfile : BaseMixedRealityServiceProfile<IExamplesHubSystemDataProvider>
    {
        [SerializeField]
        [Prefab]
        [Tooltip("Prefab for the example hub UI that allows selecting an example to load.")]
        private GameObject uiPrefab = null;

        /// <summary>
        /// Prefab for the example hub UI that allows selecting an example to load.
        /// </summary>
        public GameObject UIPrefab
        {
            get => uiPrefab;
            internal set => uiPrefab = value;
        }

        [SerializeField]
        [Tooltip("Examples available to the examples hub application.")]
        private List<ExampleProfile> examples = null;

        /// <summary>
        /// Examples available to the examples hub application.
        /// </summary>
        public List<ExampleProfile> Examples
        {
            get => examples;
            internal set => examples = value;
        }
    }
}
