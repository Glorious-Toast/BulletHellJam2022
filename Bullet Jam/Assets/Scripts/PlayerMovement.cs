using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float travelDistance = 1f;
    public float lerpSpeed = 0.4f;
    private float elapsedTime;
    public int boundsX = 25;
    public int boundsY = 25;
    public Transform graphicsTransform;
    public Transform pLocTransform;
    private bool isMoving = false;
    private Vector2 startPosition;


    private void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        Lerp();
    }

    private void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && !isMoving)
        {
            MovePlayer(Vector2.right * Input.GetAxisRaw("Horizontal"));
        }
        if (Input.GetAxisRaw("Vertical") != 0 && !isMoving)
        {
            MovePlayer(Vector2.up * Input.GetAxisRaw("Vertical"));
        }
    }

    private void MovePlayer(Vector3 mVector)
    {
        mVector = mVector.normalized * travelDistance;
        if (pLocTransform.position.x + mVector.x > boundsX || pLocTransform.position.x + mVector.x < -boundsX)
        {
            return;
        }
        if (pLocTransform.position.y + mVector.y > boundsY || pLocTransform.position.y + mVector.y < -boundsY)
        {
            return;
        }
        startPosition = graphicsTransform.position;
        isMoving = true;
        pLocTransform.position = pLocTransform.position + new Vector3(mVector.x, mVector.y, 0f);
    }

    private void Lerp()
    {
        if (graphicsTransform.position != pLocTransform.position)
        {
            elapsedTime += Time.fixedDeltaTime;
            graphicsTransform.position = Vector2.Lerp(startPosition, pLocTransform.position, Mathf.SmoothStep(0, 1, elapsedTime/lerpSpeed));
        } else
        {
            isMoving = false;
            elapsedTime = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        // Draws the bounds
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(boundsX * 2, boundsY * 2, 1f));
    }
}
