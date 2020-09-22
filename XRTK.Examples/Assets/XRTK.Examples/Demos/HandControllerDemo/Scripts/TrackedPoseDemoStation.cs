// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using TMPro;
using UnityEngine;
using XRTK.Definitions.Devices;
using XRTK.Definitions.Utilities;
using XRTK.EventDatum.Input;
using XRTK.Interfaces.InputSystem.Handlers;
using XRTK.Interfaces.Providers.Controllers.Hands;
using XRTK.SDK.Input;

namespace XRTK.Examples.Demos.HandController
{
    /// <summary>
    /// Demo script on how to handle changes in the recognized hand pose.
    /// </summary>
    [RequireComponent(typeof(InputSystemGlobalListener))]
    public class TrackedPoseDemoStation : MonoBehaviour, IMixedRealitySourcePoseHandler
    {
        [SerializeField]
        [Tooltip("Text element to display currently tracked left hand pose.")]
        private TextMeshPro leftHandPoseText = null;

        [SerializeField]
        [Tooltip("Text element to display currently tracked right hand pose.")]
        private TextMeshPro rightHandPoseText = null;

        public void OnSourceDetected(SourceStateEventData eventData)
        {

        }

        public void OnSourceLost(SourceStateEventData eventData)
        {

        }

        public void OnSourcePoseChanged(SourcePoseEventData<TrackingState> eventData)
        {

        }

        public void OnSourcePoseChanged(SourcePoseEventData<Vector2> eventData)
        {

        }

        public void OnSourcePoseChanged(SourcePoseEventData<Vector3> eventData)
        {

        }

        public void OnSourcePoseChanged(SourcePoseEventData<Quaternion> eventData)
        {

        }

        public void OnSourcePoseChanged(SourcePoseEventData<MixedRealityPose> eventData)
        {
            if (eventData.Controller.ControllerHandedness == Handedness.Left &&
                eventData.Controller is IMixedRealityHandController leftHandController)
            {
                var trackedPose = leftHandController.TrackedPoseId;
                var pose = trackedPose ?? "Unknown";
                leftHandPoseText.text = pose;
            }
            else if (eventData.Controller.ControllerHandedness == Handedness.Right &&
                eventData.Controller is IMixedRealityHandController rightHandController)
            {
                var trackedPose = rightHandController.TrackedPoseId;
                var pose = trackedPose ?? "Unknown";
                rightHandPoseText.text = pose;
            }
        }
    }
}
