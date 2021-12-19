using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private Dictionary<InteractionMode, Sprite> crosshairList;
    [SerializeField] private Image crosshair;
    private RaycastHit hitData;
    private bool interacting = false;
    private GameObject hitObject;
    [SerializeField] private LayerMask mask;
    // Start is called before the first frame update
    
    // Crosshairs
    [SerializeField] private Sprite c_default;
    [SerializeField] private Sprite c_warn;
    [SerializeField] private Sprite c_locked;
    [SerializeField] private Sprite c_examine;
    [SerializeField] private Sprite c_pickup;
    

    private void Awake()
    {
        crosshairList = new Dictionary<InteractionMode, Sprite>()
        {
            {InteractionMode.None, c_default},
            {InteractionMode.Warn, c_warn},
            {InteractionMode.Locked, c_locked},
            {InteractionMode.Examine, c_examine},
            {InteractionMode.Pickup, c_pickup}
        };
    }

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
            var outline = hitObject.GetComponent<Outline>();
            outline.enabled = true;
            ReplaceCrosshair(outline.InteractionMode);
        } else if (!hit && interacting)
        {
            interacting = false;
            hitObject.GetComponent<Outline>().enabled = false;
            ReplaceCrosshair(InteractionMode.None);
        }
    }

    void ReplaceCrosshair(InteractionMode mode)
    {
        if (crosshairList.TryGetValue(mode, out Sprite sprite))
        {
            crosshair.sprite = sprite;
        }
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
