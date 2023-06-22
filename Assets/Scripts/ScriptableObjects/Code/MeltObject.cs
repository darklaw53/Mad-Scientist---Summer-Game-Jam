using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MeltObject", menuName = "Ingredients/MeltObject")]
public class MeltObject : ScriptableObject
{
    public string materialName;
    public int meltResistance;
    public Sprite sprite;
    public GameObject corpusDArt;
    public int reward;
}