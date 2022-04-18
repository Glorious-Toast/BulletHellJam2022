using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiscoAttack : Segment
{
    // How long until the attack is active
    public float warningLength;
    // The amount of tiles that will become red
    public int attackTiles = 10;

    public DiscoAttack(float pTime, float pWarningLength, int pAttackTiles = 10)
    {
        executeTime = pTime;
        warningLength = pWarningLength;
        attackTiles = pAttackTiles;
    }
}
