using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityProp 
{
    public static void Release(PlayerCharacter player)
    {
        if (player.PlayerId == 1)
        {
            Vector3 dst = new Vector3(GameObject.Find("Camera2").transform.position.x, GameObject.Find("Camera2").transform.position.y + 8, 0);
            var clone = ObjectPool.Instance.GetPool(PropManager.Instance.Gravity, dst, Quaternion.Euler(0, 0, 180));
            clone.transform.SetParent(GameObject.Find("Camera2").transform);
            GameManager.Instance.Player2.GetComponent<Rigidbody2D>().gravityScale = 1.35f;
            GameManager.Instance.DeCoroutine(GoBackGravity(GameManager.Instance.Player2,clone));
        }
        else if (player.PlayerId == 2)
        {
            Vector3 dst = new Vector3(GameObject.Find("Main Camera").transform.position.x, GameObject.Find("Main Camera").transform.position.y + 8, 0);
            var clone = ObjectPool.Instance.GetPool(PropManager.Instance.Gravity, dst, Quaternion.Euler(0,0,180));
            clone.transform.SetParent(GameObject.Find("Main Camera").transform);
            GameManager.Instance.Player1.GetComponent<Rigidbody2D>().gravityScale = 1.2f;
            GameManager.Instance.DeCoroutine(GoBackGravity(GameManager.Instance.Player1,clone));
        }
    }

    private static IEnumerator GoBackGravity(PlayerCharacter player,GameObject go)
    {
        yield return new WaitForSeconds(2);
        player.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        go.transform.gameObject.SetActive(false);
    }
    
}
