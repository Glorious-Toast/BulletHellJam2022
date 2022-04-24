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
* "Mel3"
* "Fx"
* "Fx2"
* "Fx_Impact"
* "Fx_Riser"
*
* each cue is found via postMusic.currentUserCue
* the cue is triggered on, and then retriggered on the next cue over. 
* BUT NEVER TURNED OFF ONCE ON!
*
*/
using BulletFury.Data;
using UnityEngine;


public class InstrumentCueTracking : MonoBehaviour
{
    [SerializeField] private BulletFury.BulletManager instBulletManager = null;
    public MusicController musicController;
    //public SongManager musicController;


    [SerializeField] string instCueName;
    [SerializeField] private bool isAlreadyOn = false;
    [SerializeField] private float rotateSpeed = 0f;
    [SerializeField] public float timeRemaining = 0.1f;

    private bool spawnOnce = true;



    public void Awake()
    {
        Application.targetFrameRate = 128;
        isAlreadyOn = false;
        spawnOnce = true;
    }
    public void Start()
    {

    }

    public void Update()
    {
        //if (instBulletManager == null)
        //    return;

        if (musicController.activeInst.Contains(instCueName))
            print(instCueName);





    }


    public void instrumentBulletFire()
    {
        if (musicController.activeInst.Contains(instCueName))
        {
            instBulletManager.Spawn(transform.position, instBulletManager.Plane == BulletPlane.XY ? transform.up : transform.forward);
            transform.Rotate(instBulletManager.Plane == BulletPlane.XY ? Vector3.forward : Vector3.up, (rotateSpeed * Time.smoothDeltaTime));
        }
    }

}
