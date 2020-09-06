using UnityEngine;

public class DropDashPlayerState : PlayerState
{
    private float power;
    private bool performDropDash;

    public override void Enter(Player player)
    {
        player.attacking = true;
        player.ChangeBounds(1);
        player.PlayAudio(player.audios.spinDashCharge, 0.5f);

        power = player.stats.dropdashPower;
        performDropDash = true;
    }

    public override void Step(Player player, float deltaTime)
    {
        player.UpdateDirection(player.input.horizontal);
        player.HandleAcceleration(deltaTime);
        player.HandleGravity(deltaTime);

        if (!player.grounded && player.attacking)
        {
            if (player.input.actionUp)
            {
                performDropDash = false;
                player.state.ChangeState<JumpPlayerState>();
            }
        }
        else
        {
            if (performDropDash)
            {
                player.state.ChangeState<RollPlayerState>();
            }
            else
            {
                player.state.ChangeState<WalkPlayerState>();
            }
        }
    }

    public override void Exit(Player player)
    {
        if (performDropDash)
        {
            player.velocity.x = (player.stats.minReleasePower + (Mathf.Floor(power) / 2)) * player.direction;
            player.PlayAudio(player.audios.spinDashRelease, 0.5f);
        }
    }
}
