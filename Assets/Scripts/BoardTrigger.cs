using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTrigger : MonoBehaviour
{

    private void Awake()
    {
        InitWidget();
    }

    private GameObject _dieEffect;

    private void InitWidget()
    {
        _dieEffect = Resources.Load<GameObject>("Effects/SoulExplosionGreen");
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        //Debug.Log(coll.gameObject.name);
        if (coll.tag == "Belt" && coll.GetComponent<DisableBelt>() == null)
        {
            Debug.Log("消除");
            //消除板子.
            ObjectPool.Instance.IntoPool(coll.gameObject);
        }

        if (coll.CompareTag("Player"))
        {
            //玩家死亡.
            coll.GetComponent<PlayerCharacter>().Die();
            ObjectPool.Instance.GetPool(_dieEffect, coll.transform.position+new Vector3(0,1,0), Quaternion.Euler(-90, 0, 0));
        }
    }
}
