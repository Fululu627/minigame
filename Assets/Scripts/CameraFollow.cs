using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _followTargetTransform;

    /// <summary>
    /// 摄像机移动速度.
    /// </summary>
    [SerializeField]
    private float _moveSpeed = 1.0f;

    private void Update()
    {
        if (!GameManager.Instance.IsGaming)
        {
            return;
        }
        //摄像机往上移.
        transform.position += Vector3.up * _moveSpeed * Time.deltaTime;
    }

    private float _cameraMoveTime = 0.8f;
    /// <summary>
    /// 移动到新层时下降距离.
    /// </summary>
    private float _moveOffset = 2.8f;
    public void UpdateCameraPos(Vector2 pos)
    {
        transform.DOMove(new Vector3(transform.position.x, pos.y+_moveOffset, -10), _cameraMoveTime);
    }
}
