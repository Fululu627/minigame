using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateBelt : BeltBase
{

    public override void OnGround(PlayerCharacter player)
    {
        player.BaseJumpSpeed = Data.Instance.HighSpeed;
    }
}
