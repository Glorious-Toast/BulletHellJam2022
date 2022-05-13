using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class EditorSongManager : SongManager
{
    private LevelEditor levelEditor;
    [HideInInspector]
    public float beatProgress;

    private void Awake()
    {
        levelEditor = FindObjectOfType<LevelEditor>();
    }

    private void Update()
    {
        if (isPlaying)
        {
            beatProgress += Time.deltaTime;
            score += Time.deltaTime;
        }
    }

    public override void MusicCallbacks(object in_cookie, AkCallbackType in_type, object in_info)
    {
        base.MusicCallbacks(in_cookie, in_type, in_info);
        if (in_type == AkCallbackType.AK_MusicSyncBeat)
        {
            beatProgress = 0f;
            levelEditor.beatText.text = currentBeat.ToString();
        }
        if (in_type == AkCallbackType.AK_MusicSyncExit)
        {
            levelEditor.StopSong();
        }
        if (in_type == AkCallbackType.AK_MusicSyncUserCue)
        {
            AkMusicSyncCallbackInfo cueInfo = (AkMusicSyncCallbackInfo)in_info;
            levelEditor.CueSync(cueInfo.userCueName, currentBeat + (beatProgress / cueInfo.segmentInfo_fBeatDuration));
        }
    }
}

