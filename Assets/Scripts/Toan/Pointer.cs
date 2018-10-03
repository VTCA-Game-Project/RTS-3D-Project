using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    private Vector3 position;

    [SerializeField]
    private Camera rtsCamera;

    #region Properties
    public Vector3 Position
    {
        get
        {
            position = transform.position;
            return position;
        }
        set
        {
            value.y = 0;
            transform.position = value;

        }
    }
    #endregion
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = rtsCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray: ray,
                hitInfo: out hitInfo,
                maxDistance: 100.0f,
                layerMask: LayerMask.GetMask("Place")))
            {
                Position = hitInfo.point;
            }
        }

    }
}
