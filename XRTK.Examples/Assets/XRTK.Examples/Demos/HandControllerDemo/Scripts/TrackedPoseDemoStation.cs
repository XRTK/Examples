// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using TMPro;
using UnityEngine;
using XRTK.EventDatum.Input;
using XRTK.Interfaces.InputSystem.Handlers;
using XRTK.SDK.Input;

namespace XRTK.Examples.Demos.HandController
{
    /// <summary>
    /// Demo script on how to handle changes in the recognized hand pose.
    /// </summary>
    [RequireComponent(typeof(InputSystemGlobalListener))]
    public class TrackedPoseDemoStation : MonoBehaviour, IMixedRealityInputHandler<Definitions.Controllers.Hands.HandData>
    {
        [SerializeField]
        [Tooltip("Text element to display currently tracked left hand pose.")]
        private TextMeshPro leftHandPoseText = null;

        [SerializeField]
        [Tooltip("Text element to display currently tracked right hand pose.")]
        private TextMeshPro rightHandPoseText = null;

        /// <inheritdoc />
        public void OnInputChanged(InputEventData<Definitions.Controllers.Hands.HandData> eventData)
        {
            if (eventData.Handedness == Definitions.Utilities.Handedness.Left)
            {
                var trackedPose = eventData.InputData.TrackedPoseId;
                var pose = trackedPose != null ? trackedPose : "Unknown";
                leftHandPoseText.text = $"Left Pose: {pose}";
            }
            else if (eventData.Handedness == Definitions.Utilities.Handedness.Right)
            {
                var trackedPose = eventData.InputData.TrackedPoseId;
                var pose = trackedPose != null ? trackedPose : "Unknown";
                rightHandPoseText.text = $"Right Pose: {pose}";
            }
        }
    }
}
