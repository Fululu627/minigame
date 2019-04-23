using System.Collections;
using System.Collections.Generic;
using QxFramework.Utilities;
using UnityEngine;

/// <summary>
/// 道具、特效管理器.
/// </summary>
public class PropManager : MonoSingleton<PropManager>
{
    private GameObject _fogPrefab;

    private GameObject _firePrefab;

    private GameObject _magicBuffBluePrefab;

    private GameObject _lighnting1;

    private GameObject _lighnting2;

    private GameObject _circleStar;

    private GameObject _gravity;

    private GameObject _slowTime;

    public GameObject SlowTime
    {
        get { return _slowTime; }
    }

    public GameObject Gravity
    {
        get { return _gravity; }
    }

    public GameObject CircleStar
    {
        get { return _circleStar; }
    }
    public GameObject FogPrefab
    {
        get { return _fogPrefab; }
    }

    public GameObject FirePrefab
    {
        get { return _firePrefab; }
    }

    private GameObject _eatPropPrefab;

    public GameObject EatPropPrefab
    {
        get { return _eatPropPrefab; }
    }

    public GameObject Lighnting1
    {
        get { return _lighnting1; }
    }

    public GameObject Lighnting2
    {
        get { return _lighnting2; }
    }

    public GameObject MagicBuffBluePrefab
    {
        get { return _magicBuffBluePrefab; }
    }

    private void InitWidget()
    {
        _fogPrefab = Resources.Load<GameObject>("Prop/Fog");
        _firePrefab = Resources.Load<GameObject>("Effects/Fire");
        _eatPropPrefab = Resources.Load<GameObject>("Effects/RoundHitBlue");
        _magicBuffBluePrefab = Resources.Load<GameObject>("Effects/MagicBuffBlue");
        _lighnting1 = Resources.Load<GameObject>("Effects/LightningOrb");
        _lighnting2 = Resources.Load<GameObject>("Effects/LightningStrikeTall");
        _circleStar = Resources.Load<GameObject>("Effects/StunnedCirclingStars");
        _gravity = Resources.Load<GameObject>("Effects/Gravity");
        _slowTime = Resources.Load<GameObject>("Effects/MagicChargeBlue");
    }

    private void Awake()
    {
        InitWidget();
    }
}
