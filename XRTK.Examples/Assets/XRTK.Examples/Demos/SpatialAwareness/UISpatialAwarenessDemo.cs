// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.Examples.ExamplesHub.Interfaces;
using XRTK.Interfaces.SpatialAwarenessSystem;
using XRTK.Services;
using XRTK.Utilities;

namespace XRTK.Examples.Demos.SpatialAwareness
{
    /// <summary>
    /// UI controller for the <see cref="IMixedRealitySpatialAwarenessSystem"/> demo.
    /// </summary>
    public class UISpatialAwarenessDemo : MonoBehaviour
    {
        private void Start()
        {
            if (!MixedRealityToolkit.TryGetSystem<IMixedRealitySpatialAwarenessSystem>(out var spatialAwarenessSystem))
            {
                Debug.LogError($"Spatial awareness system not found. This demo requires the Spatial awareness system to be enabled.");
            }
        }

        private void Update()
        {
            var rotation = Quaternion.LookRotation((transform.position - CameraCache.Main.transform.position).normalized, transform.up).eulerAngles;
            rotation.x = 0f;
            rotation.z = 0f;

            transform.eulerAngles = rotation;
        }

        /// <summary>
        /// Exits the demo scene.
        /// </summary>
        public void ExitDemo_OnClick()
        {
            if (!MixedRealityToolkit.TryGetSystem<IExamplesHubSystem>(out var examplesHubSystem))
            {
                Debug.LogError($"Examples hub system not found, failed to exit example.");
                return;
            }

            examplesHubSystem.ExitExample();
        }
    }
}
