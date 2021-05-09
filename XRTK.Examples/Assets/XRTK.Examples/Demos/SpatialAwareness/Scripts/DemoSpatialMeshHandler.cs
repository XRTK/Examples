// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;
using XRTK.Definitions.SpatialAwarenessSystem;
using XRTK.EventDatum.SpatialAwareness;
using XRTK.Interfaces.SpatialAwarenessSystem;
using XRTK.Interfaces.SpatialAwarenessSystem.Handlers;
using XRTK.Services;
using XRTK.Utilities.Async;

namespace XRTK.Examples.Demos
{
    /// <summary>
    /// This class is an example of the <see cref="IMixedRealitySpatialAwarenessMeshHandler{T}"/> interface. It keeps track
    /// of the IDs of each mesh and tracks the number of updates they have received.
    /// </summary>
    public class DemoSpatialMeshHandler : MonoBehaviour, IMixedRealitySpatialAwarenessMeshHandler<SpatialMeshObject>
    {
        private IMixedRealitySpatialAwarenessSystem spatialAwarenessSystem = null;

        private IMixedRealitySpatialAwarenessSystem SpatialAwarenessSystem
            => spatialAwarenessSystem ?? (spatialAwarenessSystem = MixedRealityToolkit.GetSystem<IMixedRealitySpatialAwarenessSystem>());

        /// <summary>
        /// Collection that tracks the IDs and count of updates for each active spatial awareness mesh.
        /// </summary>
        private readonly Dictionary<Guid, uint> meshUpdateData = new Dictionary<Guid, uint>();

        private async void OnEnable()
        {
            await new WaitUntil(() => SpatialAwarenessSystem != null);
            SpatialAwarenessSystem.Register(gameObject);
        }

        private void OnDisable()
        {
            SpatialAwarenessSystem?.Unregister(gameObject);
        }

        /// <inheritdoc />
        public virtual void OnMeshAdded(MixedRealitySpatialAwarenessEventData<SpatialMeshObject> eventData)
        {
            Debug.Log($"[DemoSpatialMeshHandler.OnMeshAdded] {eventData.EventSource.SourceName}.{eventData.SpatialObject.GameObject.name}");

            // A new mesh has been added.
            if (!meshUpdateData.ContainsKey(eventData.Id))
            {
                Debug.Log($"[DemoSpatialMeshHandler] Started Tracking mesh {eventData.Id}.");
                meshUpdateData.Add(eventData.Id, 0);
            }
        }

        /// <inheritdoc />
        public virtual void OnMeshUpdated(MixedRealitySpatialAwarenessEventData<SpatialMeshObject> eventData)
        {
            // A mesh has been updated. Find it and increment the update count.
            if (meshUpdateData.TryGetValue(eventData.Id, out uint updateCount))
            {
                // Set the new update count.
                meshUpdateData[eventData.Id] = ++updateCount;

                Debug.Log($"[DemoSpatialMeshHandler.OnMeshUpdated] Mesh {eventData.Id} has been updated {updateCount} times.");
            }
        }

        /// <inheritdoc />
        public virtual void OnMeshRemoved(MixedRealitySpatialAwarenessEventData<SpatialMeshObject> eventData)
        {
            Debug.Log($"[DemoSpatialMeshHandler.OnMeshRemoved] {eventData.EventSource.SourceName}.{eventData.SpatialObject.GameObject.name}");

            // A mesh has been removed. We no longer need to track the count of updates.
            if (meshUpdateData.ContainsKey(eventData.Id))
            {
                Debug.Log($"[DemoSpatialMeshHandler] No longer tracking mesh {eventData.Id}.");
                meshUpdateData.Remove(eventData.Id);
            }
        }
    }
}
