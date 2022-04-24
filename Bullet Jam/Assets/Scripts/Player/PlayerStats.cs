using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerStats : MonoBehaviour
{
    [Range(0,100)]
    public float HP = 100f;
    private float maxHP = 100f;
    public GameObject hitbox;
    public GameObject gameOverScreen;
    public Text scoreNumber;
    public GameObject newHighScore;
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
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine("SlowSong");
    }

    IEnumerator SlowSong()
    {
        float newSpeed = 1f;
        SongManager songManager = FindObjectOfType<SongManager>();
        SpriteRenderer sp = gameObject.GetComponentInChildren<SpriteRenderer>();
        Light2D light = gameObject.GetComponentInChildren<Light2D>();
        songManager.readNotes = false;
        while (newSpeed > 0f)
        {
            yield return null;
            newSpeed -= Time.unscaledDeltaTime / 2;
            if (newSpeed < 0f)
            {
                newSpeed = 0f;
            }
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, newSpeed);
            light.intensity = newSpeed;
            Time.timeScale = newSpeed;
            AkSoundEngine.SetRTPCValue("PlaySpeed", newSpeed);
        }
        LevelEditor editor = FindObjectOfType<LevelEditor>();
        if (editor != null)
        {
            editor.StopSong();
        } else
        {
            songManager.StopSong();
        }
        if (songManager.score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", Mathf.FloorToInt(songManager.score));
            newHighScore.SetActive(true);
        }
        scoreNumber.text = Mathf.FloorToInt(songManager.score).ToString() + "s";
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<Animator>().Play("udiedurbadatthegame");

    }
}
