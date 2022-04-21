using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;
using UnityEngine.Experimental.Rendering.Universal;
using System.Linq;

public class SongManager : MonoBehaviour
{
    public bool isPlaying = false;
    public Song song;
    public float BPM;
    public int timeSignature;
    public int currentBeat;
    public uint playingID;
    [SerializeReference]
    public Segment[] playingChart;
    public int boundsX = 15;
    public int boundsY = 15;
    public GameObject bulletPrefab;
    public GameObject discoPrefab;
    private int currentNote;

    public void PlaySong()
    {
        uint callbackType = (uint)(AkCallbackType.AK_MusicSyncBeat | AkCallbackType.AK_MusicSyncBar | AkCallbackType.AK_MusicSyncExit | AkCallbackType.AK_MusicSyncUserCue);
        currentBeat = 0;
        currentNote = 0;
        boundsX = song.boundsX;
        boundsY = song.boundsY;
        playingChart = UnwrapChart(song.songChart);
        playingID = song.songEvent.Post(gameObject, callbackType, MusicCallbacks);
        isPlaying = true;
    }

    public void StopSong()
    {
        AkSoundEngine.StopPlayingID(playingID, 1, AkCurveInterpolation.AkCurveInterpolation_Linear);
        isPlaying = false;
        currentBeat = 0;
    }

    public Segment[] UnwrapChart(List<Segment> chart)
    {
        // Unwraps folders
        List<Segment> finalArray = new List<Segment>();
        foreach (Segment segment in chart)
        {
            if (segment as SegmentFolder != null)
            {
                SegmentFolder segmentFolder = (SegmentFolder)segment;
                foreach (Segment content in segmentFolder.contents)
                {
                    finalArray.Add(content);
                }
            }
            else
            {
                finalArray.Add(segment);
            }
        }
        // Unwraps bullet sequences
        List<Segment> sequenceChecker = new List<Segment>(finalArray);
        foreach (Segment segment in sequenceChecker)
        {
            BulletSequence sequence;
            try
            {
                sequence = (BulletSequence)segment;
            }
            catch
            {
                continue;
            }
            if (sequence == null) continue;
            finalArray.Remove(segment);
            Bullet addedBullet = sequence.bulletData;
            foreach (Vector2 executeVal in sequence.sequence)
            {
                addedBullet.executeTime = executeVal.x;
                addedBullet.coordinate = Mathf.RoundToInt(executeVal.y);
                finalArray.Add(addedBullet.Clone());
            }
        }
        finalArray = SortChart(finalArray);
        return finalArray.ToArray();
    }

    public List<Segment> SortChart(List<Segment> chart)
    {
        chart = chart.OrderBy(x => x.executeTime).ToList();
        return chart;
    }

    public virtual void MusicCallbacks(object in_cookie, AkCallbackType in_type, object in_info)
    {
        AkMusicSyncCallbackInfo info = (AkMusicSyncCallbackInfo)in_info;

        timeSignature = Mathf.RoundToInt(info.segmentInfo_fBarDuration / info.segmentInfo_fBeatDuration);
        BPM = 60 / info.segmentInfo_fBeatDuration;

        if (in_type == AkCallbackType.AK_MusicSyncBeat)
        {
            currentBeat++;
            if (!(currentNote >= playingChart.Length))
            {
                while (playingChart[currentNote].executeTime >= currentBeat && playingChart[currentNote].executeTime < currentBeat + 1)
                {
                    float waitTime = (playingChart[0].executeTime - currentBeat) * info.segmentInfo_fBeatDuration;
                    StartCoroutine(ExecuteAttacks(waitTime, playingChart[currentNote]));
                    currentNote++;
                    if (currentNote >= playingChart.Length)
                    {
                        break;
                    }
                }
            }
        }
        if (in_type == AkCallbackType.AK_MusicSyncBar)
        {

        }
        if (in_type == AkCallbackType.AK_MusicSyncExit)
        {

        }
    }

    IEnumerator ExecuteAttacks(float waitTime, Segment currentAttack)
    {
        yield return new WaitForSeconds(waitTime);
        if (currentAttack as Bullet != null)
        {
            ShootBullet((Bullet)currentAttack);
        }
        if (currentAttack as DiscoAttack != null)
        {
            DiscoAttack((DiscoAttack)currentAttack);
        }
    }

    private void ShootBullet(Bullet data)
    {
        Vector2 newPosition = Vector2.zero;
        // this can definitely be done better, but i'm too lazy
        if ((int)data.direction == 0) newPosition = new Vector2(transform.position.x + data.coordinate, transform.position.y + boundsY);
        if ((int)data.direction == 1) newPosition = new Vector2(transform.position.x + boundsX, transform.position.y + data.coordinate);
        if ((int)data.direction == 2) newPosition = new Vector2(transform.position.x + data.coordinate, transform.position.y - boundsY);
        if ((int)data.direction == 3) newPosition = new Vector2(transform.position.x - boundsX, transform.position.y + data.coordinate);
        float endDirection = (int)data.direction * 90f;
        if ((int)data.direction == 0 || (int)data.direction == 2) endDirection += 180f;
        GameObject firedBullet = Instantiate(bulletPrefab, newPosition, Quaternion.Euler(0f, 0f, endDirection));
        PhysBullet physData = firedBullet.GetComponent<PhysBullet>();
        physData.damage = data.damage;
        physData.speed = data.speed;
        physData.length = data.speed * (data.length * (1 / (BPM / 60)));
        // haven't eaten lunch yet, luckily there's a nice plate of spaghetti right here :D
        if ((int)data.direction == 0 || (int)data.direction == 2)
        {
            physData.timer = (physData.length + boundsY * 2 + 1) / data.speed;
        }
        if ((int)data.direction == 1 || (int)data.direction == 3)
        {
            physData.timer = (physData.length + boundsX * 2 + 1) / data.speed;
        }
        firedBullet.GetComponent<SpriteRenderer>().color = data.color;
        firedBullet.GetComponentInChildren<Light2D>().color = data.color;
    }

    private void DiscoAttack(DiscoAttack data)
    {
        GameObject firedDisco = Instantiate(discoPrefab, Vector2.zero, Quaternion.identity);
        PhysDisco physData = firedDisco.GetComponent<PhysDisco>();
        physData.damage = data.damage;
        physData.warningLength = data.warningLength * 1 / (BPM / 60);
        physData.damageTiles = data.damageTiles;
        physData.color = data.color;
        physData.bounds = new Vector2Int(boundsX, boundsY);
    }
}

