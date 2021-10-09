// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using XRTK.Examples.ExamplesHub.Definitions;
using XRTK.Examples.ExamplesHub.Interfaces;
using XRTK.Services;

#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif

namespace XRTK.Examples.ExamplesHub
{
    [System.Runtime.InteropServices.Guid("6191eb65-ec4c-434a-9a43-939fbc398582")]
    public class ExamplesHubSystem : BaseSystem, IExamplesHubSystem
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="profile">System configuration profile.</param>
        public ExamplesHubSystem(ExamplesHubSystemProfile profile)
            : base(profile)
        {
            uiPrefab = profile.UIPrefab;
            allExamples = profile.Examples;
        }

        private readonly GameObject uiPrefab;
        private readonly IReadOnlyList<ExampleProfile> allExamples;
        private Scene? currentExampleScene;

        /// <summary>
        /// Game object reference to the spawned instance of <see cref="uiPrefab"/>.
        /// </summary>
        private GameObject ExamplesUI { get; set; }

        /// <inheritdoc />
        public IReadOnlyList<ExampleProfile> SupportedExamples { get; private set; }

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();

#if UNITY_EDITOR
            // When in editor and the system gets initialized we want to make sure
            // all the examples can run in play mode and in a build, so we have to
            // add them to the build config.
            AddExampleScenesToBuildConfiguration();
#endif

            if (!Application.isPlaying)
            {
                return;
            }

            // When in editor we want to check if the developer has
            // already loaded more than the base scene in which case we the system shouldn't do anything
            // since the developer is probably working with a multi-scene setup for development.
            if (Application.isEditor && SceneManager.sceneCount > 1)
            {
                return;
            }

            // Filter for examples supported by the current platform.
            var compatibleExamples = new List<ExampleProfile>();
            for (var i = 0; i < allExamples.Count; i++)
            {
                var example = allExamples[i];
                for (int j = 0; j < example.RuntimePlatforms.Count; j++)
                {
                    var platform = example.RuntimePlatforms[j];
                    if (platform.IsAvailable)
                    {
                        compatibleExamples.Add(example);
                        break;
                    }
                }
            }
            SupportedExamples = compatibleExamples;

            // Initialize examples hub UI and events.
            ExamplesUI = Object.Instantiate(uiPrefab);
            SceneManager.sceneLoaded += SceneManager_SceneLoaded;
            SceneManager.sceneUnloaded += SceneManager_SceneUnloaded;
        }

        /// <inheritdoc />
        public override void Destroy()
        {
            if (Application.isPlaying)
            {
                SceneManager.sceneLoaded -= SceneManager_SceneLoaded;
                SceneManager.sceneUnloaded -= SceneManager_SceneUnloaded;
            }

            base.Destroy();
        }

        /// <inheritdoc />
        public void LoadExample(ExampleProfile example)
        {
            ExamplesUI.SetActive(false);
            SceneManager.LoadSceneAsync(example.SceneName, LoadSceneMode.Additive);
        }

        /// <inheritdoc />
        public void ExitExample()
        {
            if (!currentExampleScene.HasValue)
            {
                return;
            }

            SceneManager.UnloadSceneAsync(currentExampleScene.Value.buildIndex);
        }

        private void SceneManager_SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 0)
            {
                // We don't care for the base scene loading. The base scene will always load
                // first when the application launches.
                return;
            }

            Assert.IsTrue(mode == LoadSceneMode.Additive, $"Only {nameof(LoadSceneMode.Additive)} scene loading is allowed!");
            currentExampleScene = scene;

            // While we have an exmaple scene loaded we want the examples selection to disappear.
            ExamplesUI.SetActive(false);
        }

        private void SceneManager_SceneUnloaded(Scene scene)
        {
            if (currentExampleScene.HasValue && scene.buildIndex == currentExampleScene.Value.buildIndex)
            {
                // The currently loaded example was unloaded. Return to example selection.
                currentExampleScene = null;
                ExamplesUI.SetActive(true);
            }
        }

#if UNITY_EDITOR
        private void AddExampleScenesToBuildConfiguration()
        {
            var scenes = EditorBuildSettings.scenes;
            var updatedBuildScenes = new Dictionary<string, EditorBuildSettingsScene>();
            for (var i = 0; i < scenes.Length; i++)
            {
                var scene = scenes[i];
                updatedBuildScenes.Add(scene.path, scene);
            }

            for (var i = 0; i < allExamples.Count; i++)
            {
                var example = allExamples[i];
                var results = AssetDatabase.FindAssets($"t:Scene {example.SceneName}");
                if (results.Length > 0)
                {
                    var sceneAssetGUID = results[0];
                    var sceneAssetPath = AssetDatabase.GUIDToAssetPath(sceneAssetGUID);
                    if (!updatedBuildScenes.ContainsKey(sceneAssetPath))
                    {
                        updatedBuildScenes.Add(sceneAssetPath, new EditorBuildSettingsScene(sceneAssetPath, true));
                    }
                }
            }

            EditorBuildSettings.scenes = updatedBuildScenes.Values.ToArray();
        }
#endif
    }
}
