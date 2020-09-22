// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.Interfaces.Providers.Controllers.Hands;
using XRTK.Services;

namespace XRTK.Examples.Demos.HandController
{
    /// <summary>
    /// Demo for supported hand rendering modes.
    /// </summary>
    public class RenderingModeDemoStation : MonoBehaviour
    {
        public void SetNoneMode()
        {
            var providers = MixedRealityToolkit.GetActiveServices<IMixedRealityHandControllerDataProvider>();
            for (int i = 0; i < providers.Count; i++)
            {
                providers[i].RenderingMode = XRTK.Definitions.Controllers.Hands.HandRenderingMode.None;
            }
        }

        public void SetJointsMode()
        {
            var providers = MixedRealityToolkit.GetActiveServices<IMixedRealityHandControllerDataProvider>();
            for (int i = 0; i < providers.Count; i++)
            {
                providers[i].RenderingMode = XRTK.Definitions.Controllers.Hands.HandRenderingMode.Joints;
            }
        }

        public void SetMeshMode()
        {
            var providers = MixedRealityToolkit.GetActiveServices<IMixedRealityHandControllerDataProvider>();
            for (int i = 0; i < providers.Count; i++)
            {
                providers[i].RenderingMode = XRTK.Definitions.Controllers.Hands.HandRenderingMode.Mesh;
            }
        }
    }
}