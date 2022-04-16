using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostMusic : MonoBehaviour
{
    public AK.Wwise.Event MusicEvent;
    private float playingBPM = 0;
    private uint activePlaylistItem = 0;
    private uint currentSelection = 0;

    // Start is called before the first frame update
    void Start()
    {
        uint CallbackType = (uint)(AkCallbackType.AK_MusicSyncBeat | AkCallbackType.AK_MusicPlaylistSelect);
       
       MusicEvent.Post(gameObject, (uint)CallbackType, CallbackFunciton);



    }


    void CallbackFunciton(object in_cookie, AkCallbackType in_type, object in_info)
    {
        //this if statment is called every time MusicSyncBeat is triggered via wwise and then the items inside are compleated
        if(in_type == AkCallbackType.AK_MusicSyncBeat)
       {
            AkMusicSyncCallbackInfo info = (AkMusicSyncCallbackInfo)in_info;

            playingBPM = 60 / info.segmentInfo_fBeatDuration;

            print("BPM: " + playingBPM);
        }

        //this if statment is called every time MusicPlaylistSelect is triggered via wwise and then the items inside are compleated
        if (in_type == AkCallbackType.AK_MusicPlaylistSelect)
        {
            AkMusicPlaylistCallbackInfo playlistInfo = (AkMusicPlaylistCallbackInfo)in_info;


            activePlaylistItem = playlistInfo.playingID;
            currentSelection = playlistInfo.uPlaylistSelection;
            print("Playlist Item: " + activePlaylistItem + " , Playlist Selection: " + currentSelection);

        }

    }


}
