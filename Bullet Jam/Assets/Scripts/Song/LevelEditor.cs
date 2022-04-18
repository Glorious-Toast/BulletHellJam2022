using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    public List<Segment> songChart;
    [HideInInspector]
    public Text timer;
    [HideInInspector]
    public GameObject startButton;
    [HideInInspector]
    public Text startButtonText;
    [HideInInspector]
    public Text beatText;
    private EditorSongManager songManager;
    private bool isPlaying = false;
    public string sequenceFile;

    private void Awake()
    {
        songManager = FindObjectOfType<EditorSongManager>();
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
            StopSong();
        }
    }

    public void StopSong()
    {
        startButtonText.text = "Start Song";
        isPlaying = false;
        AkSoundEngine.StopPlayingID(songManager.playingID, 1, AkCurveInterpolation.AkCurveInterpolation_Linear);
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

    public void AddSequenceText()
    {

    }
}
