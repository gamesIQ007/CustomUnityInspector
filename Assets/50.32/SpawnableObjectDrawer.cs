using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SpawnableObject))]
public class SpawnableObjectDrawer : PropertyDrawer
{
    private SerializedProperty gameObjectProp;
    private SerializedProperty chanceProp;

    public void FindProperties(SerializedProperty property)
    {
        gameObjectProp = property.FindPropertyRelative("gameObject");
        chanceProp = property.FindPropertyRelative("chance");
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        FindProperties(property);

        //EditorGUI.LabelField(position, label);

        Rect titleRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        Rect objectFieldRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width * 0.5f, EditorGUIUtility.singleLineHeight);
        Rect chanceFieldRect = new Rect(position.x + position.width * 0.55f, position.y + EditorGUIUtility.singleLineHeight, position.width * 0.45f, EditorGUIUtility.singleLineHeight);

        //GUI.Box(objectFieldRect, "objectFieldRect");
        //GUI.Box(chanceFieldRect, "chanceFieldRect");

        EditorGUI.LabelField(titleRect, label, EditorStyles.boldLabel);
        EditorGUI.ObjectField(objectFieldRect, gameObjectProp, GUIContent.none);
        EditorGUI.Slider(chanceFieldRect, chanceProp, 0, 1, GUIContent.none);
    }
}
