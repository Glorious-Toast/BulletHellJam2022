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
        GUILayout.Label("Edit Song Chart:");
        if (GUILayout.Button("Add New Bullet"))
        {
            levelEditor.AddBullet();
        }
        if (GUILayout.Button("Add New Disco Attack"))
        {
            levelEditor.AddDisco();
        }if (GUILayout.Button("Randomize Disco Tiles"))
        {
            levelEditor.RandomizeDisco();
        }
        levelEditor.randomDiscoAmount = EditorGUILayout.IntField("Number of attack tiles: ", levelEditor.randomDiscoAmount);
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
        if (levelEditor.wrapInFolder)
        {
            if (GUILayout.Button("Disable Folder Wrapping"))
            {
                levelEditor.wrapInFolder = false;
            }
            levelEditor.folderName = EditorGUILayout.TextField("Folder Name: ", levelEditor.folderName);
        } else
        {
            if (GUILayout.Button("Enable Folder Wrapping"))
            {
                levelEditor.wrapInFolder = true;
            }
        }
        if (GUILayout.Button("Apply to Song Object"))
        {
            levelEditor.ApplyToSong();
        }
    }
}
