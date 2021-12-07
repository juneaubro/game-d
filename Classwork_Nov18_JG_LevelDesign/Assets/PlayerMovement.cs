using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float gravity = 8f;
    private CharacterController cc;
    private float yVel = 0f;
    private void Start() {
        cc = GetComponent<CharacterController>();
    }
    private void FixedUpdate() {
        MovePlayer();
    }
    private void MovePlayer() {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal")*speed * Time.deltaTime, yVel/1000, Input.GetAxisRaw("Vertical")*speed * Time.deltaTime).normalized;
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.z;
        if (cc.isGrounded)
            yVel = 0f;
            yVel = -(Physics.gravity.y * Physics.gravity.y * gravity * Time.deltaTime);
        move.y = yVel;
        cc.Move(move);
    }
}
