using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool isHolding;
    private GameObject heldObject;
    [SerializeField] private Crosshair Crosshair;
    
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
    }
    
    void PickUp(GameObject item)
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

    void PutDown(GameObject item)
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
