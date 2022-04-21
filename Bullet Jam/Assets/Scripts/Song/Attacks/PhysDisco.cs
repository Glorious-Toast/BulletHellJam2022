using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysDisco : MonoBehaviour
{
    public Vector2Int bounds;
    public float damage;
    public float warningLength;
    public GameObject discoTile;
    public Color color;
    public List<Vector2Int> damageTiles;
    private int batches = 15;

    private void Start()
    {
        batches = FindObjectOfType<SongManager>().boundsY*2;
        transform.position = new Vector2(transform.position.x - bounds.x, transform.position.y - bounds.y);
        foreach (Vector2 loc in damageTiles)
        {
            DiscoTile tile = Instantiate(discoTile, new Vector2(transform.position.x + loc.x, transform.position.y + loc.y), Quaternion.identity).GetComponent<DiscoTile>();
            tile.damage = damage;
            tile.activationTimer = warningLength;
            tile.color = color;
            tile.isDamager = true;
        }
        StartCoroutine("InstantiateExtra");
    }
    IEnumerator InstantiateExtra()
    {
        int batchAmount = 0;
        float displaceAmount = 0f;
        for (int x = 0; x < bounds.x * 2 + 1; x++)
        {
            for (int y = 0; y < bounds.y * 2 + 1; y++)
            {
                if (!damageTiles.Contains(new Vector2Int(x, y)))
                {
                    DiscoTile tile = Instantiate(discoTile, new Vector2(transform.position.x + x, transform.position.y + y), Quaternion.identity).GetComponent<DiscoTile>();
                    tile.isDamager = false;
                    tile.color = Random.ColorHSV(0, 1, 0.4f, 0.4f, 0.6f, 0.6f);
                    tile.activationTimer = warningLength - displaceAmount;
                    batchAmount++;
                    if (batchAmount == batches)
                    {
                        batchAmount = 0;
                        displaceAmount += warningLength / (batches*3);
                        yield return new WaitForSeconds(warningLength/(batches*3));
                    }
                }
            }
        }
        Destroy(gameObject);
    }
}
 