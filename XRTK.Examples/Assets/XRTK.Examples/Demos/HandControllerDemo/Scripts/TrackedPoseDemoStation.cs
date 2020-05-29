// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.EventDatum.Input;
using XRTK.Interfaces.InputSystem.Handlers;

namespace XRTK.Examples.Demos.HandController
{
    /// <summary>
    /// Demo script on how to handle changes in the recognized hand pose.
    /// </summary>
    public class TrackedPoseDemoStation : MonoBehaviour, IMixedRealityInputHandler<XRTK.Definitions.Controllers.Hands.HandData>
    {
        [SerializeField]
        private TMPro.TextMeshPro leftHandPoseText = null;

        [SerializeField]
        private TMPro.TextMeshPro rightHandPoseText = null;

        public void OnInputChanged(InputEventData<XRTK.Definitions.Controllers.Hands.HandData> eventData)
        {
            if (eventData.Handedness == Definitions.Utilities.Handedness.Left)
            {
                var trackedPose = eventData.InputData.TrackedPose;
                var pose = trackedPose != null ? trackedPose.Id : "Unknown";
                leftHandPoseText.text = $"Left Pose: {pose}";
            }
            else if (eventData.Handedness == Definitions.Utilities.Handedness.Right)
            {
                var trackedPose = eventData.InputData.TrackedPose;
                var pose = trackedPose != null ? trackedPose.Id : "Unknown";
                rightHandPoseText.text = $"Right Pose: {pose}";
            }
        }
    }
}
