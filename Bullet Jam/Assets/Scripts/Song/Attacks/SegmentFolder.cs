using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SegmentFolder : Segment
{
    public string name = "Segment Folder";
    [SerializeReference]
    public Segment[] contents;

    public SegmentFolder(float pTime, string pName, Segment[] pContents)
    {
        executeTime = pTime;
        name = pName;
        contents = pContents;
    }
}
