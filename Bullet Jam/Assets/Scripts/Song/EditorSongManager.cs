using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class EditorSongManager : SongManager
{
    private LevelEditor levelEditor;

    private void Awake()
    {
        levelEditor = FindObjectOfType<LevelEditor>();
    }

    public override void MusicCallbacks(object in_cookie, AkCallbackType in_type, object in_info)
    {
        base.MusicCallbacks(in_cookie, in_type, in_info);
        if (in_type == AkCallbackType.AK_MusicSyncBeat)
        {
            levelEditor.beatText.text = currentBeat.ToString();
        }
    }
}

