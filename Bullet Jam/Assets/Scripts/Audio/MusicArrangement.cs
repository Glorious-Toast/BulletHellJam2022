/*event start will always start with Intro
* -then do body of song
*
* -----------------Occuring in Wwise---------------
* --Block 1 (plays at least 3 tracks)
* ----Plays Verse
* ----Random chooses
* ------Chorus or Verse
* ----Plays Chorus
*
* --Bridge(Plays at least twice)
* ----Plays Bridge 1-2 times
* ----Play Bridge Exit
*
* --Block 2 (randomly loops 1-3 times)
* ----Plays Verse
* ----Random Chorus or Verse
* ----Plays Exit Trigger track so we know to look to load a new playlist
*
* -then loop
* -trigger outro only on final ho
*
* Notes:
* --I have used a marker track to get to the next playlist is a marker in each track in Wwise that is observed in the PostMusic script
* --If you wish to jump to a different playlist at any time set the state to your block of choice
* --I'm still deciding what is in each block and may rework the above list
* ---currently the above list gives us a fairly pop/edm song structure that loops forever
*
* each cue is found via postMusic.currentUserCue
* the cue is triggered on, and then retriggered on the next cue over. 
* BUT NEVER TURNED OFF ONCE ON!
* 
* 
* Exit Trigger Names:
* To leave block 1
* --- Block1_Exit_Cue
* Block2_Exit_Cue
* Block3_Exit_Cue
* Block4_Exit_Cue
*/


using UnityEngine;
using System.Collections.Generic;



public class MusicArrangement : MonoBehaviour
    {
        MusicController musicController;

    string[] startupSong = new string[]{ "Block1", "Block2", "Block3", "Block4" };

    public void Awake()
        {
            musicController = GetComponent<MusicController>();
      

        }

    public void Start()
    {
        string firstSong = startupSong[Random.Range(0, startupSong.Length - 1)];
        AkSoundEngine.SetState("Arrangement", firstSong);
    }

    public void PerformPlaylist()
        {

            switch (musicController.activeInst)
            {
                case "Block1_Exit_Cue":
                    AkSoundEngine.SetState("Arrangement", "Block2");
                    break;
                case "Block2_Exit_Cue":
                    AkSoundEngine.SetState("Arrangement", "Block3");
                    break;
                case "Block3_Exit_Cue":
                    AkSoundEngine.SetState("Arrangement", "Block4");
                    break;
                case "Block4_Exit_Cue":
                    AkSoundEngine.SetState("Arrangement", "Block1");
                    break;
                default:
                    AkSoundEngine.SetState("Arrangement", "Block1");
                    break;
            }

        }





    }
