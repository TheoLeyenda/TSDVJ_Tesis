using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObjectForMouse : Updateable
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateRotationObjectForMouse);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    // Update is called once per frame
    void UpdateRotationObjectForMouse()
    {
        Rotation();
    }

    private void Rotation()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = Vector3.zero;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            point = hit.point;
        }

        Vector3 relative = transform.InverseTransformPoint(point);
        float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
        transform.Rotate(0, angle, 0);
    }
}
