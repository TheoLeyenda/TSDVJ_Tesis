using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void HorizontalMove(bool isPositve)
    {
        float xMov;

        if (isPositve)
            xMov = 1 * speed * Time.deltaTime;
        else
            xMov = -1 * speed * Time.deltaTime;

        rb.velocity = new Vector3(xMov, rb.velocity.y, rb.velocity.z);
    }

    public void ForwardMove(bool isPositve)
    {
        float zMov;

        if (isPositve)
            zMov = 1 * speed * Time.deltaTime;
        else
            zMov = -1 * speed * Time.deltaTime;

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, zMov);
    }

    public void NoInput()
    {
        rb.velocity = Vector3.zero;
    }
}
