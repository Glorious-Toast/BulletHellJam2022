using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSequence : Segment
{
    public enum Direction { North, East, South, West }
    public Direction direction;
    public List<Bullet> bullets;

    [System.Serializable]
    public class Bullet
    {
        public float time;
        public int coordinate;
        public float damage = 10f;
        public float length = 2f;

        public Bullet(float pTime, int pCoordinate, float pDamage = 10f, float pLength = 2f)
        {
            time = pTime;
            coordinate = pCoordinate;
            damage = pDamage;
            length = pLength;
        }
    }

    public BulletSequence(Direction pDirection, List<Bullet> pBullets)
    {
        direction = pDirection;
        bullets = pBullets;
    }
}
