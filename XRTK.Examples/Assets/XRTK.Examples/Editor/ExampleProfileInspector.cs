// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor;
using UnityEngine;
using XRTK.Editor.Profiles;
using XRTK.Examples.ExamplesHub.Definitions;

namespace XRTK.Examples.Editor
{
    [CustomEditor(typeof(ExampleProfile))]
    public class ExampleProfileInspector : BaseMixedRealityProfileInspector
    {
        private SerializedProperty title;
        private SerializedProperty description;
        private SerializedProperty sceneName;
        private SerializedProperty docsUrl;
        private SerializedProperty platformEntries;

        protected override void OnEnable()
        {
            base.OnEnable();

            title = serializedObject.FindProperty(nameof(title));
            description = serializedObject.FindProperty(nameof(description));
            sceneName = serializedObject.FindProperty(nameof(sceneName));
            docsUrl = serializedObject.FindProperty(nameof(docsUrl));
            platformEntries = serializedObject.FindProperty(nameof(platformEntries));
        }

        public override void OnInspectorGUI()
        {
            RenderHeader("Configuration profile for an XRTK example.");

            serializedObject.Update();

            EditorGUILayout.PropertyField(title);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Description");
            var style = new GUIStyle(EditorStyles.textArea);
            style.wordWrap = true;
            description.stringValue = EditorGUILayout.TextArea(description.stringValue, style);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(sceneName);
            EditorGUILayout.PropertyField(docsUrl);
            EditorGUILayout.PropertyField(platformEntries);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
