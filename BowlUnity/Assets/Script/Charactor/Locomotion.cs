using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{

    //十字キーのみで操作(上下矢印キー＝前後，左右矢印キー＝回転)
    //CharacterControllerが必要

    public float speed = 6.0F;          //歩行速度
    public float jumpSpeed = 8.0F;      //ジャンプ力
    public float gravity = 20.0F;       //重力の大きさ
    public float rotateSpeed = 3.0F;    //回転速度

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;
    private float h, v;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }//Start()

    // Update is called once per frame
    void Update()
    {

        h = Input.GetAxis("Horizontal");    //左右矢印キーの値(-1.0~1.0)
        v = Input.GetAxis("Vertical");      //上下矢印キーの値(-1.0~1.0)

        if (controller.isGrounded)
        {
            gameObject.transform.Rotate(new Vector3(0, rotateSpeed * h, 0));
            moveDirection = speed * v * gameObject.transform.forward;
            if (Input.GetButton("Jump"))
            {
                animator.SetTrigger("jump");
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        animator.SetFloat("speed", Mathf.Abs(v));

    }//Update()

}