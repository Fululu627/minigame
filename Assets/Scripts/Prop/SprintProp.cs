using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SprintProp  {

    private static int sprintTime = 1;

	public static void Sprint(PlayerCharacter player)
    {
        if (player.PlayerId == 1)
        {
            GameManager.Instance.CreateFloor(player.PlayerId, GameManager.Instance.GetFloorNum(player.PlayerId)+1);

            player.GetComponent<BoxCollider2D>().isTrigger = true;

            player._canDrop = false;
            player._canJump = false;
            player._isOnGround = false;
            player.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 9.5f * GameManager.Instance.FloorGap, -1), sprintTime);
            var cameraTrans = GameObject.Find("Main Camera").transform;
            cameraTrans.DOMove(new Vector3(-5.57f, cameraTrans.position.y +9f * GameManager.Instance.FloorGap, -10), sprintTime);

            

            GameManager.Instance.DeCoroutine(GoBack(GameManager.Instance.Player1));
        }
        if(player.PlayerId == 2)
        {
            GameManager.Instance.CreateFloor(player.PlayerId, GameManager.Instance.GetFloorNum(player.PlayerId) + 1);

            player.GetComponent<BoxCollider2D>().isTrigger = true;
            player._canDrop = false;
            player._canJump = false;
            player._isOnGround = false;
            player.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 10 * GameManager.Instance.FloorGap, -1), sprintTime);
            var cameraTrans = GameObject.Find("Camera2").transform;
            cameraTrans.DOMove(new Vector3(cameraTrans.position.x, cameraTrans.position.y + 9 * GameManager.Instance.FloorGap,-10), sprintTime);
            

            GameManager.Instance.DeCoroutine(GoBack(GameManager.Instance.Player2));
        }
        AudioManager.Instance.PlaySound(18);
    }

    private static IEnumerator GoBack(PlayerCharacter player)
    {
        yield return new WaitForSeconds(sprintTime);
        player.GetComponent<BoxCollider2D>().isTrigger = false;
        player._canDrop = true;
        player._canJump = true;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
