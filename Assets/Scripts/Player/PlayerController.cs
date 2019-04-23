using System.Collections;
using System.Collections.Generic;
using QxFramework.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 玩家角色控制器.
/// </summary>
public class PlayerController : MonoSingleton<PlayerController>
{
    private void Awake()
    {
        InitWidget();
    }

    private PlayerCharacter _player1;
    private PlayerCharacter _player2;

    public bool canMove1;
    public bool canMove2;

    private void InitWidget()
    {
        _player1 = GameObject.Find("Player1").GetComponent<PlayerCharacter>();
        _player2 = GameObject.Find("Player2").GetComponent<PlayerCharacter>();
        canMove1 = true;
        canMove2 = true;
    }

    //鼠标位置.
    private Vector2 _mousePos;

    private void Update()
    {
        if (!GameManager.Instance.IsGaming)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //准备.
                Debug.Log("玩家1已准备");
                GameManager.Instance.GetReady(1);
                ReadyUI.Instance.ShowJoinedImage(1);
            }

            if (Input.GetButtonDown("Jump")||Input.GetKeyUp(KeyCode.E))
            {
                GameManager.Instance.GetReady(2);
                ReadyUI.Instance.ShowJoinedImage(2);
            }
            return;
        }
        Move(1);
        Move(2);
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }
    }

    private void Move(int player)
    {
        if(player == 1&&canMove1)
        {
            //TODO:判断是否显示指针.
            _mousePos = Input.mousePosition;
            //方向.
            Vector2 dir = Camera.main.ScreenToWorldPoint(_mousePos) - _player1.transform.position;
            //计算角度.
            var angle = Mathf.Acos(dir.normalized.x);
            angle = angle * 180.0f / Mathf.PI;
            //更新指针位置.
            GameUI.Instance.UpdatePointerRot(1, angle);
            //Debug.Log(angle);
            //鼠标左键松开起跳.
            if (Input.GetMouseButtonUp(0))
            {
                _player1.Jump(dir, 10);
            }
            //按下空格下降.
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _player1.Drop();
            }
            //鼠标右键放道具.
            if (Input.GetMouseButtonUp(1))
            {
                _player1.ReleaseProp();
            }
        }

        if(player == 2&&canMove2)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector2 dir = new Vector2(x, y);

            var angle = Mathf.Acos(dir.normalized.x);
            angle = angle * 180.0f / Mathf.PI;

            GameUI.Instance.UpdatePointerRot(2, angle);

            if (Input.GetButtonDown("Jump"))
            {
                _player2.Jump(dir, 10);
            }
            if (Input.GetButtonDown("Drop"))
            {
                _player2.Drop();
            }
            if (Input.GetButtonDown("Prop"))
            {
                _player2.ReleaseProp();
            }
        }
    }


}
