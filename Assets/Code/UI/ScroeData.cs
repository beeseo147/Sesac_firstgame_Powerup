using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScroeData
{
    public List<Score> scores;
    public ScroeData()
    {
        scores = new List<Score>();
    }
}
