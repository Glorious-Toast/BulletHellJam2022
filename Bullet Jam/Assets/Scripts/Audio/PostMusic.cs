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
       
        uint callbackType = (uint)(AkCallbackType.AK_MusicSyncBeat | AkCallbackType.AK_MusicSyncBar);
        MusicEvent.Post(gameObject, callbackType, MusicCallbacks);
    }

    private void MusicCallbacks(object in_cookie, AkCallbackType in_type, object in_info)
    {
        AkMusicSyncCallbackInfo info = (AkMusicSyncCallbackInfo)in_info;

        if (in_type == AkCallbackType.AK_MusicSyncBeat)
        {
            Debug.Log("Beat");
        }
        if (in_type == AkCallbackType.AK_MusicSyncBar)
        {
            Debug.Log("Bar");
        }
    }
}
