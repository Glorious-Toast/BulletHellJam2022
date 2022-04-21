using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Range(0,100)]
    public float HP = 100f;
    private float maxHP = 100f;
    public GameObject hitbox;
    private CameraShake cameraShake;

    private void Awake()
    {
        hitbox.GetComponent<Rigidbody2D>().isKinematic = true;
        cameraShake = FindObjectOfType<CameraShake>();
    }

    public void Damage(float amount)
    {
        HP -= amount;
        // Play damage sound
        if (cameraShake != null)
        {
            cameraShake.Shake(0.08f, 0.3f);
        }
        if (HP <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        HP = Mathf.Clamp(HP + amount, 0f, maxHP);
    }

    public void Die()
    {
        // AkSoundEngine.SetState("Gameplay", "Defeat");
        StartCoroutine("SlowSong");
    }

    IEnumerator SlowSong()
    {
        float newSpeed = 1f;
        while (newSpeed > 0f)
        {
            yield return null;
            newSpeed -= Time.deltaTime / 2;
            if (newSpeed < 0f)
            {
                newSpeed = 0f;
            }
            Debug.Log(newSpeed);
            AkSoundEngine.SetRTPCValue("PlaySpeed", newSpeed);
        }
        LevelEditor editor = FindObjectOfType<LevelEditor>();
        if (editor != null)
        {
            editor.StopSong();
        } else
        {
            FindObjectOfType<SongManager>().StopSong();
        }
    }
}
