using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // is the player currently holding an object?
    public bool isHolding;
    
    // the object being held (if any)
    public GameObject heldObject;
    
    [SerializeField] private Crosshair Crosshair;
    [SerializeField] private Inventory Inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E!");
            if (Crosshair.IsInteracting() && !isHolding)
            {
                // if something selected and nothing in hand, pick up item
               PickUp(Crosshair.HitObject()); 
            } else if (Crosshair.IsInteracting() && isHolding)
            {
                // if something in hand but player wants to pick up, swap items
               PutDown(heldObject);
               PickUp(Crosshair.HitObject());
            } else if (isHolding)
            {
                // if something in hand and nothing selected, drop item
                PutDown(heldObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && isHolding)
        {
            Debug.Log(heldObject);
            Inventory.Store(heldObject.GetComponent<InventoryItem>());
            Destroy(heldObject);
            isHolding = false;
        }
    }
    
    /// <summary>
    /// Puts the selected item into the player's hand.
    /// </summary>
    /// <param name="item">The GameObject to be picked up.</param>
    public void PickUp(GameObject item)
    {
        Debug.Log("picked up");
        heldObject = item;
        isHolding = true;
        // does a bunch of stuff to "freeze" the object
        item.transform.parent = this.transform;
        item.transform.position = this.transform.position;
        item.transform.rotation = Quaternion.identity;
        if (item.GetComponent<Rigidbody>())
        {
            Rigidbody itemRB = item.GetComponent<Rigidbody>();
            itemRB.useGravity = false;
            itemRB.constraints = RigidbodyConstraints.FreezePosition;
            itemRB.freezeRotation = true;
        }
    }

    /// <summary>
    /// Throws an object out of the player's hand.
    /// </summary>
    /// <param name="item">The GameObject to be removed.</param>
    public void PutDown(GameObject item)
    {
        Debug.Log("put down");
        // undoes the rigidbody restrictions
        if (item.GetComponent<Rigidbody>())
        {
            Rigidbody itemRB = item.GetComponent<Rigidbody>();
            itemRB.useGravity = true;
            itemRB.constraints = RigidbodyConstraints.None;
            itemRB.freezeRotation = false;
            itemRB.AddForce(Crosshair.transform.forward * 5, ForceMode.Impulse);
        }
        item.transform.parent = null;
        isHolding = false;
        heldObject = null;
    }
}
