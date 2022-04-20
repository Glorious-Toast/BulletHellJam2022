using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    public Song editedSong;
    [SerializeReference]
    public List<Segment> songChart;
    [HideInInspector] public Text timer;
    [HideInInspector] public GameObject startButton;
    [HideInInspector] public Text startButtonText;
    [HideInInspector] public Text beatText;
    [HideInInspector] public bool cueSync = false;
    [HideInInspector] public string syncedCue;
    [HideInInspector] public int beatDenominator = 8;
    [HideInInspector] public bool wrapInFolder;
    [HideInInspector] public string folderName = "Sequence Folder";
    public Bullet editBullet = new Bullet(0f, Bullet.Direction.North, 0, Color.white);
    public DiscoAttack editDiscoAttack = new DiscoAttack(0f, 1f);
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
        int attacks = 0;
        List<Segment> finalApplied = songChart;
        List<Segment> sequenceChecker = songChart;
        List<BulletSequence> sequences = new List<BulletSequence>();
        while (sequenceChecker.Count > 0)
        {
            Bullet bullet = (Bullet)sequenceChecker[0];
            sequenceChecker.RemoveAt(0);
            if (bullet == null) continue;
            bool flag = false;
            int iter = 0;
            foreach (BulletSequence sequence in sequences)
            {
                sequence.bulletData.executeTime = bullet.executeTime;
                sequence.bulletData.coordinate = bullet.coordinate;
                var propertiesSequence = sequence.bulletData.GetType().GetProperties();
                var propertiesBullet = bullet.GetType().GetProperties();
                bool same = true;
                int propertyIter = 0;
                while (propertyIter < propertiesSequence.Length)
                {
                    if (propertiesSequence[propertyIter] != propertiesBullet[propertyIter])
                    {
                        same = false;
                    }
                    propertyIter++;
                }
                if (same)
                {
                    flag = true;
                    sequences[iter].sequence.Add(new Vector2(bullet.executeTime, bullet.coordinate));
                }
                iter++;
            }
            if (!flag)
            {
                List<Vector2> valList = new List<Vector2>();
                valList.Add(new Vector2(bullet.executeTime, bullet.coordinate));
                sequences.Add(new BulletSequence(bullet, valList));
            }
        }
        foreach (Segment segment in finalApplied)
        {
            if (segment as Bullet != null) finalApplied.Remove(segment);
        }
        foreach (BulletSequence sequence in sequences)
        {
            Debug.Log("hey");
            if (sequence.sequence.Count > 1)
            {
                finalApplied.Add(sequence);
            } else
            {
                finalApplied.Add(sequence.bulletData);
            }
        }
        if (wrapInFolder)
        {
            List<Segment> folder = new List<Segment>();
            foreach (Segment segment in finalApplied)
            {
                attacks++;
                folder.Add(segment);
            }
            editedSong.songChart.Add(new SegmentFolder(0f, folderName, folder.ToArray()));
        } else
        {
            foreach (Segment segment in finalApplied)
            {
                attacks++;
                editedSong.songChart.Add(segment);
            }
        }
        songChart.Clear();
        Debug.Log($"Successfully applied {attacks} attacks to song " + editedSong.name);
    }

    public void AddBullet()
    {
        songChart.Insert(0, editBullet.Clone());
    }

    public void AddDisco()
    {
        songChart.Insert(0, editDiscoAttack.Clone());
    }

    public void CueSync(string cue, float time)
    {
        if (!cueSync) return;
        if (cue == syncedCue)
        {
            float dividedDecimal = time - Mathf.Floor(time);
            List<float> denominatorList = new List<float>();
            for (int denominator = 1; denominator < beatDenominator;)
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
            Bullet addedBullet = (Bullet)editBullet.Clone();
            addedBullet.executeTime = resultTime;
            songChart.Add(addedBullet);

        }
    }

    public void AddSequenceText()
    {

    }
}
