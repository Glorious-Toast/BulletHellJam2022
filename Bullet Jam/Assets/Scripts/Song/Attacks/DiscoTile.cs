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
    private Light2D light;
    private float maxActivation;

    private void Start()
    {
        maxActivation = activationTimer;
        sp = gameObject.GetComponent<SpriteRenderer>();
        sp.color = color;
        light.color = color;
        if (isDamager)
        {
            sp.enabled = true;
        }
    }

    private void Update()
    {
        if (isDamager)
        {
            sp.color = new Color(color.r, color.g, color.b, 1 - (activationTimer / maxActivation));
        }
        if (activationTimer > 0f)
        {
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
        }
        yield return new WaitForSeconds(0.1f);
        bCollider.enabled = false;
        float deactivationTimer = 1f;
        while (deactivationTimer > 0f)
        {
            yield return null;
            deactivationTimer -= Time.deltaTime;
            sp.color = new Color(color.r, color.g, color.b, deactivationTimer);
        }
        Destroy(gameObject);
    }
}
