using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanBelt : BeltBase 
{

    public override void OnGround(PlayerCharacter player)
    {
        player.BaseJumpSpeed = Data.Instance.NormalSpeed;
    }
}
