using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [Header("Inventory system")]
    [SerializeField] private int size;
    [SerializeField] private int capacity;
    [SerializeField] private List<InventoryItem> inventoryList;
    private bool full;
    private Dictionary<int, Object> itemPrefabMap = new Dictionary<int, Object>();
    [SerializeField] private Canvas canvas;
    [SerializeField] private Canvas UI;
    [SerializeField] private Hand Hand;

    [Header("Inventory menu")]
    [SerializeField] private Object iconPrefab;
    [SerializeField] private InventoryItem currentlySelected;
    [SerializeField] private Text currentlySelectedName;
    [SerializeField] private Text currentlySelectedDesc;
    [SerializeField] private Text capacityText;
    private List<GameObject> inventoryIcons = new List<GameObject>();
    [SerializeField] private int spacing;
    private int currentlySelectedIndex;

    public List<InventoryItem> InventoryList
    {
        get => inventoryList;
    }

    public bool Full
    {
        get => full;
        set => full = value;
    }

    public int Size
    {
        get => size;
        set => size = value;
    }

    public int Capacity
    {
        get => capacity;
        set => capacity = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!canvas.enabled) {OpenInventory(); } else { CloseInventory(); }
        }
        
        if (Input.GetKeyDown(KeyCode.M) && canvas.enabled)
        {
            Remove(currentlySelected);
            if (!ShiftItems(-1)) ShiftItems(1);
        }
    }

    public bool Store(InventoryItem item)
    {
        if (full) return false;
        size++;
        if (size == capacity) full = true;
        
        inventoryList.Add(item);
        capacityText.text = "Used space: " + size + " / " + capacity;
        if (!itemPrefabMap.ContainsKey(item.ID)) 
        {
            itemPrefabMap.Add(item.ID, item.Prefab);
        }
        
        return true;
    }

    public (bool, Object) Remove(InventoryItem item)
    {
        if (size == 0 || !inventoryList.Contains(item)) return (false, null);
        size--;
        if (full) full = false;

        inventoryList.Remove(item);
        capacityText.text = "Used space: " + size + " / " + capacity;

        if (size == 0)
        {
            currentlySelectedName.text = "EMPTY";
            currentlySelectedDesc.text = "Go pick up some stuff, then come back here.";
            CloseInventory();
        }
        return (true, item.Prefab);
    }
    
    public (bool, Object) Remove(int id)
    {
        if (size == 0 || !itemPrefabMap.TryGetValue(id, out Object obj)) return (false, null);
        size--;
        if (full) full = false;

        foreach (var item in inventoryList.Where(item => item.ID == id))
        {
            inventoryList.Remove(item);
            break;
        }
        
        return (true, obj);
    }

    public void OpenInventory()
    {
        canvas.enabled = true;
        UI.enabled = false;
        Time.timeScale = 0;
        var currX = 960;
        capacityText.text = "Used space: " + size + " / " + capacity;
        
        foreach (var item in inventoryList)
        {
            GameObject icon = Instantiate(iconPrefab, new Vector3(currX, 640, 0), Quaternion.identity, canvas.transform) as GameObject;
            icon.GetComponent<Image>().sprite = item.InventoryIcon;
            inventoryIcons.Add(icon);
            if (currX == 960) currentlySelected = item;
            currX += spacing;
        }

        currentlySelectedName.text = size == 0 ? "EMPTY" : currentlySelected.Name;
        currentlySelectedDesc.text = size == 0 ? "Go pick up some stuff, then come back here." : currentlySelected.Description;
        currentlySelectedIndex = 0;
    }

    public void CloseInventory()
    {
        canvas.enabled = false;
        UI.enabled = true;
        Time.timeScale = 1;

        foreach (var icon in inventoryIcons)
        {
            Destroy(icon);
        }

        currentlySelected = null;
        currentlySelectedIndex = 0;
        currentlySelectedName.text = null;
        currentlySelectedDesc.text = null;
    }

    public bool ShiftItems(int i)
    {
        if (!canvas.enabled || size == 0) return false;
        if (currentlySelectedIndex == 0 && i < 0 || currentlySelectedIndex == capacity - 1 && i > 0) return false;
        
        foreach (var icon in inventoryIcons)
        {
            var currentPos = icon.transform.position;
            currentPos = new Vector3(currentPos.x + i * spacing, currentPos.y, currentPos.z);
            icon.transform.position = currentPos;
        }

        currentlySelectedIndex += i;
        currentlySelected = inventoryList[currentlySelectedIndex];
        currentlySelectedName.text = currentlySelected.Name;
        currentlySelectedDesc.text = currentlySelected.Description;

        return true;
    }

    public void RemoveToHand(InventoryItem item)
    {
        if (Hand.isHolding) Hand.PutDown(Hand.heldObject);
        (bool success, var prefab) = Remove(item);
        if (!success)
        {
            Debug.LogError("An error occured while trying to remove the item from inventory.");
            return;
        }
        
        Hand.PickUp(Instantiate(prefab) as GameObject);
        
    }
}
