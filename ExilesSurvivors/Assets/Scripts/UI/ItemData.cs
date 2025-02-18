using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public int width;
    public int height;
    public Sprite itemIcon;
}
