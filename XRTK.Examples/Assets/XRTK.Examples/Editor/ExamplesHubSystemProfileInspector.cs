// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using XRTK.Editor.Profiles;
using XRTK.Examples.ExamplesHub.Definitions;

namespace XRTK.Examples.Editor
{
    [CustomEditor(typeof(ExamplesHubSystemProfile))]
    public class ExamplesHubSystemProfileInspector : MixedRealityServiceProfileInspector
    {
        private SerializedProperty uiPrefab;
        private SerializedProperty examples;
        private ReorderableList examplesList;
        private int currentlySelectedExampleElementIndex;

        protected override void OnEnable()
        {
            base.OnEnable();

            uiPrefab = serializedObject.FindProperty(nameof(uiPrefab));
            examples = serializedObject.FindProperty(nameof(examples));

            examplesList = new ReorderableList(serializedObject, examples, true, false, true, true)
            {
                elementHeight = EditorGUIUtility.singleLineHeight * 1.5f
            };
            examplesList.drawHeaderCallback += ExamplesList_DrawHeaderCallback;
            examplesList.drawElementCallback += ExamplesList_DrawConfigurationOptionElement;
            examplesList.onAddCallback += ExamplesList_OnConfigurationOptionAdded;
            examplesList.onRemoveCallback += ExamplesList_OnConfigurationOptionRemoved;
        }

        public override void OnInspectorGUI()
        {
            RenderHeader("Configuration profile for the Examples Hub system.");

            serializedObject.Update();

            EditorGUILayout.PropertyField(uiPrefab);
            EditorGUILayout.Space();
            examplesList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }

        private void ExamplesList_DrawHeaderCallback(Rect rect)
        {
            EditorGUI.LabelField(rect, "Examples");
        }

        private void ExamplesList_DrawConfigurationOptionElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (isFocused)
            {
                currentlySelectedExampleElementIndex = index;
            }

            rect.height = EditorGUIUtility.singleLineHeight;
            rect.y += 3;
            var exampleDataProperty = examples.GetArrayElementAtIndex(index);
            var selectedExampleData = EditorGUI.ObjectField(rect, exampleDataProperty.objectReferenceValue, typeof(ExampleProfile), false) as ExampleProfile;

            if (selectedExampleData != null)
            {
                selectedExampleData.ParentProfile = ThisProfile;
            }

            exampleDataProperty.objectReferenceValue = selectedExampleData;
        }

        private void ExamplesList_OnConfigurationOptionAdded(ReorderableList list)
        {
            examples.arraySize += 1;
            var index = examples.arraySize - 1;

            var mappingProfileProperty = examples.GetArrayElementAtIndex(index);
            mappingProfileProperty.objectReferenceValue = null;
            serializedObject.ApplyModifiedProperties();
        }

        private void ExamplesList_OnConfigurationOptionRemoved(ReorderableList list)
        {
            if (currentlySelectedExampleElementIndex >= 0)
            {
                examples.DeleteArrayElementAtIndex(currentlySelectedExampleElementIndex);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
