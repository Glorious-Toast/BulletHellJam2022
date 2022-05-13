using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class Song : ScriptableObject
{
    public string songEvent;
    public int boundsX;
    public int boundsY;
    [SerializeReference]
    public List<Segment> songChart;

}
