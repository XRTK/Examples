﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using XRTK.Definitions.BoundarySystem;
using XRTK.EventDatum.Boundary;
using XRTK.Interfaces.BoundarySystem;
using XRTK.Services;
using System.Collections.Generic;
using XRTK.Utilities.Async;
using UnityEngine;
using UnityEngine.Experimental.XR;

namespace XRTK.Examples.Demos
{
    /// <summary>
    /// Demo class to show different ways of using the boundary system and visualizing the data.
    /// </summary>
    public class BoundaryVisualizationDemo : MonoBehaviour, IMixedRealityBoundaryHandler
    {
        private GameObject markerParent;
        private readonly List<GameObject> markers = new List<GameObject>();

        [SerializeField]
        private bool showFloor = true;

        [SerializeField]
        private bool showPlayArea = true;

        [SerializeField]
        private bool showTrackedArea = true;

        [SerializeField]
        private bool showBoundaryWalls = true;

        [SerializeField]
        private bool showBoundaryCeiling = true;

        #region MonoBehaviour Implementation

        private void Awake()
        {
            markerParent = new GameObject();
            markerParent.name = "Boundary Demo Markers";
            markerParent.transform.parent = MixedRealityToolkit.Instance.MixedRealityPlayspace;
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

        private void Update()
        {
            if (MixedRealityToolkit.BoundarySystem != null)
            {
                MixedRealityToolkit.BoundarySystem.ShowFloor = showFloor;
                MixedRealityToolkit.BoundarySystem.ShowPlayArea = showPlayArea;
                MixedRealityToolkit.BoundarySystem.ShowTrackedArea = showTrackedArea;
                MixedRealityToolkit.BoundarySystem.ShowBoundaryWalls = showBoundaryWalls;
                MixedRealityToolkit.BoundarySystem.ShowBoundaryCeiling = showBoundaryCeiling;
            }
        }

        private async void OnEnable()
        {
            await new WaitUntil(() => MixedRealityToolkit.BoundarySystem != null);
            MixedRealityToolkit.BoundarySystem.Register(gameObject);
        }

        private void OnDisable()
        {
            MixedRealityToolkit.BoundarySystem?.Unregister(gameObject);
        }

        #endregion MonoBehaviour Implementation

        #region IMixedRealityBoundaryHandler Implementation

        /// <inheritdoc />
        public void OnBoundaryVisualizationChanged(BoundaryEventData eventData)
        {
            Debug.Log("[BoundaryVisualizationDemo] Boundary visualization changed.");
        }

        #endregion IMixedRealityBoundaryHandler Implementation

        /// <summary>
        /// Displays the boundary as an array of spheres where spheres in the
        /// bounds are a different color.
        /// </summary>
        private void AddMarkers()
        {
            // Get the rectangular bounds.

            if (!MixedRealityToolkit.BoundarySystem.TryGetRectangularBoundsParams(out Vector2 centerRect, out float angleRect, out float widthRect, out float heightRect))
            {
                // If we have no boundary manager or rectangular bounds we will show no indicators
                return;
            }

            MixedRealityBoundaryVisualizationProfile visualizationProfile = MixedRealityToolkit.Instance.ActiveProfile.BoundaryVisualizationProfile;
            if (visualizationProfile == null)
            {
                // We do not have a visualization profile configured, therefore do not render the indicators.
                return;
            }

            const int indicatorCount = 20;
            const float indicatorDistance = 0.2f;
            const float indicatorScale = 0.1f;
            const float dimension = indicatorCount * indicatorDistance;

            Vector3 center = new Vector3(centerRect.x, 0f, centerRect.y);
            Vector3 corner = center - (new Vector3(dimension, 0.0f, dimension) * 0.5f);

            corner.y += 0.05f;
            for (int xIndex = 0; xIndex < indicatorCount; ++xIndex)
            {
                for (int yIndex = 0; yIndex < indicatorCount; ++yIndex)
                {
                    Vector3 offset = new Vector3(xIndex * indicatorDistance, 0.0f, yIndex * indicatorDistance);
                    Vector3 position = corner + offset;

                    Material material = null;
                    // Check inscribed rectangle first
                    if (MixedRealityToolkit.BoundarySystem.Contains(position, Boundary.Type.PlayArea))
                    {
                        material = visualizationProfile.PlayAreaMaterial;
                    }
                    // Then check geometry
                    else if (MixedRealityToolkit.BoundarySystem.Contains(position, Boundary.Type.TrackedArea))
                    {
                        material = visualizationProfile.TrackedAreaMaterial;
                    }

                    if (material != null)
                    {
                        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        marker.name = "Boundary Demo Marker";
                        marker.transform.parent = markerParent.transform;
                        marker.transform.position = position;
                        marker.transform.localScale = Vector3.one * indicatorScale;
                        marker.GetComponent<MeshRenderer>().sharedMaterial = material;
                        markers.Add(marker);
                    }
                }
            }
        }
    }
}
