using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class DiscoTile : DamagerBase
{
    public bool isDamager = false;
    public Color color;
    public float activationTimer;
    // Make sure collider is disabled
    public BoxCollider2D bCollider;
    private SpriteRenderer sp;
    public Light2D lightSource;
    private float maxActivation;

    private void Start()
    {
        maxActivation = activationTimer;
        sp = gameObject.GetComponent<SpriteRenderer>();
        sp.color = color;
        lightSource.color = color;
        if (isDamager)
        {
            sp.enabled = true;
            lightSource.enabled = true;
        }
    }

    private void Update()
    {
        if (activationTimer > 0f)
        {
            if (isDamager)
            {
                sp.color = new Color(color.r, color.g, color.b, 1 - (activationTimer / maxActivation));
                lightSource.intensity = (1 - (activationTimer / maxActivation));
            }
            activationTimer -= Time.deltaTime;
            if (activationTimer < 0f)
            {
                if (isDamager)
                {
                    bCollider.enabled = true;
                }
                StartCoroutine("Deactivate");
            }
        }
    }

    IEnumerator Deactivate()
    {
        if (!isDamager)
        {
            sp.enabled = true;
            lightSource.enabled = true;
        } else
        {
            lightSource.intensity = 2f;
        }
        yield return new WaitForSeconds(0.1f);
        bCollider.enabled = false;
        float deactivationTimer = 1f;
        while (deactivationTimer > 0f)
        {
            yield return null;
            deactivationTimer -= Time.deltaTime;
            sp.color = new Color(color.r, color.g, color.b, deactivationTimer);
            if (isDamager)
            {
                lightSource.intensity = deactivationTimer * 2;
            } else
            {
                lightSource.intensity = deactivationTimer / 4;
            }
        }
        Destroy(gameObject);
    }
}
