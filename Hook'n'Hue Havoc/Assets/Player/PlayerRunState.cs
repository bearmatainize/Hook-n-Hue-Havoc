using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player) {
        player.playerAnim.ResetTrigger("Idle");
        player.playerAnim.SetTrigger("Run");
    }

    public override void UpdateState(PlayerStateManager player) {
        player.thirdPersonController.movePivot();
        player.thirdPersonController.moveController();

        if(player.thirdPersonController.xInputMovement * player.thirdPersonController.xInputMovement + player.thirdPersonController.yInputMovement * player.thirdPersonController.yInputMovement == 0) {
            player.SwitchState(player.IdleState);
        }
        // Check for input to fire the grapple hook
        if (player.playerInput.actions["Grapple"].IsPressed())
        {
            player.SwitchState(player.GrappleState);
        }
    }
}
