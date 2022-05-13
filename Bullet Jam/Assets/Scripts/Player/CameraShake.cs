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

    public void Shake(float length)
    {
        Vector2 shakeVector = new Vector2(Random.Range(-10f, 10f) / 10, Random.Range(-10f, 10f) / 10).normalized * length;
        mainCam.transform.localPosition = mainCam.transform.localPosition + new Vector3(shakeVector.x, shakeVector.y, 0f);
    }
}
