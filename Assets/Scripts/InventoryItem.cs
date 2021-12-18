using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    private int id;
    private string name;
    private string description;
    private Sprite inventoryIcon; 
    private Object prefab;

    public InventoryItem(int id, string name, string description, Sprite inventoryIcon, Object prefab)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.inventoryIcon = inventoryIcon;
        this.prefab = prefab;
    }

    public InventoryItem(InventoryItem item)
    {
        this.id = item.id;
        this.name = item.name;
        this.description = item.description;
        this.inventoryIcon = item.inventoryIcon;
        this.prefab = item.prefab;
    }

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
