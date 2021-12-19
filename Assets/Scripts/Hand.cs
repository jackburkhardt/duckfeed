using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isHolding;
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
               PickUp(Crosshair.HitObject()); 
            } else if (isHolding)
            {
               PutDown(Crosshair.HitObject()); 
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && isHolding)
        {
            Debug.Log(heldObject);
            Inventory.Store(heldObject.GetComponent<InventoryItem>());
            Destroy(heldObject);
        }
    }
    
    public void PickUp(GameObject item)
    {
        Debug.Log("picked up");
        heldObject = item;
        isHolding = true;
        item.transform.parent = this.transform;
        item.transform.position = this.transform.position;
        if (item.GetComponent<Rigidbody>())
        {
            Rigidbody itemRB = item.GetComponent<Rigidbody>();
            itemRB.useGravity = false;
            itemRB.rotation = this.transform.rotation;
            itemRB.constraints = RigidbodyConstraints.FreezePosition;
            itemRB.freezeRotation = true;
        }
    }

    public void PutDown(GameObject item)
    {
        Debug.Log("put down");
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
