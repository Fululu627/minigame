using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBelt : BeltBase
{

    public override void OnGround(PlayerCharacter player)
    {
        player.BaseJumpSpeed = Data.Instance.NormalSpeed;
        StartCoroutine(HideSelf());
    }

    private IEnumerator HideSelf()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

}
