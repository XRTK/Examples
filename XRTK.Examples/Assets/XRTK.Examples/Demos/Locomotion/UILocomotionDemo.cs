// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.Examples.ExamplesHub.Interfaces;
using XRTK.Interfaces.LocomotionSystem;
using XRTK.Services;
using XRTK.Utilities;

namespace XRTK.Examples.Demos.Locomotion
{
    /// <summary>
    /// UI controller for the locomotion system demo UI.
    /// </summary>
    public class UILocomotionDemo : MonoBehaviour
    {
        private ILocomotionSystem locomotionSystem;

        private void Start()
        {
            if (!MixedRealityToolkit.TryGetSystem(out locomotionSystem))
            {
                Debug.LogError($"Locomotion system not found. The locomotion demo requires the locomotion system to be enabled.");
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
        /// Enables the <see cref="Providers.LocomotionSystem.BlinkTeleportLocomotionProvider"/> implementation of
        /// <see cref="ITeleportLocomotionProvider"/>.
        /// </summary>
        public void BlinkTeleport_OnClick()
        {
            locomotionSystem.EnableLocomotionProvider<Providers.LocomotionSystem.BlinkTeleportLocomotionProvider>();
        }

        /// <summary>
        /// Enables the <see cref="Providers.LocomotionSystem.DashTeleportLocomotionProvider"/> implementation of
        /// <see cref="ITeleportLocomotionProvider"/>.
        /// </summary>
        public void DashTeleport_OnClick()
        {
            locomotionSystem.EnableLocomotionProvider<Providers.LocomotionSystem.DashTeleportLocomotionProvider>();
        }

        /// <summary>
        /// Enables the <see cref="Providers.LocomotionSystem.InstantTeleportLocomotionProvider"/> implementation of
        /// <see cref="ITeleportLocomotionProvider"/>.
        /// </summary>
        public void InstantTeleport_OnClick()
        {
            locomotionSystem.EnableLocomotionProvider<Providers.LocomotionSystem.InstantTeleportLocomotionProvider>();
        }

        /// <summary>
        /// Exits the locomotion demo scene.
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
