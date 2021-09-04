// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;
using XRTK.Definitions;
using XRTK.Interfaces;

namespace XRTK.Examples.ExamplesHub.Definitions
{
    /// <summary>
    /// Configuration profile for an XRTK example.
    /// </summary>
    [CreateAssetMenu(menuName = "Mixed Reality Toolkit/Demos/Example Profile", fileName = "ExampleProfile", order = 99)]
    public class ExampleProfile : BaseMixedRealityProfile
    {
        [SerializeField]
        [Tooltip("The examples title.")]
        private string title = null;

        /// <summary>
        /// The examples title.
        /// </summary>
        public string Title => title;

        [SerializeField]
        [Tooltip("Example content description.")]
        private string description = null;

        /// <summary>
        /// Example content description.
        /// </summary>
        public string Description => description;

        [SerializeField]
        [Tooltip("The name of the scene containing the example content.")]
        private string sceneName = null;

        /// <summary>
        /// The name of the scene containing the example content.
        /// </summary>
        public string SceneName => sceneName;

        [SerializeField]
        [Tooltip("An URL pointing to documentation related to this example.")]
        private string docsUrl = null;

        /// <summary>
        /// An URL pointing to documentation related to this example.
        /// </summary>
        public string DocsUrl
        {
            get => docsUrl;
            internal set => docsUrl = value;
        }

        [SerializeField]
        [Tooltip("Platforms supported by this example.")]
        private RuntimePlatformEntry platformEntries = new RuntimePlatformEntry();

        private List<IMixedRealityPlatform> runtimePlatforms = null;
        /// <summary>
        /// Platforms supported by this example.
        /// </summary>
        public IReadOnlyList<IMixedRealityPlatform> RuntimePlatforms
        {
            get
            {
                if (runtimePlatforms == null ||
                    runtimePlatforms.Count == 0 ||
                    runtimePlatforms.Count != platformEntries?.RuntimePlatforms?.Length)
                {
                    runtimePlatforms = new List<IMixedRealityPlatform>();

                    for (int i = 0; i < platformEntries?.RuntimePlatforms?.Length; i++)
                    {
                        var platformType = platformEntries.RuntimePlatforms[i]?.Type;

                        if (platformType == null)
                        {
                            continue;
                        }

                        IMixedRealityPlatform platformInstance;

                        try
                        {
                            platformInstance = Activator.CreateInstance(platformType) as IMixedRealityPlatform;
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(e);
                            continue;
                        }

                        runtimePlatforms.Add(platformInstance);
                    }
                }

                return runtimePlatforms;
            }
        }

        [SerializeField]
        [Tooltip("Does this example work with transparent displays?")]
        private bool worksWithTransparentDisplay = true;

        /// <summary>
        /// Does this example work with transparent displays?
        /// </summary>
        public bool WorksWithTransparentDisplay
        {
            get => worksWithTransparentDisplay;
            internal set => worksWithTransparentDisplay = value;
        }

        [SerializeField]
        [Tooltip("Does this example work with opaque displays?")]
        private bool worksWithOpaqueDisplay = true;

        /// <summary>
        /// Does this example work with opaque displays?
        /// </summary>
        public bool WorksWithOpaqueDisplay
        {
            get => worksWithOpaqueDisplay;
            internal set => worksWithOpaqueDisplay = value;
        }
    }
}
