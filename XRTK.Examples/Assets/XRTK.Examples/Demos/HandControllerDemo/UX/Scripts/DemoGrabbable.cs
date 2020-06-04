// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.Definitions.InputSystem;
using XRTK.Definitions.Utilities;
using XRTK.EventDatum.Input;
using XRTK.Interfaces.InputSystem.Handlers;
using XRTK.SDK.Input.Handlers;

namespace XRTK.Examples.Demos.HandController.UX
{
    public class DemoGrabbable : BaseInputHandler, IMixedRealityInputHandler, IMixedRealityInputHandler<MixedRealityPose>
    {
        private bool isGripped;

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

            if (eventData.MixedRealityInputAction == gripPoseAction && isGripped)
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

            if (eventData.MixedRealityInputAction == grabAction)
            {
                isGripped = true;
                eventData.Use();
            }
        }

        public void OnInputUp(InputEventData eventData)
        {
            if (eventData.used)
            {
                return;
            }

            if (eventData.MixedRealityInputAction == grabAction)
            {
                isGripped = false;
                eventData.Use();
            }
        }
    }
}