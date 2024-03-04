using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer2 : MonoBehaviour
{
    public float speed = 10.0f; // Movement speed
    public float rotationSpeed = 100.0f; // Rotation speed
    public float jumpForce = 10.0f; // Jump force
    private Rigidbody rb;
    public float pushForce = 3.0f; // Push force
    private ControllerColliderHit contact;
    public InventoryManager inventoryManager;
    private float lastKeyPressTime = 0;
    private float doublePressDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Prevent the Rigidbody from rotating in the X and Z axes
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
void OnControllerColliderHit(ControllerColliderHit hit){
    contact = hit;
    Rigidbody body = hit.collider.attachedRigidbody;
    if (body != null && !body.isKinematic){
        body.velocity = hit.moveDirection * pushForce;
    }
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (Time.time - lastKeyPressTime < doublePressDelay){
                string equippedItem = inventoryManager.equippedItem;
                if (equippedItem != null){
                    Debug.Log("Using " + equippedItem);
                    inventoryManager.ConsumeItem(equippedItem);
                }
            }
            lastKeyPressTime = Time.time;
        }
    }

}