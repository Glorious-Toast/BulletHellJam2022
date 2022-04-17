using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    public List<Segment> songChart;
    [Tooltip("Rounds to the nearest 1/beatDenominator of a beat when you add a note.")]
    public int beatDenominator = 1;
    [HideInInspector]
    public Text timer;
    [HideInInspector]
    public GameObject startButton;
    [HideInInspector]
    public Text startButtonText;
    private SongManager songManager;
    private bool isPlaying = false;

    private void Awake()
    {
        songManager = FindObjectOfType<SongManager>();
    }

    public void OnStartSong()
    {
        if (!isPlaying)
        {
            startButton.SetActive(false);
            isPlaying = true;
            StartCoroutine("StartSong");
        } else
        {
            startButtonText.text = "Start Song";
            isPlaying = false;
            AkSoundEngine.StopPlayingID(songManager.playingID, 1, AkCurveInterpolation.AkCurveInterpolation_Linear);
        }
    }

    IEnumerator StartSong()
    {
        float countdown = 3f;
        while (countdown > 0f)
        {
            countdown -= Time.deltaTime;
            timer.text = (Mathf.Ceil(countdown)).ToString();
            yield return null;
        }
        timer.text = "";
        songManager.PlaySong();
        startButton.SetActive(true);
        startButtonText.text = "Stop Song";
    }
}
