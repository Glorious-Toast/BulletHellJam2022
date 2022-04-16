using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostMusic : MonoBehaviour
{
    public AK.Wwise.Event MusicEvent;
    private float playingBPM = 0;

    // Start is called before the first frame update
    void Start()
    {
       
        MusicEvent.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBeat, Callback);
    }

    void Callback(object in_cookie, AkCallbackType in_type, object in_info)
    {
        AkMusicSyncCallbackInfo info = (AkMusicSyncCallbackInfo)in_info;

        playingBPM = 60 / info.segmentInfo_fBeatDuration;
        print(playingBPM);
    }
}
