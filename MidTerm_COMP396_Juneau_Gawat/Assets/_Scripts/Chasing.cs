using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour
{
    public float seeDist;
    public float speed = 2f;

    private SphereCollider sc;
    private Rigidbody rb;
    private GameObject player;
    [SerializeField]
    private Material red;
    [SerializeField]
    private Material blue;
    private MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<SphereCollider>();
        sc.radius = seeDist;

        rb = GetComponent<Rigidbody>();

        mr = GetComponent<MeshRenderer>();

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player") {
            transform.LookAt(player.transform.position);
            Debug.Log(player.transform.position);
            rb.velocity = transform.forward * speed;
            mr.material = red;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            mr.material = blue;
        }
    }
}
