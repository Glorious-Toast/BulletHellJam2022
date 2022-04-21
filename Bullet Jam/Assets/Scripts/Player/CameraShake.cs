using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCam;

    private float shakeAmount = 0f;
    private bool isShaking = false;

    private void Awake()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }
    }

    private void Update()
    {
        Vector3 targetPos = new Vector3(0f, 0f, -10f);
        if (mainCam.transform.localPosition != targetPos && !isShaking)
        {
            mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, targetPos, Time.deltaTime * 15f);
            if (Vector3.Distance(mainCam.transform.localPosition, targetPos) < 0.001f)
            {
                mainCam.transform.localPosition = targetPos;
            }
        }
    }

    public void Shake(float amount, float length)
    {
        shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);

    }

    void BeginShake()
    {
        isShaking = true;
        if (shakeAmount > 0f)
        {
            Vector3 camPos = mainCam.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }
    }

    void StopShake()
    {
        CancelInvoke("BeginShake");
        isShaking = false;
    }
}
