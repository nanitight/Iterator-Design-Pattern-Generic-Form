using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserFighterSelection
{
    public BaseFighter[] narutoSelected,
        dbzSelected;

   public override string ToString()
    {
        string str = "";
        foreach(var f in dbzSelected)
        {
            str += f.ToString()+ "##";
        }
        return str;
    }
}
