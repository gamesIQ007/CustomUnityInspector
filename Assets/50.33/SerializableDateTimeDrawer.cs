using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SerializableDateTime))]
public class SerializableDateTimeDrawer : PropertyDrawer
{
    private SerializedProperty yearProp;
    private SerializedProperty monthProp;
    private SerializedProperty dayProp;
    private SerializedProperty hourProp;
    private SerializedProperty minuteProp;
    private SerializedProperty secondProp;

    public void FindProperties(SerializedProperty property)
    {
        yearProp = property.FindPropertyRelative("year");
        monthProp = property.FindPropertyRelative("month");
        dayProp = property.FindPropertyRelative("day");
        hourProp = property.FindPropertyRelative("hour");
        minuteProp = property.FindPropertyRelative("minute");
        secondProp = property.FindPropertyRelative("second");
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        FindProperties(property);

        Rect dateRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        Rect timeRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);

        int[] date = new int[3] { yearProp.intValue, monthProp.intValue, dayProp.intValue };
        int[] time = new int[3] { hourProp.intValue, minuteProp.intValue, secondProp.intValue };

        EditorGUI.BeginProperty(position, null, property);

        EditorGUI.BeginChangeCheck();

        EditorGUI.MultiIntField(dateRect, new GUIContent[] { new GUIContent("Year"), new GUIContent("Month"), new GUIContent("Day") }, date);
        EditorGUI.MultiIntField(timeRect, new GUIContent[] { new GUIContent("Hour"), new GUIContent("Minute"), new GUIContent("Second") }, time);

        if (EditorGUI.EndChangeCheck())
        {
            yearProp.intValue = date[0];
            monthProp.intValue = date[1];
            dayProp.intValue = date[2];
            hourProp.intValue = time[0];
            minuteProp.intValue = time[1];
            secondProp.intValue = time[2];
        }

        EditorGUI.EndProperty();
    }
}
