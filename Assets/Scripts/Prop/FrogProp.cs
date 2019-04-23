using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogProp
{
    private static Vector2 offset = new Vector2(0,2.5f);

    public static void Release(PlayerCharacter player)
    {
        if (player.PlayerId == 1)
        {
            //获取玩家2的上一层位置.
            int floorNum = GameManager.Instance.Player2.CurrentFloor + 1;
            Vector2 dst = new Vector2(9.3f,GameManager.Instance.FloorList2[floorNum]);
            dst += offset;
            ObjectPool.Instance.GetPool(PropManager.Instance.FogPrefab, dst, Quaternion.identity);
        }
        else if (player.PlayerId == 2)
        {
            //获取玩家1的上一层位置.
            int floorNum = GameManager.Instance.Player1.CurrentFloor + 1;
            Vector2 dst = new Vector2(-4.1f, GameManager.Instance.FloorList1[floorNum]);
            dst += offset;
            ObjectPool.Instance.GetPool(PropManager.Instance.FogPrefab, dst, Quaternion.identity);
        }
    }
}
