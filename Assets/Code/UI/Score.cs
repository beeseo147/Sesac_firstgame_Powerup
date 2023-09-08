using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score
{
    public int PlayerNumber;
    public float score;
    public Score(int playerNumber,float score)
    {
        this.PlayerNumber = playerNumber;
        this.score = score;
    }
}
