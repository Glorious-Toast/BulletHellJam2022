using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysBullet : MonoBehaviour
{
    public float damage;
    // The length it traverses in a second
    public float speed;
    public float length;
    public float timer;

    private void Start()
    {
        transform.localScale = new Vector3(1f, length, 1f);
        transform.position = transform.position + (-transform.up * (length /2));
        transform.position = transform.position + (-transform.up / 2);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void Update()
    {
        transform.position = transform.position + (transform.up * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Destroy(gameObject);
        }
    }
}
