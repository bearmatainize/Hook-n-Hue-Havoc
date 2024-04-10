using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{


    public override void EnterState(PlayerStateManager player) {
        player.playerAnim.ResetTrigger("Run");
        player.playerAnim.SetTrigger("Idle");
    }

    public override void UpdateState(PlayerStateManager player) {
        if(player.thirdPersonController.xInputMovement * player.thirdPersonController.xInputMovement + player.thirdPersonController.yInputMovement * player.thirdPersonController.yInputMovement > 0) {
            player.SwitchState(player.RunState);
        }
        // Check for input to fire the grapple hook
        if (player.playerInput.actions["Grapple"].IsPressed())
        {
            player.SwitchState(player.GrappleState);
        }
        /*if(player.playerMovement.jumpRequest)
        {
            player.SwitchState(player.JumpState);
        }*/
    }
}
