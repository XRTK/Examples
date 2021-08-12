// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.Attributes;
using XRTK.Examples.ExamplesHub.Interfaces;
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
        [Prefab]
        [Tooltip("Examples list item prefab.")]
        private GameObject exampleListItemPrefab = null;

        private IExamplesHubSystem examplesHubSystem;

        private void Start()
        {
            if (!MixedRealityToolkit.TryGetSystem(out examplesHubSystem))
            {
                Debug.LogError($"Examples Hub system not found.");
                return;
            }

            var examples = examplesHubSystem.Examples;
            for (var i = 0; i < examples.Count; i++)
            {
                var listItem = Instantiate(exampleListItemPrefab).GetComponent<UIExampleListItem>();
                listItem.transform.SetParent(scrollViewContent, false);
                listItem.Initialize(this, examples[i]);
            }
        }

        public void UIExampleListItem_Selected(UIExampleListItem item)
        {
            examplesHubSystem.LoadExample(item.Example);
        }

        public void UIExampleListItem_Hover(UIExampleListItem item)
        {
            descriptionText.text = item.Example.Description;
        }
    }
}
