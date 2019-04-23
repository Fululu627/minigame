using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProp  {

	public static void Release(PlayerCharacter player)
    {
        if (player.PlayerId == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 dst = new Vector2(Random.Range(5.1f, 12.9f), GameObject.Find("Camera2").transform.position.y + 7);
                var clone = ObjectPool.Instance.GetPool(PropManager.Instance.Lighnting1, dst, Quaternion.identity);
                clone.transform.SetParent(GameObject.Find("Camera2").transform);
             

                GameManager.Instance.DeCoroutine(WaitLighnting(GameManager.Instance.Player2,dst,clone));
            }
        }
        if(player.PlayerId == 2)
        {
            for (int i = 0; i <5; i++)
            {
                Vector2 dst = new Vector2(Random.Range(-8f, -1f), GameObject.Find("Main Camera").transform.position.y + 7);
                var clone = ObjectPool.Instance.GetPool(PropManager.Instance.Lighnting1, dst, Quaternion.identity);
                clone.transform.SetParent(GameObject.Find("Main Camera").transform);


                GameManager.Instance.DeCoroutine(WaitLighnting(GameManager.Instance.Player1, dst, clone));
            }
        }
        AudioManager.Instance.PlaySound(20);
    }

    public static IEnumerator WaitLighnting(PlayerCharacter player, Vector2 vec,GameObject go)
    {
        yield return new WaitForSeconds(1.5f);
        go.SetActive(false);
        var clone= ObjectPool.Instance.GetPool(PropManager.Instance.Lighnting2, vec, Quaternion.Euler(0, 0, 90));
        
        RaycastHit2D[] hitInfo;
        hitInfo = Physics2D.RaycastAll(vec, new Vector2(0, -1));
        foreach (var hit in hitInfo)
        {
            if (hit.collider.tag == "Player")
            {
                if (hit.transform.GetComponent<PlayerCharacter>().PlayerId == 1)
                {
                    PlayerController.Instance.canMove1 = false;
                    var clone1 = ObjectPool.Instance.GetPool(PropManager.Instance.CircleStar, hit.transform.position + new Vector3(0, 1, 0), Quaternion.Euler(90,0,44));
                    clone1.transform.SetParent(hit.transform);
                    yield return new WaitForSeconds(3f);
                    PlayerController.Instance.canMove1 = true;
                    clone1.transform.gameObject.SetActive(false);
                }
                if (hit.transform.GetComponent<PlayerCharacter>().PlayerId == 2)
                {
                    PlayerController.Instance.canMove2 = false;
                    var clone2 = ObjectPool.Instance.GetPool(PropManager.Instance.CircleStar, hit.transform.position + new Vector3(0, 1, 0), Quaternion.Euler(90, 0, 44));
                    clone2.transform.SetParent(hit.transform);
                    yield return new WaitForSeconds(3f);
                    PlayerController.Instance.canMove2 = true;
                    clone2.transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
