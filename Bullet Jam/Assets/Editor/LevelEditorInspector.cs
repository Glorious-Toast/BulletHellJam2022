using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
        if (levelEditor.cueSync)
        {
            if (GUILayout.Button("Disable Cue Sync"))
            {
                levelEditor.cueSync = false;
            }
            levelEditor.syncedCue = EditorGUILayout.TextField("Cue: ", levelEditor.syncedCue);
            levelEditor.beatDenominator = EditorGUILayout.IntField("Beat Denominator Steps: ", levelEditor.beatDenominator);
        } else
        {
            if (GUILayout.Button("Enable Cue Sync"))
            {
                levelEditor.cueSync = true;
            }
        }
        GUILayout.Label("");
        if (GUILayout.Button("Apply to Song Object"))
        {
            levelEditor.ApplyToSong();
        }
    }
}
