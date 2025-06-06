using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : State
{

    public PlayerCrouchState(PlayerStateManager context): base(context){
    }

    public override void Enter()
    {
        context.animator.SetBool("isCrouching", true);
        context.rigidbody2D.linearVelocity = new Vector2(0, 0);
    }

    public override void UpdateState()
    {
        if(context.targetYaxis >= 0){
            context.ChangeState(context.groundState);
        }
    }

    public override void Exit()
    {
         context.animator.SetBool("isCrouching", false);
    }

    public override void OnJump(){
        if(OneWayPlatformCheck()){
            return;
        }
        context.ChangeState(context.groundState);
    }


    public override Quaternion getTargetRotation(){
        return Quaternion.Euler(0, context.transform.rotation.eulerAngles.y, 0);
    }
    bool OneWayPlatformCheck(){
        Vector2 start = context.transform.position;
        Vector2 dir = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(start, dir, 1f, LayerMask.GetMask("Ground")); 

        if (hit.collider != null && hit.collider.CompareTag("OneWayPlatform"))
        {
            context.FallOneWayPlatform(hit.collider);
            return true;
        }

        return false;
    }
}