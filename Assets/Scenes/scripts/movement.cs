using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
private float yaw, pitch;
    private Rigidbody rb;
    public float velocidad, sens, salto;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //camara
        pitch -= Input.GetAxisRaw("Mouse Y") * sens;
        pitch = Mathf.Clamp(pitch, -90, 90);
        yaw += Input.GetAxisRaw("Mouse X") * sens;
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);

        //Saltar
        if (Input.GetKey(KeyCode.Space) && Physics.Raycast(rb.transform.position, Vector3.down, 1))
        {
            rb.velocity = new Vector3(rb.velocity.x, salto, rb.velocity.z);
        }
    }

    private void FixedUpdate()
    {
        //Mover
        Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * velocidad;
        Vector3 forward = new Vector3(-Camera.main.transform.right.z, 0, Camera.main.transform.right.x);
        Vector3 wishDirection = (forward * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rb.velocity.y);
        rb.velocity = wishDirection;
    }
}
