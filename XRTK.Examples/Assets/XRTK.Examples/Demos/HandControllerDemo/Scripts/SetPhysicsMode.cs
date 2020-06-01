using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XRTK.Definitions.Controllers.Hands;
using XRTK.Interfaces.Providers.Controllers.Hands;
using XRTK.Services;

namespace XRTK.Examples.Demos.HandController
{
    public class SetPhysicsMode : MonoBehaviour
    {
        private Vector3[] startPositions;
        private Quaternion[] startRotations;
        private List<IMixedRealityHandControllerDataProvider> providers;

        [SerializeField]
        private Transform[] objects = null;

        [SerializeField]
        private Toggle physicsToggle = null;

        private void Start()
        {
            startPositions = new Vector3[objects.Length];
            startRotations = new Quaternion[objects.Length];
            providers = MixedRealityToolkit.GetActiveServices<IMixedRealityHandControllerDataProvider>();
            physicsToggle.isOn = providers[0].HandPhysicsEnabled;

            for (int i = 0; i < objects.Length; i++)
            {
                startPositions[i] = objects[i].position;
                startRotations[i] = objects[i].rotation;
            }
        }

        public void TogglePhysics(bool isOn)
        {
            if (isOn)
            {
                EnablePhysics();
            }
            else
            {
                DisablePhysics();
            }
        }

        private void EnablePhysics()
        {
            for (int i = 0; i < providers.Count; i++)
            {
                providers[i].HandPhysicsEnabled = true;
            }
        }

        private void DisablePhysics()
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].position = startPositions[i];
                objects[i].rotation = startRotations[i];
            }

            for (int i = 0; i < providers.Count; i++)
            {
                providers[i].HandPhysicsEnabled = false;
            }
        }

        public void SetHandBoundsMode()
        {
            for (int i = 0; i < providers.Count; i++)
            {
                providers[i].BoundsMode = HandBoundsMode.Hand;
            }
        }

        public void SetFingerBoundsMode()
        {
            for (int i = 0; i < providers.Count; i++)
            {
                providers[i].BoundsMode = HandBoundsMode.Fingers;
            }
        }
    }
}