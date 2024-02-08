using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SerializeExamplerTool : MonoBehaviour
{
    [MenuItem("Example/SetValue")]
    public static void SetValue()
    {
        SerializeExample example = GameObject.FindObjectOfType<SerializeExample>();

        SerializedObject serializedObject = new SerializedObject(example);

        SerializedProperty serializedProperty = serializedObject.FindProperty("value");

        serializedProperty.floatValue = 100;

        serializedObject.ApplyModifiedProperties();
    }
}
