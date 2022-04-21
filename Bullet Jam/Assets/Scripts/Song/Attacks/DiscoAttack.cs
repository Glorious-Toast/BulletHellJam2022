using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiscoAttack : Segment
{
    // How long until the attack is active
    public float warningLength;
    public float damage;
    // The amount of tiles that will become red
    public int attackTiles = 10;
    public Color color;
    public List<Vector2Int> damageTiles;

    public DiscoAttack(float pTime, float pWarningLength, Color pColor, float pDamage = 10f, int pAttackTiles = 10)
    {
        executeTime = pTime;
        warningLength = pWarningLength;
        damage = pDamage;
        attackTiles = pAttackTiles;
        color = pColor;
    }
}
