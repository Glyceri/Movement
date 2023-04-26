using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Object), true), CanEditMultipleObjects]
public class BaseEditor : Editor
{
    ButtonEditor buttonEditor;
    public virtual void OnEnable()
    {
        buttonEditor = new ButtonEditor(target);
    }

    public sealed override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        buttonEditor.Draw(targets);
    }
}
