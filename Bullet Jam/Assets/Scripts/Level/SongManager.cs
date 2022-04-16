using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class SongManager : MonoBehaviour
{
    public Song song;
    public float BPM;
    public int timeSignature;

    private void PlaySong()
    {
        uint callbackType = (uint)(AkCallbackType.AK_MusicSyncBeat | AkCallbackType.AK_MusicSyncBar | AkCallbackType.AK_MusicSyncEntry | AkCallbackType.AK_MusicSyncExit);
        song.songEvent.Post(gameObject, callbackType, MusicCallbacks);
    }

    private void MusicCallbacks(object in_cookie, AkCallbackType in_type, object in_info)
    {
        AkMusicSyncCallbackInfo info = (AkMusicSyncCallbackInfo)in_info;
        AkMusicPlaylistCallbackInfo pInfo = (AkMusicPlaylistCallbackInfo)in_info;

        if (in_type == AkCallbackType.AK_MusicSyncBeat)
        {

        }
    }
}
