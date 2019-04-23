using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 玩家角色.
/// </summary>
public class PlayerCharacter : MonoBehaviour
{

    [SerializeField]
    private int _playerId;

    public int PlayerId
    {
        get { return _playerId; }
    }

    private int _currentFloor = 0;

    public int CurrentFloor
    {
        get { return _currentFloor; }
    }

    private void Awake()
    {
        InitWidget();
        Init();
    }

    private void Update()
    {
        //TODO:改变Camera
        if (_playerId == 1)
        {
            GameUI.Instance.UpdatePointerPos(_playerId, Camera.main.WorldToScreenPoint(transform.position));
        }
        if(_playerId == 2)
        {
            GameUI.Instance.UpdatePointerPos(_playerId, GameObject.Find("Camera2").GetComponent<Camera>().WorldToScreenPoint(transform.position));
        }
    }

    private Rigidbody2D _rigid2D;

    private Animator _animator;

    private GameObject _dropEffect;
    private GameObject _jumpEffect;
    /// <summary>
    /// 初始化组件.
    /// </summary>
    private void InitWidget()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _dropEffect = Resources.Load<GameObject>("Effects/FlashBomb2");
        _jumpEffect = Resources.Load<GameObject>("Effects/SmokeExplosionWhite");
    }

    /// <summary>
    /// 初始化.
    /// </summary>
    private void Init()
    {
        
    }

    private float _baseJumpSpeed = 1.2f;

    /// <summary>
    /// 跳跃速度基数.
    /// </summary>
    public float BaseJumpSpeed
    {
        get { return _baseJumpSpeed; }
        set { _baseJumpSpeed = value; }
    }

    private readonly Vector3 _jumpEffectOffset = new Vector3(0,-0.33f);

    /// <summary>
    /// 跳跃.
    /// </summary>
    /// <param name="dir">方向</param>
    /// <param name="power">蓄力长度</param>
    public void Jump(Vector2 dir, float power)
    {
        Debug.Log("jump");
        if (!_canJump)
        {
            return;
        }
        //特效.
        GameObject clone = ObjectPool.Instance.GetPool(_jumpEffect, transform.position + _jumpEffectOffset, Quaternion.Euler(new Vector3(-90, 0, 0)));
        ObjectPool.Instance.IntoPool(clone,1);
        _animator.SetBool("IsJumping", true);
        _rigid2D.velocity = new Vector2(dir.normalized.x * _baseJumpSpeed * power, _baseJumpSpeed * power);
        _canJump = false;
        _canDrop = true;
        _isOnGround = false;
        //隐藏指针.
        GameUI.Instance.HidePointer(_playerId);
        Debug.Log("Speed:" + _baseJumpSpeed);
        AudioManager.Instance.PlaySound(12);
    }

    /// <summary>
    /// 下降速度.
    /// </summary>
    [SerializeField]
    private float _dropSpeed = 15.0f;

    public  bool _canDrop = false;

    /// <summary>
    /// 下降.
    /// </summary>
    public void Drop()
    {
        //_animator.SetTrigger("Drop");
        _animator.SetBool("IsJumping", false);
        _rigid2D.velocity = new Vector2(0, -_dropSpeed);
        _rigid2D.AddForce(new Vector2(0, -1), ForceMode2D.Impulse);
        _canDrop = false;
        AudioManager.Instance.PlaySound(13);
    }

    //时间:10/5=2秒
    private float _maxJump = 10;
    private float _collectEnergySpeed = 5;

    public  bool _isOnGround = true;

    private void OnCollisionEnter2D(Collision2D collInfo)
    {

        if (collInfo.collider.tag == "Belt" && _rigid2D.velocity.y <= 0 && transform.position.y > collInfo.collider.transform.position.y)
        {
            if (_isOnGround)
            {
                return;
            }
            //落在了板子上
            Debug.Log("落在了板子上");
            //执行板子上的操作.
            collInfo.collider.GetComponent<BeltBase>().OnGround(this);
            //_animator.SetTrigger("Drop");
            _animator.SetBool("IsJumping", false);
            _rigid2D.velocity = Vector2.zero;
            //显示特效.
            Vector3 effectVector = 
                new Vector3(transform.position.x, collInfo.collider.transform.position.y + 0.13f, 10);
            GameObject clone = ObjectPool.Instance.GetPool(_dropEffect, effectVector, Quaternion.Euler(new Vector3(-90, 0, 0)));

            clone.transform.SetParent(collInfo.collider.transform);
            ObjectPool.Instance.IntoPool(clone,2.0f);
            //改变分数.
            SetFloor(collInfo.collider.GetComponent<BeltBase>().Id);
            //更新摄像机位置.
            _canJump = true;
            _isOnGround = true;
            //显示指针.
            //TODO:摄像机
            GameUI.Instance.ShowPointer(_playerId, Camera.main.WorldToScreenPoint(transform.position));
			//音效.
            AudioManager.Instance.PlaySound(11);

            //改尾巴颜色
            transform.gameObject.GetComponent<SpriteRenderer>().color = collInfo.gameObject.GetComponent<SpriteRenderer>().color;
            transform.Find("Trail").GetComponent<TrailRenderer>().startColor= collInfo.gameObject.GetComponent<SpriteRenderer>().color;

            //改指针颜色
            


        }

    }
    private void OnCollisionStay2D(Collision2D collInfo)
    {
        if (collInfo.collider.tag == "Belt" && _rigid2D.velocity.y == 0)
        {
            //待在板子上.
            _rigid2D.velocity = new Vector2(collInfo.collider.GetComponent<BeltBase>().MoveSpeed, 0);
            _canJump = true;
            _isOnGround = true;
            GameUI.Instance.ShowPointer(_playerId, Camera.main.WorldToScreenPoint(transform.position));
        }
    }

    public  bool _canJump = true;

    /// <summary>
    /// 设置层数.
    /// </summary>
    /// <param name="floor"></param>
    private void SetFloor(int floor)
    {
        if (floor > _currentFloor)
        {
            
            if (_playerId == 1)
            {
                Camera.main.GetComponent<CameraFollow>().UpdateCameraPos(transform.position);
            }
            else if(_playerId  == 2)
            {
                GameObject.Find("Camera2").GetComponent<CameraFollow>().UpdateCameraPos(transform.position);
            }
            _currentFloor = floor;
            Debug.Log("改变了层数:" + _currentFloor);
            GameUI.Instance.UpdatePlayerFloor(_playerId,_currentFloor);
            
        }

        if (_currentFloor >= GameManager.Instance.GetFloorNum(_playerId) - 5)
        {
            //生成新的层.
            Debug.Log("可以生成新的了");
            GameManager.Instance.CreateFloor(_playerId, GameManager.Instance.GetFloorNum(_playerId) + 1);
        }
    }

    private Action<PlayerCharacter> _releseProp;

    public void SetProp(Action<PlayerCharacter> releseProp)
    {
        _releseProp = releseProp;
    }

    /// <summary>
    /// 释放道具.
    /// </summary>
    public void ReleaseProp()
    {
        if (_releseProp == null)
        {
            return;
        }
        _releseProp(this);
        _releseProp = null;
        //更新UI信息为空.
        GameUI.Instance.ShowNullPropInfo(_playerId);
        Debug.Log("释放道具");
    }

    /// <summary>
    /// 判断是否有道具.
    /// </summary>
    /// <returns></returns>
    public bool HaveProp()
    {
        return _releseProp != null;
    }

    /// <summary>
    /// 角色死亡.
    /// </summary>
    public void Die()
    {
        AudioManager.Instance.PlaySound(14);
        GameManager.Instance.PlayerDie(_playerId);
        //TODO:对手胜利.
        if (_playerId == 1)
        {
            GameManager.Instance.Win(2);
        }
        else if (_playerId == 2)
        {
            GameManager.Instance.Win(1);
        }       

        transform.gameObject.SetActive(false); 
    }
}
