using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    [SerializeField]
    string dragTag;

    Rigidbody2D selectableRgb;
    Transform selectedTransform;

    Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Draggable();
        }
        if (Input.GetMouseButtonUp(0))
        {
            DropTheObject();
        }
        
    }
    private void FixedUpdate()
    {
        if (selectableRgb != null)
        {
            DragTheObject
                ();
        }
    }

    void Draggable()
    {
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition));
        if (hit2D && hit2D.collider.tag.Equals(dragTag))
        {
            selectableRgb = hit2D.collider.attachedRigidbody;
            if (selectableRgb == null)
            {
                Debug.LogWarning("Rigidbody yok");
            }
            else
            {
                selectedTransform = hit2D.transform;
            }
        }
    }

    void DragTheObject()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 velocity = selectableRgb.velocity;
        Vector2.SmoothDamp(selectableRgb.position, mousePos, ref velocity, .1f);
        selectableRgb.velocity = velocity;

    }

    void DropTheObject()
    {
        if (selectableRgb == null)
        {
            return;
        }
        selectableRgb = null;

    }
}
