using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysBullet : MonoBehaviour
{
    public float damage;
    public float speed;

    private void Update()
    {
        transform.position = transform.position + (transform.up * speed * Time.deltaTime);
    }

}
