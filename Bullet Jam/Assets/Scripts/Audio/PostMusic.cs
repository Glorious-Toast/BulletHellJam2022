using System;
using UnityEngine;

public class PostMusic : MonoBehaviour
{
    public AK.Wwise.Event MusicEvent;
    private float playingBPM = 0;
    private uint activePlaylistItem = 0;
    private uint currentPlaylistSelection = 0;

    // Start is called before the first frame update
    void Start()
    {
        uint CallbackType = (uint)(AkCallbackType.AK_MusicSyncBeat | AkCallbackType.AK_MusicPlaylistSelect | AkCallbackType.AK_MusicSyncUserCue);

        MusicEvent.Post(gameObject, (uint)CallbackType, CallbackFunciton);
    }

    public void Update()
    {
        SwitchPlaylist();
    }

    private void SwitchPlaylist()
    {
        switch (currentPlaylistSelection)
        {
            case 0:
                //Intro
                // print("Playing Intro - " + currentPlaylistSelection);
                break;

            case 1:
                //Verse
                // print("Playing Verse - " + currentPlaylistSelection);
                break;

            case 2:
                //Chorus
                //print("Playing Chorus - " + currentPlaylistSelection);
                break;

            case 3:
                //Bridge
                // print("Playing Bridge - " + currentPlaylistSelection);
                break;
            case 4:
                //print("Playing 4 - " + currentPlaylistSelection);
                break;
            case 5:
                //print("Playing 5 - " + currentPlaylistSelection);
                break;

            default:
                //print("Playing Default");
                break;
        }
    }

    void CallbackFunciton(object in_cookie, AkCallbackType in_type, object in_info)
    {
        //this if statment is called every time MusicSyncBeat is triggered via wwise and then the items inside are compleated
        if (in_type == AkCallbackType.AK_MusicSyncBeat)
        {
            AkMusicSyncCallbackInfo info = (AkMusicSyncCallbackInfo)in_info;

            playingBPM = 60 / info.segmentInfo_fBeatDuration;

            //print("BPM: " + playingBPM);
        }

        //this if statment is called every time MusicPlaylistSelect is triggered via wwise and then the items inside are compleated
        if (in_type == AkCallbackType.AK_MusicPlaylistSelect)
        {
            AkMusicPlaylistCallbackInfo playlistInfo = (AkMusicPlaylistCallbackInfo)in_info;


            activePlaylistItem = playlistInfo.playingID;
            currentPlaylistSelection = playlistInfo.uPlaylistSelection;
            // print("Playlist Item: " + activePlaylistItem + " , Playlist Selection: " + currentPlaylistSelection);

        }

        if (in_type == AkCallbackType.AK_MusicSyncUserCue)
        {
            print(in_type);
            AkMusicSyncCallbackInfo cueInfo = (AkMusicSyncCallbackInfo)in_info;

            string currentUserCue = cueInfo.userCueName;


            switch (currentUserCue)
            {
                case "D_Kick":
                    print("Cue - Kick");
                    break;
                case "D_Chat":
                    print("Cue - Closed Hat");
                    break;
                case "D_Snare":
                    print("Cue - Snare");
                    break;
                case "Bass":
                    print("Cue - Bass");
                    break;
                case "Mel":
                    print("Cue - Melody");
                    break;
                default:
                    print("Cue - Default");
                    break;

            }


        }

    }


}
