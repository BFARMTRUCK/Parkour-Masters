using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer1 : MonoBehaviour
{
    public float speed = 10.0f; // Movement speed
    public float rotationSpeed = 100.0f; // Rotation speed
    public float jumpForce = 10.0f; // Jump force
    private Rigidbody rb;

    // Start is called before the first frame update
   void Start()
{
    rb = GetComponent<Rigidbody>();
    // Prevent the Rigidbody from rotating in the X and Z axes
    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
}


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.S) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
}