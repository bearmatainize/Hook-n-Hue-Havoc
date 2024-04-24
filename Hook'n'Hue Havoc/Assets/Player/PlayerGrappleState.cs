using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrappleState : PlayerBaseState
{
    private bool isGrappleFired = false;
    //private bool isGrappleCooldown = false;
    //private float grappleCooldown = 1.0f;
    //private float grappleTimer = 0.0f;

    public override void EnterState(PlayerStateManager player)
    {
        // Initialize grapple hook
        player.grappleHook.Initialize(player.firstPersonController.transform);

        // Subscribe to the performed event of the Grapple action
        player.playerInput.actions["Grapple"].performed += ctx => GrappleStarted(player);
        player.playerInput.actions["Grapple"].canceled += ctx => GrappleCanceled(player);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        
    }

    public void ExitState(PlayerStateManager player)
    {
        // Unsubscribe from the performed event of the Grapple action
        player.playerInput.actions["Grapple"].performed -= ctx => GrappleStarted(player);
        player.playerInput.actions["Grapple"].canceled -= ctx => GrappleCanceled(player);
        player.SwitchState(player.IdleState);
    }

    private void GrappleStarted(PlayerStateManager player)
    {
        // Fire the grapple if it is not already fired
        if (!isGrappleFired)
        {
            player.grappleHook.FireGrapple();
            isGrappleFired = true;
            //isGrappleCooldown = false;
        }
    }

    private void GrappleCanceled(PlayerStateManager player)
    {
        if (isGrappleFired)
        {
            player.grappleHook.ReleaseGrapple();
            ExitState(player);
            isGrappleFired = false;
            //isGrappleCooldown = true;
            //grappleTimer = 0.0f; // Reset the timer
        }
    }
}
