/*
* -----------Instrument Cues-------------- -
*
* "D_Kick"
* "D_Snare"
* "D_CHat"
* "D_Crash"
* "Bass"
* "Bass2"
* "Mel"
* "Mel2"
* "Fx"
* "Fx2"
*
* each cue is found via postMusic.currentUserCue
* the cue is triggered on, and then retriggered on the next cue over. 
* BUT NEVER TURNED OFF ONCE ON!
*
*/
using UnityEngine;

public class InstrumentCueTracking : MonoBehaviour
{
    MusicController musicController;
    public void Awake()
    {
        musicController = GetComponent<MusicController>();
    }
    public void TrackIntrumentCues()
    {
        switch (musicController.currentUserCue)
        {
            case "D_Kick":

                break;
            case "D_Snare":

                break;
            case "D_CHat":

                break;
            case "D_Crash":

                break;
            case "Bass":

                break;
            case "Bass1":

                break;
            case "Mel":

                break;
            case "Mel2":

                break;
            case "Fx":

                break;
            case "Fx2":

                break;

            default:

                break;
        }
    }
}
