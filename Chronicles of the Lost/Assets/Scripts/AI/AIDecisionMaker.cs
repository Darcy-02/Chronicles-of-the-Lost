
using UnityEngine;

public class AIDecisionMaker
{
    public enum Action
    {
        MoveCloser,
        Punch,
        Kick,
        Block
    }


    public Action Decide(float distance, float health)
    {
        // Defend when health is low
        if (health < 20f)
        {
            return Action.Block;
        }

        // Move closer when player is far away
        if (distance > 3f)
        {
            return Action.MoveCloser;
        }

        // Choose an attack more often
        float decision = Random.value;

        if (decision < 0.6f)
        {
            return Action.Punch;
        }
        else if (decision < 0.9f)
        {
            return Action.Kick;
        }
        else
        {
            return Action.Block;
        }
    }

}

