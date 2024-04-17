using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player) {
        player.playerAnim.ResetTrigger("Idle");
        player.playerAnim.SetTrigger("Run");
    }

    public override void UpdateState(PlayerStateManager player) {
        // Move and rotate the player based on input
        player.firstPersonController.Update();

        if (Mathf.Approximately(player.firstPersonController.xInput, 0f) && Mathf.Approximately(player.firstPersonController.zInput, 0f))
        {
            player.SwitchState(player.IdleState);
        }
        // Check for input to fire the grapple hook
        if (player.playerInput.actions["Grapple"].IsPressed())
        {
            player.SwitchState(player.GrappleState);
        }
    }
}
