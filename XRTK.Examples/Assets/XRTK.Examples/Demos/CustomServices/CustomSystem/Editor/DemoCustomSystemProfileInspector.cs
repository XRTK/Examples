// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor;
using XRTK.Editor.Profiles;

namespace XRTK.Examples.Demos.CustomServices.Editor
{
    /// <summary>
    /// This class demonstrates how to expose serialized fields into custom system profiles.
    /// </summary>
    [CustomEditor(typeof(DemoCustomSystemProfile))]
    public class DemoCustomSystemProfileInspector : MixedRealityServiceProfileInspector
    {
        private SerializedProperty myCustomStringData;

        protected override void OnEnable()
        {
            // Initialize the base class properties
            base.OnEnable();

            myCustomStringData = serializedObject.FindProperty(nameof(myCustomStringData));
        }

        public override void OnInspectorGUI()
        {
            // Call into the base class to render the data provider list
            base.OnInspectorGUI();

            serializedObject.Update();
            EditorGUILayout.PropertyField(myCustomStringData);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
