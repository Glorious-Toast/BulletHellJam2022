using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Range(0,100)]
    public float HP = 100f;
    private float maxHP = 100f;
    public GameObject hitbox;

    private void Awake()
    {
       hitbox.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void Update()
    {

    }

    public void Damage(float amount)
    {
        HP -= amount;
        // Play damage sound
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

    }
}
