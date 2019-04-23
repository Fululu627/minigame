using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBelt : BeltBase {

    public override void OnGround(PlayerCharacter player)
    {
        player.BaseJumpSpeed = Data.Instance.LowSpeed;
    }
}
