using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool hasPowerUp;
    public float speed;
    public float jumpForce;
    public float gravity;
    private Rigidbody rb;
    private Vector3 Gravity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Gravity = new Vector3(0, gravity, 0);
        Physics.gravity = Gravity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gravity.y != gravity)
            Gravity.y = gravity;

        float hm = Input.GetAxisRaw("Horizontal");
        float vm = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(hm, 0, vm) * speed * Time.deltaTime;

        transform.position += movement;

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }
    void Jump() {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        Debug.Log("as");
    }
}
