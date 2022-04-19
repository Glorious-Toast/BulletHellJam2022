using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bullet : Segment
{
    public enum Direction { North, East, South, West }
    public Direction direction;
    [Tooltip("Which column/row the bullet is on")]
    public int coordinate;
    public float damage = 10f;
    [Tooltip("The speed of the bullet, measured in units per second")]
    public float speed = 10f;
    [Tooltip("How long the bullet is, in beats")]
    public float length = 2f;
    public Color color = Color.red;

    public Bullet(float pTime, Direction pDirection, int pCoordinate, Color pColor, float pDamage = 10f, float pSpeed = 10f, float pLength = 2f)
    {
        executeTime = pTime;
        direction = pDirection;
        coordinate = pCoordinate;
        damage = pDamage;
        speed = pSpeed;
        length = pLength;
        color = pColor;
    }
}
