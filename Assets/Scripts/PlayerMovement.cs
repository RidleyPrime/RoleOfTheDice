using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Jump")) {
            rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y + 5, rb.velocity.z);
        }

        if (Input.GetKeyDown("W"))
        {
            rb.velocity = new Vector3(rb.velocity.x + 5, rb.velocity.y, rb.velocity.z);
        }
    }
}
