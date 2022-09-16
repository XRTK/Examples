// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.InputSystem;
using XRTK.Definitions.Utilities;
using XRTK.EventDatum.Input;
using XRTK.Interfaces.InputSystem.Handlers;
using XRTK.SDK.Input.Handlers;
using XRTK.Services;

namespace XRTK.Examples.Demos.Input
{
    public class DemoInputHandler : BaseInputHandler,
            IMixedRealitySourceStateHandler,
            IMixedRealityInputHandler,
            IMixedRealityInputHandler<float>,
            IMixedRealityInputHandler<Vector2>,
            IMixedRealityInputHandler<Vector3>,
            IMixedRealityInputHandler<Quaternion>,
            IMixedRealityInputHandler<MixedRealityPose>,
            IMixedRealityGestureHandler<Vector3>,
            IMixedRealityGestureHandler<Quaternion>,
            IMixedRealityGestureHandler<MixedRealityPose>
    {
        [SerializeField]
        [Tooltip("The action that will move the camera forward, back, left, and right.")]
        private InputActionReference movementAction = null;

        [SerializeField]
        [Tooltip("The action that will pivot the camera on it's axis.")]
        private InputActionReference rotateAction = null;

        [SerializeField]
        [Tooltip("The action that will move the camera up or down vertically.")]
        private InputActionReference heightAction = null;

        private Vector3 newPosition = Vector3.zero;

        private Vector3 newRotation = Vector3.zero;

        #region Monobehaviour Implementation

        protected override void OnEnable()
        {
            base.OnEnable();

            Debug.Log($"[OnEnable] Is XRTK initialized? {MixedRealityToolkit.Instance != null}");
            Debug.Log($"[OnEnable] Is Input System initialized? {InputSystem != null}");
        }

        protected override void Start()
        {
            base.Start();

            Debug.Log($"[Start] Is XRTK initialized? {MixedRealityToolkit.Instance != null}");
            Debug.Log($"[Start] Is Input System initialized? {InputSystem != null}");

            if (InputSystem != null)
            {
                foreach (var inputSource in InputSystem.DetectedInputSources)
                {
                    Debug.Log($"[OnSourceDetected] {inputSource.SourceName}");
                }
            }
        }

        #endregion Monobehaviour Implementation

        #region IMixedRealityInputHandler Implementation

        void IMixedRealityInputHandler.OnInputUp(InputEventData eventData)
        {
            Debug.Log($"[OnInputUp] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityInputHandler.OnInputDown(InputEventData eventData)
        {
            Debug.Log($"[OnInputDown] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityInputHandler<float>.OnInputChanged(InputEventData<float> eventData)
        {
            // Debug.Log($"[OnInputChanged] {eventData.InputSource.SourceName} | {eventData.Context}");

            if (eventData.Context.action == heightAction.action)
            {
                newPosition.x = 0f;
                newPosition.y = eventData.InputData;
                newPosition.z = 0f;
                gameObject.transform.position += newPosition;
            }
        }

        void IMixedRealityInputHandler<Vector2>.OnInputChanged(InputEventData<Vector2> eventData)
        {
            // Debug.Log($"[OnInputChanged] {eventData.InputSource.SourceName} | {eventData.Context}");

            if (eventData.Context.action == movementAction.action)
            {
                newPosition.x = eventData.InputData.x;
                newPosition.y = 0f;
                newPosition.z = eventData.InputData.y;
                gameObject.transform.position += newPosition;
            }
            else if (eventData.Context.action == rotateAction.action)
            {
                newRotation.x = eventData.InputData.x;
                newRotation.y = eventData.InputData.y;
            }
        }

        void IMixedRealityInputHandler<Vector3>.OnInputChanged(InputEventData<Vector3> eventData)
        {
            // Debug.Log($"[OnInputChanged] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityInputHandler<Quaternion>.OnInputChanged(InputEventData<Quaternion> eventData)
        {
            // Debug.Log($"[OnInputChanged] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityInputHandler<MixedRealityPose>.OnInputChanged(InputEventData<MixedRealityPose> eventData)
        {
            // Debug.Log($"[OnInputChanged] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        #endregion IMixedRealityInputHandler Implementation

        #region IMixedRealitySourceStateHandler Implementation

        void IMixedRealitySourceStateHandler.OnSourceDetected(SourceStateEventData eventData)
        {
            Debug.Log($"[OnSourceDetected] {eventData.InputSource.SourceName}");
        }

        void IMixedRealitySourceStateHandler.OnSourceLost(SourceStateEventData eventData)
        {
            Debug.Log($"[OnSourceLost] {eventData.InputSource.SourceName}");
        }

        #endregion IMixedRealitySourceStateHandler Implementation

        #region IMixedRealityGestureHandler Implementation

        void IMixedRealityGestureHandler.OnGestureStarted(InputEventData eventData)
        {
            Debug.Log($"[OnGestureStarted] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityGestureHandler.OnGestureUpdated(InputEventData eventData)
        {
            Debug.Log($"[OnGestureUpdated] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityGestureHandler.OnGestureCompleted(InputEventData eventData)
        {
            Debug.Log($"[OnGestureCompleted] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityGestureHandler<Vector3>.OnGestureUpdated(InputEventData<Vector3> eventData)
        {
            Debug.Log($"[OnGestureUpdated] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityGestureHandler<Vector3>.OnGestureCompleted(InputEventData<Vector3> eventData)
        {
            Debug.Log($"[OnGestureCompleted] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityGestureHandler<Quaternion>.OnGestureUpdated(InputEventData<Quaternion> eventData)
        {
            Debug.Log($"[OnGestureUpdated] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityGestureHandler<Quaternion>.OnGestureCompleted(InputEventData<Quaternion> eventData)
        {
            Debug.Log($"[OnGestureCompleted] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityGestureHandler<MixedRealityPose>.OnGestureUpdated(InputEventData<MixedRealityPose> eventData)
        {
            Debug.Log($"[OnGestureUpdated] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityGestureHandler<MixedRealityPose>.OnGestureCompleted(InputEventData<MixedRealityPose> eventData)
        {
            Debug.Log($"[OnGestureCompleted] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        void IMixedRealityGestureHandler.OnGestureCanceled(InputEventData eventData)
        {
            Debug.Log($"[OnGestureCanceled] {eventData.InputSource.SourceName} | {eventData.Context}");
        }

        #endregion IMixedRealityGestureHandler Implementation
    }
}
