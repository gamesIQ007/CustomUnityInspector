using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ActionBase), true)]
public class ActionBaseDrawer : PropertyDrawer
{
    private Color headerBackgroundColor = new Color(0.8f, 0.8f, 0.8f);

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = 0;

        SerializedProperty[] visibleProperties = GetVisibleSerializedProperties(property);

        for (int i = 0; i < visibleProperties.Length; i++)
        {
            height += EditorGUI.GetPropertyHeight(visibleProperties[i]) + EditorGUIUtility.standardVerticalSpacing;
        }

        return height + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Нарисовать блок заднего фона
        Rect backgroundBoxRect = EditorGUI.IndentedRect(position);
        GUI.Box(backgroundBoxRect, GUIContent.none);

        // Нарисовать блок с заголовком
        Rect titleBoxRect = EditorGUI.IndentedRect(position);
        titleBoxRect.height = EditorGUIUtility.singleLineHeight;
        Color prevColor = GUI.backgroundColor;
        GUI.backgroundColor = headerBackgroundColor;
        GUI.Box(titleBoxRect, GUIContent.none);
        GUI.backgroundColor = prevColor;

        // Нарисовать заголовок
        Rect labelRect = position;
        labelRect.height = EditorGUIUtility.singleLineHeight;
        GUI.Box(labelRect, label + "", EditorStyles.boldLabel);

        // Нарисовать свойства
        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        EditorGUI.indentLevel++;

        SerializedProperty[] visibleProperties = GetVisibleSerializedProperties(property);
        for (int i = 0; i < visibleProperties.Length; i++)
        {
            position.height = EditorGUI.GetPropertyHeight(visibleProperties[i]);

            EditorGUI.PropertyField(position, visibleProperties[i]);

            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
        }

        EditorGUI.indentLevel--;
    }

    private SerializedProperty[] GetVisibleSerializedProperties(SerializedProperty property)
    {
        List<SerializedProperty> allProperties = new List<SerializedProperty>();

        SerializedProperty endProperty = property.GetEndProperty();

        if (property.NextVisible(true))
        {
            do
            {
                if (SerializedProperty.EqualContents(property, endProperty)) break;

                allProperties.Add(property.Copy());
            }
            while (property.NextVisible(false));
        }

        return allProperties.ToArray();
    }
}
