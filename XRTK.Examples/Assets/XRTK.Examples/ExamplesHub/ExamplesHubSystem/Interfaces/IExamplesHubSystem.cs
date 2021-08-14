// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using XRTK.Examples.ExamplesHub.Definitions;
using XRTK.Interfaces;

namespace XRTK.Examples.ExamplesHub.Interfaces
{
    public interface IExamplesHubSystem : IMixedRealitySystem
    {
        /// <summary>
        /// Gets a list of examples supported on the current runtime platform.
        /// </summary>
        IReadOnlyList<ExampleProfile> SupportedExamples { get; }

        /// <summary>
        /// Loads the specified <see cref="Example"/>.
        /// </summary>
        /// <param name="example">The <see cref="Example"/> to load.</param>
        void LoadExample(ExampleProfile example);

        /// <summary>
        /// Exits the currently loaded example and returns to example selection.
        /// </summary>
        void ExitExample();
    }
}
