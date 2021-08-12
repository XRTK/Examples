// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor;
using XRTK.Editor.Profiles;
using XRTK.Examples.ExamplesHub.Definitions;

namespace XRTK.Examples.Editor
{
    [CustomEditor(typeof(ExamplesHubSystemProfile))]
    public class ExamplesHubSystemProfileInspector : MixedRealityServiceProfileInspector
    {
        private SerializedProperty uiPrefab;
        private SerializedProperty examples;

        protected override void OnEnable()
        {
            base.OnEnable();

            uiPrefab = serializedObject.FindProperty(nameof(uiPrefab));
            examples = serializedObject.FindProperty(nameof(examples));
        }

        public override void OnInspectorGUI()
        {
            RenderHeader("Configuration profile for the Examples Hub system.");

            serializedObject.Update();

            EditorGUILayout.PropertyField(uiPrefab);
            EditorGUILayout.PropertyField(examples);

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}
