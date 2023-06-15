using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName ="DataF" , menuName = "BaseFighter" , order = 1)]
public abstract class BaseFighter : ScriptableObject
{
    public string fighterName;
    public float healthPoints ;
    [SerializeField] public Sprite avatar;

    public abstract int GetPowerValue();
}
