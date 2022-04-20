using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class Song : ScriptableObject
{
    public AK.Wwise.Event songEvent;
    public int boundsX;
    public int boundsY;
    [SerializeReference]
    public List<Segment> songChart;

}
