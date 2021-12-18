using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Sprite defaultCrosshair;
    [SerializeField] private Sprite interactCrosshair;
    [SerializeField] private Image crosshair;
    private RaycastHit hitData;
    private bool interacting = false;
    private GameObject hitObject;
    [SerializeField] private LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        var hit = Physics.Raycast(ray, out hitData, 10, mask);

        if (hit && !interacting)
        {
            Debug.Log("raycast hit!0");
            interacting = true;
            hitObject = hitData.collider.gameObject;
            ReplaceCrosshair();
            hitObject.GetComponent<Outline>().enabled = true;
        } else if (!hit && interacting)
        {
            interacting = false;
            
            hitObject.GetComponent<Outline>().enabled = false;
            ReplaceCrosshair();
        }
    }

    void ReplaceCrosshair()
    {
        crosshair.sprite = interacting ? interactCrosshair : defaultCrosshair;
    }

    public bool IsInteracting()
    {
        return interacting;
    }

    public RaycastHit HitData()
    {
        return hitData;
    }

    public GameObject HitObject()
    {
        return hitObject;
    }
}
