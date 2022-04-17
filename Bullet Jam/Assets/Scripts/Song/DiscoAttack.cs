using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoAttack : Segment
{
    // How long until the attack is active
    public float attackTime;
    // The amount of tiles that will become red
    public int attackTiles = 10;

    public DiscoAttack(float pAttackTime, int pAttackTiles = 10)
    {
        attackTime = pAttackTime;
        attackTiles = pAttackTiles;
    }
}
