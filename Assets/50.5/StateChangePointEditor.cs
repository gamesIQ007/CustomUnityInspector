using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StateChangePoint))]
public class StateChangePointEditor : Editor
{
    private SerializedProperty stateProp;
    private SerializedProperty movementTargetProp;
    private SerializedProperty waitTimeProp;
    private SerializedProperty exploreAroundRadiusProp;

    private StateChangePoint trigger;

    private void OnEnable()
    {
        stateProp = serializedObject.FindProperty("nextState");
        movementTargetProp = serializedObject.FindProperty("movementTarget");
        waitTimeProp = serializedObject.FindProperty("waitTime");
        exploreAroundRadiusProp = serializedObject.FindProperty("exploreAroundRadius");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(stateProp);

        StateType state = (StateType)stateProp.enumValueIndex;

        if (state == StateType.MoveTo)
        {
            EditorGUILayout.PropertyField(movementTargetProp);
        }

        if (state == StateType.Stay)
        {
            EditorGUILayout.PropertyField(waitTimeProp);
        }

        if (state == StateType.ExploreAround)
        {
            EditorGUILayout.PropertyField(exploreAroundRadiusProp);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        trigger = (StateChangePoint)target;

        StateType state = (StateType)stateProp.enumValueIndex;

        if (state == StateType.MoveTo)
        {
            EditorGUI.BeginChangeCheck();

            Vector3 targetPos = Handles.PositionHandle(movementTargetProp.vector3Value, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                movementTargetProp.vector3Value = targetPos;

                serializedObject.ApplyModifiedProperties();
            }

            Handles.DrawLine(trigger.transform.position, movementTargetProp.vector3Value);
        }

        if (state == StateType.ExploreAround)
        {
            Handles.color = new Color(1, 1, 0, 0.2f);
            Handles.DrawSolidDisc(trigger.transform.position, Vector3.up, exploreAroundRadiusProp.floatValue);
        }
    }
}
