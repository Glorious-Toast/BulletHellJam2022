using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Segment
{
    public float executeTime;

    public Segment Clone()
    {
        return (Segment)this.MemberwiseClone();
    }
}
