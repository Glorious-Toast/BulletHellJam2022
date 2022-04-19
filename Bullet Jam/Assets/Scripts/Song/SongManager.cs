using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;
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
    public List<Segment> playingChart;
    public int spawnX = 15;
    public int spawnY = 15;
    public GameObject bulletPrefab;
    public GameObject discoPrefab;

    public void PlaySong()
    {
        uint callbackType = (uint)(AkCallbackType.AK_MusicSyncBeat | AkCallbackType.AK_MusicSyncBar | AkCallbackType.AK_MusicSyncExit);
        currentBeat = 0;
        playingID = song.songEvent.Post(gameObject, callbackType, MusicCallbacks);
        playingChart = song.songChart;
        isPlaying = true;
        SortChart();
    }

    public void StopSong()
    {
        AkSoundEngine.StopPlayingID(playingID, 1, AkCurveInterpolation.AkCurveInterpolation_Linear);
        isPlaying = false;
        currentBeat = 0;
    }

    public void SortChart()
    {
        playingChart = playingChart.OrderBy(x => x.executeTime).ToList();
    }

    public virtual void MusicCallbacks(object in_cookie, AkCallbackType in_type, object in_info)
    {
        AkMusicSyncCallbackInfo info = (AkMusicSyncCallbackInfo)in_info;

        timeSignature = Mathf.RoundToInt(info.segmentInfo_fBarDuration / info.segmentInfo_fBeatDuration);
        BPM = 60 / info.segmentInfo_fBeatDuration;

        if (in_type == AkCallbackType.AK_MusicSyncBeat)
        {
            currentBeat++;
            if (playingChart.Count > 0)
            {
                while (playingChart[0].executeTime >= currentBeat && playingChart[0].executeTime < currentBeat + 1)
                {
                    float waitTime = (playingChart[0].executeTime - currentBeat) * info.segmentInfo_fBeatDuration;
                    StartCoroutine(ExecuteAttacks(waitTime, playingChart[0]));
                    playingChart.RemoveAt(0);
                    if (playingChart.Count == 0)
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
        if ((int)data.direction == 0) newPosition = new Vector2(transform.position.x + data.coordinate, transform.position.y + spawnY);
        if ((int)data.direction == 1) newPosition = new Vector2(transform.position.x + spawnX, transform.position.y + data.coordinate);
        if ((int)data.direction == 2) newPosition = new Vector2(transform.position.x + data.coordinate, transform.position.y - spawnY);
        if ((int)data.direction == 3) newPosition = new Vector2(transform.position.x - spawnX, transform.position.y + data.coordinate);
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
            physData.timer = (physData.length + spawnY*2 + 1) / data.speed;
        }
        if ((int)data.direction == 1 || (int)data.direction == 3)
        {
            physData.timer = (physData.length + spawnX*2 + 1) / data.speed;
        }
        firedBullet.GetComponent<SpriteRenderer>().color = data.color;
    }

    private void DiscoAttack(DiscoAttack data)
    {
        Debug.Log("Disco attack!");
    }
}

