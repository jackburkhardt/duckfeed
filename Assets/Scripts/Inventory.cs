using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [Header("Inventory system")]
    // Current size of the inventory
    [SerializeField] private int size;
    
    // Max capacity of the inventory
    [SerializeField] private int capacity;
    
    // List of all items in the inventory
    [SerializeField] private List<InventoryItem> inventoryList;
    
    private bool full;
    
    // map of item ID to their prefab (this ended up being needed bc having prefabs
    // reference themselves does not really work as intended)
    private Dictionary<int, Object> itemPrefabMap = new Dictionary<int, Object>();
    
    // Inventory canvas
    [SerializeField] private Canvas canvas;
    
    // Main UI canvas
    [SerializeField] private Canvas UI;
    
    // the Hand for hand-ling (get it?) taking and putting objects from inventory into hand
    [SerializeField] private Hand Hand;

    [Header("Inventory menu")]
    // basic prefab for inventory icons (basically an image)
    [SerializeField] private Object iconPrefab;
    
    // Current highlighted item in the inventory
    [SerializeField] private InventoryItem currentlySelected;
    [SerializeField] private Text currentlySelectedName;
    [SerializeField] private Text currentlySelectedDesc;
    private int currentlySelectedIndex;
    
    // UI text for showing inventory capacity
    [SerializeField] private Text capacityText;
    
    // dictionary of inventory icons to their item IDs (since the image for IDs is different than the
    // GameObject used to represent them on the canvas)
    private Dictionary<int, GameObject> inventoryIcons = new Dictionary<int, GameObject>();
    
    // Horizontal spacing between inventory icons
    [SerializeField] private int spacing;

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
            if (!canvas.enabled) { OpenInventory(); } else { CloseInventory(); }
        }
        
        if (Input.GetKeyDown(KeyCode.M) && canvas.enabled)
        {
            Remove(currentlySelected);
            // If we can't shift the inventory one way, try the other.
            if (!ShiftItems(-1)) ShiftItems(1);
        }
        
        if (Input.GetKeyDown(KeyCode.P) && canvas.enabled)
        {
            RemoveToHand(currentlySelected);
            if (!ShiftItems(-1)) ShiftItems(1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShiftItems(-1); 
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShiftItems(1);
        }
    }

    /// <summary>
    /// Stores an item into the inventory.
    /// </summary>
    /// <param name="item">The InventoryItem to be stored.</param>
    /// <returns>True if successful, false if storage failed.</returns>
    public bool Store(InventoryItem item)
    {
        if (full) return false;
        size++;
        if (size == capacity) full = true;
        
        inventoryList.Add(item);
        capacityText.text = "Used space: " + size + " / " + capacity;
        if (!itemPrefabMap.ContainsKey(item.ID)) 
        {
            // Is this map necessary? Probably not.
            // Update: yes it was lol
            itemPrefabMap.Add(item.ID, item.Prefab);
        }
        
        // Should I be throwing proper exceptions/errors for this stuff? Probably. Next time, I guess.
        return true;
    }

    /// <summary>
    /// Removes an item from the inventory. Will not restore the item to the player; for that, see
    /// RemoveToHand.
    /// </summary>
    /// <param name="item">The InventoryItem to be removed.</param>
    /// <returns>A tuple with a bool (True/False depending on removal success) and an Object
    /// (null if removal failed, Object prefab if success).</returns>
    public (bool, Object) Remove(InventoryItem item)
    {
        if (size == 0 || !inventoryList.Contains(item)) return (false, null);
        size--;
        if (full) full = false;

        inventoryList.Remove(item);
        if (inventoryIcons.TryGetValue(item.ID, out var gameObj)) { Destroy(gameObj); }
        inventoryIcons.Remove(item.ID);
        capacityText.text = "Used space: " + size + " / " + capacity;

        if (size == 0)
        {
            currentlySelectedName.text = "EMPTY";
            currentlySelectedDesc.text = "Go pick up some stuff, then come back here.";
            //CloseInventory();
        }

        if (!itemPrefabMap.TryGetValue(item.ID, out Object obj))
        {
            Debug.LogError("An error occured while trying to get this object's prefab from the map.");
        }
        return (true, obj);
    }
    
    /// <summary>
    /// Removes an item from the inventory. Will not restore the item to the player; for that, see
    /// RemoveToHand.
    /// </summary>
    /// <param name="id">The ID number of the InventoryItem to be removed.</param>
    /// <returns>A tuple with a bool (True/False depending on removal success) and an Object
    /// (null if removal failed, Object prefab if success).</returns>
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

    /// <summary>
    /// Opens the Inventory UI to the player.
    /// </summary>
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
            inventoryIcons.Add(item.ID, icon);
            if (currX == 960) currentlySelected = item;
            currX += spacing;
        }

        currentlySelectedName.text = size == 0 ? "EMPTY" : currentlySelected.Name;
        currentlySelectedDesc.text = size == 0 ? "Go pick up some stuff, then come back here." : currentlySelected.Description;
        currentlySelectedIndex = 0;
    }

    /// <summary>
    /// Closes the inventory UI to the player.
    /// </summary>
    public void CloseInventory()
    {
        canvas.enabled = false;
        UI.enabled = true;
        Time.timeScale = 1;

        foreach (var icon in inventoryIcons)
        {
            Destroy(icon.Value);
        }

        inventoryIcons.Clear();
        currentlySelected = null;
        currentlySelectedIndex = 0;
        currentlySelectedName.text = null;
        currentlySelectedDesc.text = null;
    }

    /// <summary>
    /// Shift items either left or right in the inventory UI (carousel style).
    /// </summary>
    /// <param name="i">Either -1 or 1 to indicate direction of shift</param>
    /// <returns>True if successful, false if the shift failed</returns>
    bool ShiftItems(int i)
    {
        if (!canvas.enabled || size == 0) return false;
        if (currentlySelectedIndex == 0 && i < 0 || currentlySelectedIndex == size - 1 && i > 0) return false;
        
        foreach (var iconPair in inventoryIcons)
        {
            var icon = iconPair.Value;
            var currentPos = icon.transform.position;
            currentPos = new Vector3(currentPos.x + -i * spacing, currentPos.y, currentPos.z);
            icon.transform.position = currentPos;
        }

        currentlySelectedIndex += i;
        currentlySelected = inventoryList[currentlySelectedIndex];
        currentlySelectedName.text = currentlySelected.Name;
        currentlySelectedDesc.text = currentlySelected.Description;

        return true;
    }

    /// <summary>
    /// Removes an item from the inventory and places it into the player's hand, dropping the
    /// currently held item (if applicable).
    /// </summary>
    /// <param name="item">The InventoryItem to be removed.</param>
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
