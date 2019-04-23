using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProp  {
    private static Vector2 offset = new Vector2(0, 2.5f);

    public static void Release(PlayerCharacter player)
    {
        if (player.PlayerId == 1)
        {
            //获取玩家2的上一层位置.
            //int floorNum = GameManager.Instance.Player2.CurrentFloor + 1;
            //Vector2 pos = GameManager.Instance.FloorList2[floorNum].transform.position;
            Vector2 dst = new Vector2(GameObject.Find("Camera2").transform.position.x+1, GameObject.Find("Camera2").transform.position.y - 2.35f);
            ObjectPool.Instance.GetPool(PropManager.Instance.FirePrefab, dst, Quaternion.identity);
        }
        else if (player.PlayerId == 2)
        {
            //获取玩家1的上一层位置.
            //int floorNum = GameManager.Instance.Player1.CurrentFloor + 1;
            //Vector2 pos = GameManager.Instance.FloorList1[floorNum].transform.position;
            Vector2 dst = new Vector2(GameObject.Find("Main Camera").transform.position.x+4f, GameObject.Find("Main Camera").transform.position.y - 2.35f);
            ObjectPool.Instance.GetPool(PropManager.Instance.FirePrefab, dst, Quaternion.identity);
        }
        AudioManager.Instance.PlaySound(19);
    }
}
