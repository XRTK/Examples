// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XRTK.Definitions.Controllers.Hands;
using XRTK.Interfaces.Providers.Controllers.Hands;
using XRTK.Services;

namespace XRTK.Examples.Demos.HandController
{
    /// <summary>
    /// Demo showcases hand physics settings and usage.
    /// </summary>
    public class HandPhysicsDemoStation : MonoBehaviour
    {
        private List<IMixedRealityHandControllerDataProvider> providers;

        [SerializeField]
        private Toggle physicsToggle = null;

        [SerializeField]
        private Image handsModeEnabledImage = null;

        [SerializeField]
        private Image fingersModeEnabledImage = null;

        private void Start()
        {
            providers = MixedRealityToolkit.GetActiveServices<IMixedRealityHandControllerDataProvider>();
            physicsToggle.isOn = providers[0].HandPhysicsEnabled;
            handsModeEnabledImage.gameObject.SetActive(providers[0].BoundsMode == HandBoundsLOD.Low);
            fingersModeEnabledImage.gameObject.SetActive(providers[0].BoundsMode == HandBoundsLOD.High);
        }

        public void TogglePhysics(bool isOn)
        {
            if (isOn)
            {
                EnablePhysics();
            }
            else
            {
                DisablePhysics();
            }
        }

        private void EnablePhysics()
        {
            for (int i = 0; i < providers.Count; i++)
            {
                providers[i].HandPhysicsEnabled = true;
            }
        }

        private void DisablePhysics()
        {
            for (int i = 0; i < providers.Count; i++)
            {
                providers[i].HandPhysicsEnabled = false;
            }
        }

        public void SetHandBoundsMode()
        {
            for (int i = 0; i < providers.Count; i++)
            {
                providers[i].BoundsMode = HandBoundsLOD.Low;
            }

            fingersModeEnabledImage.gameObject.SetActive(false);
            handsModeEnabledImage.gameObject.SetActive(true);
        }

        public void SetFingerBoundsMode()
        {
            for (int i = 0; i < providers.Count; i++)
            {
                providers[i].BoundsMode = HandBoundsLOD.High;
            }

            handsModeEnabledImage.gameObject.SetActive(false);
            fingersModeEnabledImage.gameObject.SetActive(true);
        }
    }
}