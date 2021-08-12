// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace XRTK.Examples.ExamplesHub.UI
{
    public class UIExampleListItem : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Text element used to display the item title.")]
        private TMPro.TextMeshProUGUI titleText = null;

        private UIExamplesHub uiController;

        /// <summary>
        /// Gets the <see cref="ExamplesHub.Example"/> item represented by this UI element.
        /// </summary>
        public Example Example { get; private set; }

        /// <summary>
        /// Initializes the list item.
        /// </summary>
        /// <param name="ui">Reference to the main UI controller.</param>
        /// <param name="example">The example to display using this item.</param>
        public void Initialize(UIExamplesHub ui, Example example)
        {
            uiController = ui;
            Example = example;
            titleText.text = example.Name;
        }

        /// <summary>
        /// The example was clicked.
        /// </summary>
        public void OnClick() => uiController.UIExampleListItem_Selected(this);
    }
}
