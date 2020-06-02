// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;
using XRTK.Definitions.BoundarySystem;
using XRTK.Services;
using XRTK.Utilities.Async;

namespace XRTK.Examples.Demos.BoundarySystem
{
    /// <summary>
    /// Demo class to show different ways of using the boundary system and visualizing the data.
    /// </summary>
    public class BoundaryVisualizationDemo : MonoBehaviour
    {
        private GameObject markerParent;
        private readonly List<GameObject> markers = new List<GameObject>();

        [SerializeField]
        private bool showBoundary = true;

        [SerializeField]
        private bool showFloor = true;

        [SerializeField]
        private bool showBoundaryWalls = true;

        [SerializeField]
        private bool showBoundaryCeiling = true;

        #region MonoBehaviour Implementation

        private void Awake()
        {
            markerParent = new GameObject("Boundary Demo Markers");
            markerParent.transform.parent = MixedRealityToolkit.CameraSystem.MainCameraRig.PlayspaceTransform;
        }

        private void Start()
        {
            if (MixedRealityToolkit.BoundarySystem != null)
            {
                if (markers.Count == 0)
                {
                    AddMarkers();
                }
            }
        }

        private async void OnEnable()
        {
            await new WaitUntil(() => MixedRealityToolkit.BoundarySystem != null);
            MixedRealityToolkit.BoundarySystem.BoundaryProximityAlert += OnBoundaryProximityAlert;
            MixedRealityToolkit.BoundarySystem.ShowBoundary = showBoundary;
            MixedRealityToolkit.BoundarySystem.ShowFloor = showFloor;
            MixedRealityToolkit.BoundarySystem.ShowWalls = showBoundaryWalls;
            MixedRealityToolkit.BoundarySystem.ShowCeiling = showBoundaryCeiling;
        }

        private void OnDisable()
        {
            if (MixedRealityToolkit.BoundarySystem != null)
            {
                MixedRealityToolkit.BoundarySystem.BoundaryProximityAlert -= OnBoundaryProximityAlert;
            }
        }

        #endregion MonoBehaviour Implementation

        private void OnBoundaryProximityAlert(GameObject trackedGameObject, ProximityAlert proximityAlert)
        {
            switch (proximityAlert)
            {
                case ProximityAlert.Clear:
                    Debug.Log($"{gameObject.name} is safely inside the boundary.");
                    break;
                case ProximityAlert.Touch:
                    Debug.LogWarning($"{gameObject.name} is approaching the boundary!");
                    break;
                case ProximityAlert.Exit:
                    Debug.LogError($"{gameObject.name} has left the boundary!");
                    break;
                case ProximityAlert.Enter:
                    Debug.Log($"{gameObject.name} has entered the boundary.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(proximityAlert), proximityAlert, null);
            }
        }

        /// <summary>
        /// Displays the boundary as an array of spheres where spheres in the
        /// bounds are a different color.
        /// </summary>
        private void AddMarkers()
        {
            // Get the rectangular bounds.

            if (!MixedRealityToolkit.BoundarySystem.TryGetRectangularBoundsParams(out var centerRect, out var angleRect, out var widthRect, out var heightRect))
            {
                // If we have no boundary manager or rectangular bounds we will show no indicators
                return;
            }

            const int indicatorCount = 20;
            const float indicatorDistance = 0.2f;
            const float indicatorScale = 0.1f;
            const float dimension = indicatorCount * indicatorDistance;

            var center = new Vector3(centerRect.x, 0f, centerRect.y);
            var corner = center - (new Vector3(dimension, 0.0f, dimension) * 0.5f);

            corner.y += 0.05f;

            for (int xIndex = 0; xIndex < indicatorCount; ++xIndex)
            {
                for (int yIndex = 0; yIndex < indicatorCount; ++yIndex)
                {
                    var offset = new Vector3(xIndex * indicatorDistance, 0.0f, yIndex * indicatorDistance);
                    var position = corner + offset;

                    if (MixedRealityToolkit.BoundarySystem.IsInsideBoundary(position))
                    {
                        var marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        marker.name = "Boundary Demo Marker";
                        marker.transform.SetParent(markerParent.transform, false);
                        marker.transform.position = position;
                        marker.transform.localScale = Vector3.one * indicatorScale;
                        markers.Add(marker);
                    }
                }
            }
        }
    }
}
