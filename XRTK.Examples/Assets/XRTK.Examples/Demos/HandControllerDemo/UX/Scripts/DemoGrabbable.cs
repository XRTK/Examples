// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.Definitions.InputSystem;
using XRTK.Definitions.Utilities;
using XRTK.EventDatum.Input;
using XRTK.Interfaces.InputSystem;
using XRTK.Interfaces.InputSystem.Handlers;
using XRTK.SDK.Input.Handlers;
using XRTK.Services;

namespace XRTK.Examples.Demos.HandController.UX
{
    /// <summary>
    /// A simple grabbable interaciton handler component. This component was just
    /// created for demo purposes and is not part of the XRTK SDK. Feel free to use it as a starting
    /// point for your own component until XRTK has a proper component for it.
    /// </summary>
    /// <remarks>This component is not optimized for production use.</remarks>
    public class DemoGrabbable : BaseInputHandler, IMixedRealityInputHandler, IMixedRealityInputHandler<MixedRealityPose>
    {
        private bool isGripped;
        private float lerpTime;
        private Handedness gripHandedness;
        private bool lerpBackToStart;
        private Vector3 startPosition;
        private Quaternion startRotation;

        [SerializeField]
        private MixedRealityInputAction grabAction = MixedRealityInputAction.None;

        [SerializeField]
        private MixedRealityInputAction gripPoseAction = MixedRealityInputAction.None;

        [SerializeField]
        [Tooltip("If the distance to the grabbed object is above the threshold it will lerp towards the grab pose.")]
        private float lerpDistanceThreshold = .05f;

        [SerializeField]
        [Tooltip("Grab pose lerp duration.")]
        private float lerpDuration = 1f;

        [SerializeField]
        [Tooltip("Grip pose offset for the object.")]
        private Vector3 gripPositionOffset = Vector3.zero;

        private void Awake()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
        }

        private void Update()
        {
            if (lerpBackToStart)
            {
                var fraction = lerpTime / lerpDuration;
                transform.position = Vector3.Slerp(transform.position, startPosition, fraction);
                transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, fraction);
                lerpBackToStart = fraction < 1f;
                lerpTime += Time.deltaTime;
            }
        }

        public void OnInputChanged(InputEventData<MixedRealityPose> eventData)
        {
            if (eventData.used)
            {
                return;
            }

            if (eventData.Handedness == gripHandedness && eventData.MixedRealityInputAction == gripPoseAction && isGripped)
            {
                if (Vector3.Distance(transform.position, eventData.InputData.Position) <= lerpDistanceThreshold)
                {
                    transform.position = eventData.InputData.Position + eventData.InputData.Rotation * gripPositionOffset;
                    transform.rotation = eventData.InputData.Rotation;
                }
                else
                {
                    var fraction = lerpTime / lerpDuration;
                    transform.position = Vector3.Slerp(transform.position, eventData.InputData.Position + eventData.InputData.Rotation * gripPositionOffset, fraction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, eventData.InputData.Rotation, fraction);
                    lerpTime += Time.deltaTime;
                }
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

                if (MixedRealityToolkit.TryGetSystem<IMixedRealityInputSystem>(out var inputSystem))
                {
                    inputSystem.Register(gameObject);
                }

                lerpTime = 0f;
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

                if (MixedRealityToolkit.TryGetSystem<IMixedRealityInputSystem>(out var inputSystem))
                {
                    inputSystem.Unregister(gameObject);
                }

                eventData.Use();

                lerpTime = 0f;
                lerpBackToStart = true;
            }
        }
    }
}