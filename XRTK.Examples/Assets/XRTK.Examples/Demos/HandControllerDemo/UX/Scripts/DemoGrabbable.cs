// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.Definitions.InputSystem;
using XRTK.Definitions.Utilities;
using XRTK.EventDatum.Input;
using XRTK.Interfaces.InputSystem.Handlers;
using XRTK.SDK.Input.Handlers;
using XRTK.Services;

namespace XRTK.Examples.Demos.HandController.UX
{
    public class DemoGrabbable : BaseInputHandler, IMixedRealityInputHandler, IMixedRealityInputHandler<MixedRealityPose>
    {
        private bool isGripped;
        private Handedness gripHandedness;

        [SerializeField]
        private MixedRealityInputAction grabAction = MixedRealityInputAction.None;

        [SerializeField]
        private MixedRealityInputAction gripPoseAction = MixedRealityInputAction.None;

        public void OnInputChanged(InputEventData<MixedRealityPose> eventData)
        {
            if (eventData.used)
            {
                return;
            }

            if (eventData.Handedness == gripHandedness && eventData.MixedRealityInputAction == gripPoseAction && isGripped)
            {
                transform.position = eventData.InputData.Position;
                transform.rotation = eventData.InputData.Rotation;
            }
        }

        public void OnInputDown(InputEventData eventData)
        {
            if (eventData.used)
            {
                return;
            }

            if (!isGripped && eventData.MixedRealityInputAction == grabAction)
            {
                isGripped = true;
                gripHandedness = eventData.Handedness;

                if (MixedRealityToolkit.IsInitialized &&
                MixedRealityToolkit.InputSystem != null)
                {
                    MixedRealityToolkit.InputSystem.Register(gameObject);
                }

                eventData.Use();
            }
        }

        public void OnInputUp(InputEventData eventData)
        {
            if (eventData.used || eventData.Handedness != gripHandedness)
            {
                return;
            }

            if (eventData.MixedRealityInputAction == grabAction)
            {
                isGripped = false;
                MixedRealityToolkit.InputSystem?.Unregister(gameObject);
                eventData.Use();
            }
        }
    }
}