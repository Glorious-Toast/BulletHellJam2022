using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletSequence : Segment
{
    public Bullet bulletData;
    [Tooltip("The first number is executeTime, the second number is coordinate.")]
    public List<Vector2> sequence;

    public BulletSequence(Bullet pBulletData, List<Vector2> pSequence)
    {
        bulletData = pBulletData;
        sequence = pSequence;
    }
}
