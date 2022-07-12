using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float groundDistance = 0.05f;
    public bool jump;
    public bool onGround;
    public LayerMask groundLayerMask;
    private float verticalVelocity;
    private float gravity = -9.8f;
    private Vector3 movement;
    private Vector3 lastDirection;
    private Animator animator;
    [HideInInspector] public CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        movement = new Vector3(0, 0, Input.GetAxis("Horizontal"));
        JumpAndGravity();
        Rotate();
        Move();
    }

    private void Move()
    {
        characterController.Move(movement * speed * Time.deltaTime + new Vector3(0, verticalVelocity, 0));
        animator.SetFloat("Movement", movement.magnitude);
        
    }
  
    private void Rotate()
    {
        if (Mathf.Abs(movement.magnitude) <= 0.1f && Mathf.Abs(movement.magnitude) > 0.0f)
            lastDirection = movement.z > 0 ? new Vector3(0, 0, 1) : new Vector3(0, 0, -1);

        if (movement.magnitude != 0)
        {
            Quaternion lerp = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * rotationSpeed);
            transform.rotation = lerp;
        }
        else
        {
            if(lastDirection.magnitude != 0)
            {
                Quaternion lerp = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lastDirection), Time.deltaTime * rotationSpeed);
                transform.rotation = lerp;
                if (IsReady())
                    lastDirection = Vector3.zero;
            }
        }
        
        
    }
    
    private void JumpAndGravity()
    {
        onGround = Physics.CheckSphere(transform.position, groundDistance, groundLayerMask);
        verticalVelocity = onGround? 0 : gravity;
    }

    public bool IsReady()
    {
        if (Vector3.Angle(transform.forward, Vector3.back) <= 1 || Vector3.Angle(transform.forward, Vector3.forward) <= 1)
            return true;
        return false;
    }
}
