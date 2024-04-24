using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnim.SetTrigger("Jump");
        player.firstPersonController.Jump();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.firstPersonController.Update();

        if (player.firstPersonController.IsGrounded())
        {
            if (Mathf.Approximately(player.firstPersonController.xInput, 0f) && Mathf.Approximately(player.firstPersonController.zInput, 0f))
            {
                player.SwitchState(player.IdleState);
            }
            else
            {
                player.SwitchState(player.RunState);
            }
        }
    }
}
