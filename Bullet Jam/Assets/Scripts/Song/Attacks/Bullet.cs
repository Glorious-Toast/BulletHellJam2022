using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bullet : Segment
{
    public enum Direction { North, East, South, West }
    public Direction direction;
    public int coordinate;
    public float damage = 10f;
    public float speed = 10f;
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
