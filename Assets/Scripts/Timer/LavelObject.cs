using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[CreateAssetMenu(fileName ="Lavel", menuName ="Lavel/CreateObjectLavel")]
public class LavelObject : ScriptableObject
{
    public ItemLavelObject[] _ItemLavels;
}
[Serializable]
public struct ItemLavelObject
{
    public string _bestTimeKeyLavel;
    public int _indexLavel;
}

