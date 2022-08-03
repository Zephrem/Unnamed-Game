using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;

    public abstract void Use();

    #region __ACCESSORS__
    public string GetName()
    {
        return itemName;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
    #endregion
}
