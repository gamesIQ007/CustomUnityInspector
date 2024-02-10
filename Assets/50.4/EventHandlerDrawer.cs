using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EventHandler))]
public class EventHandlerDrawer : PropertyDrawer
{
    private SerializedProperty actionProp;
    private EventHandler eventHandler;
    private Color headerBackgroundColor = new Color(0.8f, 0.8f, 0.8f);

    private void FindProperties(SerializedProperty property)
    {
        actionProp = property.FindPropertyRelative("actions");
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        FindProperties(property);

        float actionsHeigth = 0;

        SerializedProperty actions = actionProp;

        for (int i = 0; i < actions.arraySize; i++)
        {
            SerializedProperty currentAction = actions.GetArrayElementAtIndex(i);

            actionsHeigth += EditorGUI.GetPropertyHeight(currentAction) + EditorGUIUtility.standardVerticalSpacing;
        }

        return actionsHeigth + EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        FindProperties(property);

        eventHandler = (EventHandler)fieldInfo.GetValue(property.serializedObject.targetObject);

        // Рисуем заголовок
        Rect headerRect = position;
        headerRect.height = EditorGUIUtility.singleLineHeight;
        Color prevColor = GUI.backgroundColor;
        GUI.backgroundColor = headerBackgroundColor;
        GUI.Box(headerRect, GUIContent.none);
        GUI.backgroundColor = prevColor;

        EditorGUI.LabelField(headerRect, label, EditorStyles.boldLabel);

        // Рисуем действия
        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        EditorGUI.indentLevel++;

        for (int i = 0; i < actionProp.arraySize; i++)
        {
            SerializedProperty currentAction = actionProp.GetArrayElementAtIndex(i);

            position.height = EditorGUI.GetPropertyHeight(currentAction);

            EditorGUI.BeginProperty(position, new GUIContent("Actions"), currentAction);

                EditorGUI.PropertyField(position, currentAction, new GUIContent("Actions"), true);

            EditorGUI.EndProperty();

            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
        }

        EditorGUI.indentLevel--;

        // Рисуем кнопку
        Rect buttonRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        if (EditorGUI.DropdownButton(buttonRect, new GUIContent("Add action"), FocusType.Passive))
        {
            BuildAddActionMenu().DropDown(buttonRect);
        }
    }

    private List<Type> GetAllSubclassOf(Type parent)
    {
        List<Type> types = new List<Type>();

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        for (int i = 0; i < assemblies.Length; i++)
        {
            Type[] allTypes = assemblies[i].GetTypes();

            for (int j = 0; j < allTypes.Length; j++)
            {
                if (allTypes[j].IsSubclassOf(parent))
                {
                    types.Add(allTypes[j]);
                }
            }
        }

        return types;
    }

    private string[] GetActionPath()
    {
        List<string> allStrings = new List<string>();

        List<Type> allActionClasses = GetAllSubclassOf(typeof(ActionBase));

        for (int i = 0; i < allActionClasses.Count; i++)
        {
            object[] allAttributes = allActionClasses[i].GetCustomAttributes(false);

            for (int j = 0; j < allAttributes.Length; j++)
            {
                if (allAttributes[j] is ActionPathAttribute actionPathAttribute)
                {
                    allStrings.Add(actionPathAttribute.Path);
                }
            }
        }

        return allStrings.ToArray();
    }

    private GenericMenu BuildAddActionMenu()
    {
        GenericMenu menu = new GenericMenu();

        string[] allActionPaths = GetActionPath();

        List<Type> allActionClasses = GetAllSubclassOf(typeof(ActionBase));

        for (int i = 0; i < allActionPaths.Length; i++)
        {
            menu.AddItem(new GUIContent(allActionPaths[i]), false, AddAction, allActionClasses[i]);
        }

        return menu;
    }

    private void AddAction(object actionType)
    {
        if (actionType is Type type)
        {
            if (eventHandler != null)
            {
                eventHandler.AddAction(type);
            }
        }
    }
}
