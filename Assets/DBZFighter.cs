using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DataFile", menuName ="DBZFighter", order =1)]
public class DBZFighter : BaseFighter
{
    public float healthPoints, attackPower;

    public override string ToString()
    {
        string text = "My name is "+fighterName+". My superpower has a limit of "+attackPower+". To take me out you need a total attack of "+healthPoints+".";
        return text ;
    }
}
