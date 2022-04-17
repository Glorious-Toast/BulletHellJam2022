using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class SongManager : MonoBehaviour
{
    public Song song;
    public float BPM;
    public int timeSignature;
    public uint playingID;

    public void PlaySong()
    {
        uint callbackType = (uint)(AkCallbackType.AK_MusicSyncBeat | AkCallbackType.AK_MusicSyncBar);
        playingID = song.songEvent.Post(gameObject, callbackType, MusicCallbacks);
        
    }

    private void MusicCallbacks(object in_cookie, AkCallbackType in_type, object in_info)
    {
        AkMusicSyncCallbackInfo info = (AkMusicSyncCallbackInfo)in_info;

        timeSignature = Mathf.RoundToInt(info.segmentInfo_fBarDuration / info.segmentInfo_fBeatDuration);
        BPM = 60 / info.segmentInfo_fBeatDuration;

        if (in_type == AkCallbackType.AK_MusicSyncBeat)
        {
            
        }
        if (in_type == AkCallbackType.AK_MusicSyncBar)
        {
            
        }
    }
}

