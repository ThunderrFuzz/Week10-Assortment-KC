using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : MonoBehaviour
{
    public int score = 0;
    public int frameScore = 0;
    public int roll = 0;
    public int totalPins = 10;
    public int strikeBonus = 0;
    public int spareBonus = 0;

    public void AddToFrameScore(int pinsHit)
    {
        frameScore += pinsHit;
        roll++;

        if (roll == 1 && frameScore == totalPins) 
        {
            strikeBonus = 10;  
            score += frameScore + strikeBonus;
            ResetFrame();
        }
        else if (roll == 2)  
        {
            if (frameScore == totalPins) 
            {
                spareBonus = 10 - frameScore;
                score += frameScore + spareBonus;
            }
            else
            {
                score += frameScore;
            }
            ResetFrame();
        }
    }

    void ResetFrame()
    {
        frameScore = 0;
        roll = 0;
        strikeBonus = 0;
        spareBonus = 0;
    }
}
