// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using XRTK.Interfaces;

namespace XRTK.Examples.ExamplesHub.Interfaces
{
    public interface IExamplesHubSystem : IMixedRealitySystem
    {
        /// <summary>
        /// Gets a list of available examples.
        /// </summary>
        IReadOnlyList<Example> Examples { get; }

        /// <summary>
        /// Loads the specified <see cref="Example"/>.
        /// </summary>
        /// <param name="example">The <see cref="Example"/> to load.</param>
        void LoadExample(Example example);

        /// <summary>
        /// Exits the currently loaded example and returns to example selection.
        /// </summary>
        void ExitExample();
    }
}
