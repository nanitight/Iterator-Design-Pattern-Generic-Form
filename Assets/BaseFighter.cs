using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName ="DataF" , menuName = "BaseFighter" , order = 1)]
public class BaseFighter : ScriptableObject
{
    public string fighterName;
    [SerializeField] public GameObject avatar;
}
