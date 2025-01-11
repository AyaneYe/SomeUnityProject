using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb =GetComponent<Rigidbody2D>();
        physicsCheck= GetComponent<PhysicsCheck>();
        playerController=GetComponent<PlayerController>();
    }

    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", physicsCheck.isGrounded);
        anim.SetFloat("Speed", playerController.speed);
        anim.SetBool("isCrouch", playerController.isCrouch);
        anim.SetBool("isDead", playerController.isDead);
        anim.SetBool("isAttack", playerController.isAttack);
    }

    //Update只能单词执行，需拆分出来
    public void PlayerHurt()
    {
        anim.SetTrigger("hurt");
    }

    public void PlayerAttack()
    {
        anim.SetTrigger("Attack");
    }
}
