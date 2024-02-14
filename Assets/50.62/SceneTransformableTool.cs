using UnityEngine;

public class SceneTransformableTool : ScriptableObject
{
    [SerializeField] RandomRotationTool randomRotationTool;
    [SerializeField] RandomScaleTool randomScaleTool;
    [SerializeField] ResetRotationAndScaleTool resetRotationAndScaleTool;

    public void DrawAll()
    {
        randomRotationTool.Draw();
        randomScaleTool.Draw();
        resetRotationAndScaleTool.Draw();
    }
}
