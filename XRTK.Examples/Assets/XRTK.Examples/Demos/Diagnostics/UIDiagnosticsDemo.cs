// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.Examples.ExamplesHub.Interfaces;
using XRTK.Interfaces.DiagnosticsSystem;
using XRTK.Services;
using XRTK.Utilities;

namespace XRTK.Examples.Demos.Diagnostics
{
    /// <summary>
    /// UI controller for the <see cref="IMixedRealityDiagnosticsSystem"/> demo.
    /// </summary>
    public class UIDiagnosticsDemo : MonoBehaviour
    {
        private IMixedRealityDiagnosticsSystem diagnosticsSystem;

        private void Start()
        {
            if (!MixedRealityToolkit.TryGetSystem(out diagnosticsSystem))
            {
                Debug.LogError($"Diagnostics system not found. This demo requires the diagnostics system to be enabled.");
                return;
            }

            diagnosticsSystem.IsWindowEnabled = true;
        }

        private void OnDestroy()
        {
            if (diagnosticsSystem != null)
            {
                diagnosticsSystem.IsWindowEnabled = false;
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

