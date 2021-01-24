// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using XRTK.Definitions.Utilities;
using XRTK.EventDatum.Input;
using XRTK.Interfaces.InputSystem.Handlers;
using XRTK.Services;
using UnityEngine;
using XRTK.Extensions;
using XRTK.Interfaces.InputSystem;
using XRTK.SDK.Utilities.Solvers;

namespace XRTK.Examples.Demos
{
    /// <summary>
    /// This class is used in the SolverExamples scene, used to swap between active solvers
    /// and placeholder solvers displayed in the scene.
    /// </summary>
    public class SwapVolume : MonoBehaviour, IMixedRealityPointerHandler
    {
        [SerializeField]
        [Tooltip("The scene object to be hidden when the active solver is enabled.")]
        private GameObject hideThisObject = null;

        [SerializeField]
        [Tooltip("The solver prefab to be spawned and used when this volume is activated.")]
        private GameObject spawnThisPrefab = null;

        [SerializeField]
        [Tooltip("Whether to update the solver's target to be the controller that clicked on the volume or not.")]
        private bool updateSolverTargetToClickSource = true;

        private SolverHandler solverHandler;
        private GameObject spawnedObject;

        private void Awake()
        {
            // This example script depends on both GameObjects being properly set.
            if (hideThisObject.IsNull() || spawnThisPrefab.IsNull())
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            spawnedObject = Instantiate(spawnThisPrefab, hideThisObject.transform.position, hideThisObject.transform.rotation);
            spawnedObject.SetActive(false);
            solverHandler = spawnedObject.GetComponent<SolverHandler>();
        }

        /// <inheritdoc />
        public void OnPointerUp(MixedRealityPointerEventData eventData) { }

        /// <inheritdoc />
        public void OnPointerDown(MixedRealityPointerEventData eventData) { }

        /// <inheritdoc />
        public void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            if (spawnedObject.activeSelf)
            {
                spawnedObject.SetActive(false);
                hideThisObject.SetActive(true);
            }
            else
            {
                spawnedObject.SetActive(true);

                if (updateSolverTargetToClickSource && !solverHandler.IsNull())
                {
                    if (MixedRealityToolkit.TryGetSystem<IMixedRealityInputSystem>(out var inputSystem) &&
                        inputSystem.TryGetController(eventData.InputSource, out var controller))
                    {
                        switch (controller.ControllerHandedness)
                        {
                            case Handedness.Right:
                                solverHandler.TrackedObjectToReference = TrackedObjectType.RightHandOrController;
                                break;
                            case Handedness.Left:
                                solverHandler.TrackedObjectToReference = TrackedObjectType.LeftHandOrController;
                                break;
                        }
                    }
                }

                hideThisObject.SetActive(false);
            }

            eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
        }

        private void OnDestroy()
        {
            spawnedObject.Destroy();
            hideThisObject.Destroy();
        }
    }
}
