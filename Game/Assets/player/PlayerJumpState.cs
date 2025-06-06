using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : State
{
    private int targetXaxis = 0; 
    
    private enum Direction
    {
        Left = -1,
        Right = 1
    }
    private Direction direction = Direction.Right;

    public PlayerJumpState(PlayerStateManager context): base(context){
    }
  
    public override void Enter()
    {
        context.rigidbody2D.linearDamping = 2f; 

        context.animator.SetBool("isGrounded", false);
        context.rigidbody2D.linearVelocity = new Vector2(context.rigidbody2D.linearVelocity.x, context.jumpForce);
        context.isGrounded = false;
    }

    public override void UpdateState()
    {
        if(!context.isDashing){
            context.rigidbody2D.linearVelocity = new Vector2(context.move * context.airSpeed, context.rigidbody2D.linearVelocity.y);
        }
        
        if(context.isGrounded){
            context.animator.SetBool("isGrounded", true);
            context.rigidbody2D.linearVelocity = new Vector2(0, 0);
            context.ChangeState(context.groundState);
        }
    }

    public override void Exit()
    {
        context.animator.SetBool("isGrounded", false);
    }

    public override void OnJump()
    {
        if (context.canDoubleJump && !context.hasDoubleJumped)
        {
            context.rigidbody2D.linearVelocity = new Vector2(context.rigidbody2D.linearVelocity.x, context.jumpForce);
            context.hasDoubleJumped = true;
            context.animator.SetTrigger("jump");
        }
    }

    public override Quaternion getTargetRotation(){
        return Quaternion.Euler(0, context.transform.rotation.eulerAngles.y, 0);
    }
}
