// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using XRTK.Interfaces.DiagnosticsSystem;
using XRTK.Services;
using XRTK.Utilities.Async;

namespace XRTK.Examples.Demos
{
    public class DiagnosticsDemoControls : MonoBehaviour
    {
        private async void Start()
        {
            try
            {
                await MixedRealityToolkit.GetSystem<IMixedRealityDiagnosticsSystem>().WaitUntil(system => system != null);
            }
            catch (TimeoutException)
            {
                Debug.LogWarning($"The {nameof(IMixedRealityDiagnosticsSystem)} may be disabled. To run this demo, it needs to be enabled. Check your configuration settings.");
            }

            // Turn on the diagnostics for this demo.
            if (MixedRealityToolkit.TryGetSystem<IMixedRealityDiagnosticsSystem>(out var diagnosticsSystem))
            {
                diagnosticsSystem.IsWindowEnabled = true;
            }
        }
    }
}
