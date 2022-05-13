using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerBase : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent.GetComponent<PlayerStats>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
