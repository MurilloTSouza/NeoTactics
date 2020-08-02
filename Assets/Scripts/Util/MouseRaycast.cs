using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast
{
    public static GameObject GetPointerCollider()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        } else { return null; }
    }

    public static T GetPointerCollider<T>()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach(RaycastHit hit in hits)
        {
            T obj = hit.collider.GetComponent<T>();
            if(obj != null) { return obj; }
        }
        return default(T);
    }
}
