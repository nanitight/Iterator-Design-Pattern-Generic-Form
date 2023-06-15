using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Naruto Data",menuName ="Naruto",order =1)]
public class NarutoFighter : BaseFighter
{
    public string powerName ;

    public override string ToString()
    {
        string text = "My name is " + fighterName + ". My superpower is called " + powerName + ". To take me out you need a total attack of " + healthPoints + ".";
        return text;
    }

    public override int GetPowerValue()
    {
        return powerName.Length;
    }
}
