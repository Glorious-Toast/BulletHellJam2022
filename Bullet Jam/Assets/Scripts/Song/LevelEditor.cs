using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    public Song editedSong;
    [SerializeReference]
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

    private void Awake()
    {
        songManager = FindObjectOfType<EditorSongManager>();
    }

    public void SongToggleButton()
    {
        if (!songManager.isPlaying)
        {
            startButton.SetActive(false);
            StartCoroutine("StartSong");
        } else
        {
            StopSong();
        }
    }

    public void StopSong()
    {
        startButtonText.text = "Start Song";
        beatText.text = "";
        songManager.StopSong();
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

    public void ApplyToSong()
    {
        editedSong.songChart.Add(new DiscoAttack(5f, 3f));
        int attacks = 0;
        foreach (Segment segment in songChart)
        {
            attacks++;
            editedSong.songChart.Add(segment);
            editedSong.songChart = editedSong.songChart.OrderBy(x => x.executeTime).ToList();
        }
        songChart.Clear();
        Debug.Log($"Successfully applied {attacks} attacks to song " + editedSong.name);
    }

    public void AddBullet()
    {
        songChart.Insert(0, new Bullet(0f, Bullet.Direction.North, 0, Color.white));
    }

    public void AddDisco()
    {
        songChart.Insert(0, new DiscoAttack(0f, 1f));
    }

    public void AddSequenceText()
    {

    }
}
