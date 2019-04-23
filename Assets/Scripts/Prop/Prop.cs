using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// 道具类.
/// </summary>
public class Prop : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            if (player.HaveProp())
            {
                //TODO:提醒玩家现有的道具.
                return;
            }
            AudioManager.Instance.PlaySound(15);
            //吃到道具.
            GameObject clone = ObjectPool.Instance.GetPool(PropManager.Instance.EatPropPrefab,
                transform.position + new Vector3(0, 1, 0),
                Quaternion.Euler(-90, 0, 0));
            ObjectPool.Instance.IntoPool(clone,2);
            //随机化道具.
            int pickNum = UnityEngine.Random.Range(0, 100);
            int id = GetPropId(pickNum);
            player.SetProp(GetAction(id));
            //改图片和信息.
            GameUI.Instance.ShowPropInfo(player.PlayerId, id);

            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 获取一个道具ID
    /// </summary>
    /// <param name="num">随机数</param>
    /// <returns></returns>
    private static int GetPropId(int num)
    {
        int lowLimit = 0;
        int id = 0;
        for (int i = 0; i < 6; i++)
        {
            if (num >= lowLimit && num < TableAgent.Instance.GetInt("Prop", i.ToString(), "Probability"))
            {
                id = i;
                return id;
            }

            lowLimit = TableAgent.Instance.GetInt("Prop", i.ToString(), "Probability");
        }

        return id;
    }

    private Action<PlayerCharacter> GetAction(int id)
    {
        switch (id)
        {
            case 0:
                return FrogProp.Release;
                break;
            case 1:
                return GravityProp.Release;
                break;
            case 2:
                return SlowTimeProp.Release;
                break;
            case 3:
                return FireProp.Release;
                break;
            case 4:
                return LightningProp.Release;
                break;
            case 5:
                return SprintProp.Sprint;
                break;
        }

        return null;
    }
}
