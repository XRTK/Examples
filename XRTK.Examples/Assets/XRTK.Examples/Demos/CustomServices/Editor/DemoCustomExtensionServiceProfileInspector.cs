// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor;
using XRTK.Editor.Profiles;
using XRTK.Examples.Demos.CustomExtensionServices;

namespace XRTK.Examples.Demos.CustomServices.Editor
{
    /// <summary>
    /// This class demonstrates how to expose serialized fields into custom service profiles.
    /// </summary>
    [CustomEditor(typeof(DemoCustomExtensionServiceProfile))]
    public class DemoCustomExtensionServiceProfileInspector : MixedRealityRegisteredServiceProvidersProfileInspector
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
