using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsAlive=true;
    [SerializeField]
    float angSpeed = 3f;
    [SerializeField]
    float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mv = Input.GetAxisRaw("Vertical");
        float mh = Input.GetAxisRaw("Horizontal");

        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
        //                                              Quaternion.LookRotation(moveDir),
        //                                              5 * Time.deltaTime);
        this.transform.eulerAngles = new Vector3(0, mh*angSpeed*Time.deltaTime, 0);
        Vector3 movement = new Vector3(mh * speed, 0,mv*speed)*Time.deltaTime;
        this.transform.position += movement;
    }
}
