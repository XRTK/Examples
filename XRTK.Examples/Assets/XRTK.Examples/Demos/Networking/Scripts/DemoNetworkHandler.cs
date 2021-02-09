// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using XRTK.EventDatum.Networking;
using XRTK.Interfaces.NetworkingSystem.Handlers;
using XRTK.Services;
using UnityEngine;
using XRTK.Interfaces.NetworkingSystem;

public class DemoNetworkHandler : MonoBehaviour,
    IMixedRealityNetworkingHandler<float>,
    IMixedRealityNetworkingHandler<string>
{
    private IMixedRealityNetworkingSystem networkingSystem;

    private IMixedRealityNetworkingSystem NetworkingSystem
    {
        get
        {
            if (networkingSystem == null)
            {
                MixedRealityToolkit.TryGetSystem<IMixedRealityNetworkingSystem>(out networkingSystem);
            }

            return networkingSystem;
        }
    }

    private void Start()
    {
        NetworkingSystem.Register(gameObject);
        NetworkingSystem.SendData("Hi");
        NetworkingSystem.SendData(5f);
        NetworkingSystem.SendData(Vector3.zero);
    }

    /// <inheritdoc />
    public void OnDataReceived(BaseNetworkingEventData<float> eventData)
    {
    }

    /// <inheritdoc />
    public void OnDataReceived(BaseNetworkingEventData<string> eventData)
    {
    }
}