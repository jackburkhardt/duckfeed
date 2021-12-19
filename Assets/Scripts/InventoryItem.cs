using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    // ID number for inventory objects, starting at 0
    [SerializeField] private int id;
    // Name of object (for display)
    [SerializeField] private string name;
    // Description of object (for display)
    [SerializeField] private string description;
    [SerializeField] private Sprite inventoryIcon; 
    [SerializeField] private Object prefab;
    
    public int ID
    {
        get => id;
        set => id = value;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public string Description
    {
        get => description;
        set => description = value;
    }

    public Sprite InventoryIcon
    {
        get => inventoryIcon;
        set => inventoryIcon = value;
    }

    public Object Prefab
    {
        get => prefab;
        set => prefab = value;
    }
}
