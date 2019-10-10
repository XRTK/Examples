// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using XRTK.Services;
using XRTK.Utilities.Async;
using UnityEngine;

namespace XRTK.Examples.Demos
{
    public class DiagnosticsDemoControls : MonoBehaviour
    {
        private async void Start()
        {
            if (!MixedRealityToolkit.Instance.ActiveProfile.IsDiagnosticsSystemEnabled)
            {
                Debug.LogWarning("Diagnostics system is disabled. To run this demo, it needs to be enabled. Check your configuration settings.");
                return;
            }

            await MixedRealityToolkit.DiagnosticsSystem.WaitUntil(system => system != null);

            // Turn on the diagnostics for this demo.
            MixedRealityToolkit.DiagnosticsSystem.ShowDiagnostics = true;
        }
    }
}
