using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelEditor levelEditor = (LevelEditor)target;
        GUILayout.Label("Attacks:");
        if (GUILayout.Button("Add New Bullet"))
        {
            levelEditor.AddBullet();
        }
        if (GUILayout.Button("Add New Disco Attack"))
        {
            levelEditor.AddDisco();
        }
        GUILayout.Label("");
        if (GUILayout.Button("Apply to Song Object"))
        {
            levelEditor.ApplyToSong();
        }
    }
}
