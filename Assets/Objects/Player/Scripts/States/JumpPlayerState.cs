using UnityEngine;

public class JumpPlayerState : PlayerState
{
    bool dropDashFlag;

	public override void Enter(Player player)
	{
		player.attacking = true;
		player.ChangeBounds(1);
        dropDashFlag = false;
	}

	public override void Step(Player player, float deltaTime)
	{
		player.UpdateDirection(player.input.horizontal);
		player.HandleAcceleration(deltaTime);
		player.HandleGravity(deltaTime);

		if (!player.grounded && player.attacking)
		{
			if (player.input.actionUp && player.velocity.y > player.stats.minJumpHeight)
			{
				player.velocity.y = player.stats.minJumpHeight;
                dropDashFlag = true;
			}

            if(player.input.actionDown && dropDashFlag)
            {
                player.state.ChangeState<DropDashPlayerState>();
            }
		}
		else
		{
			player.state.ChangeState<WalkPlayerState>();
		}
	}
}