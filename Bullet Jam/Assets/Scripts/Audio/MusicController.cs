/*
 * PRIMARY MUSIC CONTROLLER
 * callback fundtionality here
 * see other scripts for use of cues and state setting
 * */


using UnityEngine;
using System.Collections;
using BulletFury.Data;


    public class MusicController : MonoBehaviour
    {
        public MusicArrangement arrangement;

        public AK.Wwise.Event MusicEvent;
        private float playingBPM = 0;
        private uint activePlaylistItem = 0;
        private uint currentPlaylistSelection = 0;

        [HideInInspector]
        public bool playlistItemDone = false;
        [HideInInspector]
        public uint currentArrangementState = 0;
        [HideInInspector]
        public uint currentGameplayState = 0;
        [HideInInspector]
        public string currentUserCue;

        public void Awake()
        {
            arrangement = GetComponent<MusicArrangement>();
      
            //set the starting state for the music in the game
            AkSoundEngine.SetState("Gamestate", "Gameplay");
            AkSoundEngine.SetState("Arrangement", "Block1");
        }

        // Start is called before the first frame update
        void Start()
        {


            uint CallbackType = (uint)(AkCallbackType.AK_MusicSyncBeat
                | AkCallbackType.AK_MusicPlaylistSelect
                | AkCallbackType.AK_MusicSyncUserCue
                | AkCallbackType.AK_EnableGetMusicPlayPosition
               );


            MusicEvent.Post(gameObject, (uint)CallbackType, CallbackFunciton);
        }

        public void Update()
        {
            //AkSoundEngine.GetState("Arrangement", out currentArrangementState);
            //AkSoundEngine.GetState("Arrangement", out currentGameplayState);

            arrangement.PerformPlaylist();
            

            /* print("Arrangement State: " + currentArrangementState + 
                 " |Gameplay State: " + currentGameplayState + 
                 " Current Cue: " + currentUserCue +
                 " |Bridge Played Status: " + arrangement.bridgePlayed + 
                 " |Block 1 Played Status: " + arrangement.block1Played + 
                 " |Block 2 Played Status: " + arrangement.block2Played);
             arrangement.PerformPlaylist(currentPlaylistSelection);*/



        }



        void CallbackFunciton(object in_cookie, AkCallbackType in_type, object in_info)
        {
            //this if statment is called every time MusicSyncBeat is triggered via wwise and then the items inside are compleated
            if (in_type == AkCallbackType.AK_MusicSyncBeat)
            {
                AkMusicSyncCallbackInfo info = (AkMusicSyncCallbackInfo)in_info;

                playingBPM = 60 / info.segmentInfo_fBeatDuration;

                //print("BPM: " + playingBPM);
                //print(info.segmentInfo_fBeatDuration);
            }

            //this if statment is called every time MusicPlaylistSelect is triggered via wwise and then the items inside are compleated
            if (in_type == AkCallbackType.AK_MusicPlaylistSelect)
            {

                AkMusicPlaylistCallbackInfo playlistInfo = (AkMusicPlaylistCallbackInfo)in_info;
                AkCallbackInfo callbackInfo = (AkMusicPlaylistCallbackInfo)in_info;

               // activePlaylistItem = playlistInfo.playingID;
                //currentPlaylistSelection = playlistInfo.uPlaylistSelection;
                //print("Playlist Item: " + activePlaylistItem + " , Playlist Selection: " + currentPlaylistSelection);

               // currentPlaylistSelection += currentPlaylistSelection;



                //if a playlist has finished playing, we have finished! Time to call the next
                // if (playlistInfo.uPlaylistItemDone == 0)
                //     playlistItemDone = true;
            }

            if (in_type == AkCallbackType.AK_MusicSyncUserCue)
            {

                AkMusicSyncCallbackInfo cueInfo = (AkMusicSyncCallbackInfo)in_info;

                currentUserCue = cueInfo.userCueName;

                //Debug.Log("Cue: " + currentUserCue);
            }





            /*       if(in_type == AkCallbackType.AK_EnableGetSourcePlayPosition)
                    {
                        AkSoundEngine.GetSourcePlayPosition(2932040671, out int outPosition);//use to gt paly position
                    }*/

        }


    }
