// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.UI;
using XRTK.Attributes;
using XRTK.Examples.ExamplesHub.Interfaces;
using XRTK.Extensions;
using XRTK.Services;

namespace XRTK.Examples.ExamplesHub.UI
{
    /// <summary>
    /// UI controller for example selection.
    /// </summary>
    public class UIExamplesHub : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Text element used to render example descriptions.")]
        private TMPro.TextMeshProUGUI descriptionText = null;

        [SerializeField]
        [Tooltip("Scroll view root transform containing all list items.")]
        private Transform scrollViewContent = null;

        [SerializeField]
        [Tooltip("Reference to the example launch button.")]
        private Button launchExampleButton = null;

        [SerializeField]
        [Prefab]
        [Tooltip("Examples list item prefab.")]
        private GameObject exampleListItemPrefab = null;

        private IExamplesHubSystem examplesHubSystem;
        private UIExampleListItem selectedExample;

        private void Start()
        {
            if (!MixedRealityToolkit.TryGetSystem(out examplesHubSystem))
            {
                Debug.LogError($"Examples Hub system not found.");
                return;
            }

            launchExampleButton.interactable = false;
            var examples = examplesHubSystem.SupportedExamples;
            for (var i = 0; i < examples.Count; i++)
            {
                var listItem = Instantiate(exampleListItemPrefab).GetComponent<UIExampleListItem>();
                listItem.transform.SetParent(scrollViewContent, false);
                listItem.Initialize(this, examples[i]);
            }
        }

        /// <summary>
        /// An example was selected in the examples list view.
        /// </summary>
        /// <param name="item">The selected example item.</param>
        public void UIExampleListItem_Selected(UIExampleListItem item)
        {
            selectedExample = item;
            launchExampleButton.interactable = true;
            descriptionText.text = item.Example.Description;
        }

        /// <summary>
        /// Loads the currently selected example.
        /// </summary>
        public void LaunchExample_Click()
        {
            if (!selectedExample.IsNull())
            {
                examplesHubSystem.LoadExample(selectedExample.Example);
            }
        }
    }
}
