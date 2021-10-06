using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hm = Input.GetAxisRaw("Horizontal");
        float vm = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(hm, 0, vm) * speed * Time.deltaTime;

        transform.position += movement;
    }
}
