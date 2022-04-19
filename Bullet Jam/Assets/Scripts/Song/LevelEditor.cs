using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
    [HideInInspector]
    public bool cueSync = false;
    [HideInInspector]
    public string syncedCue;
    [HideInInspector]
    public int beatDenominator = 8;
    public Bullet editedBullet = new Bullet(0f, Bullet.Direction.North, 0, Color.white);
    public DiscoAttack editedDiscoAttack = new DiscoAttack(0f, 1f);
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
        songChart.Insert(0, new Bullet(editedBullet.executeTime, editedBullet.direction, editedBullet.coordinate, editedBullet.color, editedBullet.damage, editedBullet.speed, editedBullet.length));
    }

    public void AddDisco()
    {
        songChart.Insert(0, editedDiscoAttack);
    }

    public void CueSync(string cue, float time)
    {
        if (cue == syncedCue)
        {
            int denominator = 1;
            float dividedDecimal = time - Mathf.Floor(time);
            List<float> denominatorList = new List<float>();
            for (int i = beatDenominator; i > 0; i--)
            {
                denominator++;
                float rounded = Mathf.Round(dividedDecimal * denominator);
                rounded /= denominator;
                denominatorList.Add(Mathf.Abs(dividedDecimal - rounded));
            }
            int smallestIndex = 0;
            int currentIndex = 0;
            foreach (float num in denominatorList)
            {
                if (num < denominatorList[smallestIndex])
                {
                    smallestIndex = currentIndex;
                }
                currentIndex++;
            }
            float resultTime = Mathf.Round(time * (smallestIndex + 2));
            resultTime /= smallestIndex + 2;
            Bullet addedBullet = new Bullet(resultTime, editedBullet.direction, editedBullet.coordinate, editedBullet.color, editedBullet.damage, editedBullet.speed, editedBullet.length);
            songChart.Add(addedBullet);

        }
    }

    public void AddSequenceText()
    {

    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
