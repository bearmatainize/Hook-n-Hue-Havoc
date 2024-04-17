using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrappleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        // Initialize grapple hook
        player.grappleHook.Initialize(player.firstPersonController.transform);
        player.grappleHook.FireGrapple();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        // Check for input to release the grapple hook
        if (player.playerInput.actions["Release"].IsPressed())
        {
            player.grappleHook.ReleaseGrapple();
            player.SwitchState(player.IdleState); // Switch back to idle state
        }
    }
}

