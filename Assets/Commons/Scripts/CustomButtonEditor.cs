 #if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.UI;

[CanEditMultipleObjects]
[CustomEditor(typeof(CustomButton), true)]
public class CustomButtonEditor : ButtonEditor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SerializedProperty OnPressed = serializedObject.FindProperty("OnPressed");

        //EditorGUIUtility.LookLikeControls();
        EditorGUILayout.PropertyField(OnPressed);

        serializedObject.ApplyModifiedProperties();
    }
}
 #endif
 