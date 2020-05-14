using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Machine", fileName = "New Machine")]
public class Machine : ScriptableObject
{
    public string machineName;
    public GameObject prefab;
    public bool placeable;
    public Sprite icon;
    public int[] craftItems;
}
