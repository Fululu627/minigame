using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTimeProp  {

    public static void Release(PlayerCharacter player)
    {
        
        var tran1 = GameObject.Find("Main Camera").transform;
        var tran2 = GameObject.Find("Camera2").transform;
        ObjectPool.Instance.GetPool(PropManager.Instance.SlowTime, new Vector3 (tran1.position.x+1,tran1.position.y), Quaternion.identity);
        ObjectPool.Instance.GetPool(PropManager.Instance.SlowTime, new Vector3(tran2.position.x - 1, tran2.position.y), Quaternion.identity);
        GameManager.Instance.DeCoroutine(GoBack(player));
    }

    private static IEnumerator GoBack(PlayerCharacter player)
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 1f;
    }
}
