// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace XRTK.Examples.ExamplesHub
{
    [Serializable]
    public class Example
    {
        [SerializeField]
        private string name = null;

        /// <summary>
        /// The name of the example.
        /// </summary>
        public string Name => name;

        [SerializeField]
        private string description = null;

        /// <summary>
        /// The description of the example content.
        /// </summary>
        public string Description => description;

        [SerializeField]
        private string sceneName = null;

        /// <summary>
        /// The name of the scene containing the example content.
        /// </summary>
        public string SceneName => sceneName;
    }
}
