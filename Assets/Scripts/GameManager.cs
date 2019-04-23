using System.Collections;
using System.Collections.Generic;
using QxFramework.Utilities;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private bool _isGaming = false;
    public bool IsGaming
    {
        get { return _isGaming; }
    }

    private PlayerCharacter _player1;
    private PlayerCharacter _player2;

    public PlayerCharacter Player1
    {
        get { return _player1; }
    }

    public PlayerCharacter Player2
    {
        get { return _player2; }
    }

    private void Awake()
    {
        InitWidget();
        AudioManager.Instance.PlayMusic(1);
        Data.Instance.LoadTable();
        //Init();
    }

    private void InitWidget()
    {
        _player1 = GameObject.Find("Player1").GetComponent<PlayerCharacter>();
        _player2 = GameObject.Find("Player2").GetComponent<PlayerCharacter>();
        _winEffect = Resources.Load<GameObject>("Effects/ConfettiBlast");
        _emojiEffect = Resources.Load<GameObject>("Effects/EmojiCool");
    }

    private void Init()
    {
        FloorList1.Clear();
        FloorList2.Clear();
        FloorList1.Add(0);
        FloorList2.Add(0);
        CreateFloor(1, 1);
        CreateFloor(2, 1);
        //TODO:删掉Test
        //_player1.SetProp(IceRayProp.Release);
    }

    /// <summary>
    /// 开始游戏.
    /// </summary>
    private void StartGame()
    {
        _isGaming = true;
        Init();
        GameUI.Instance.ShowPointer(1, Camera.main.WorldToScreenPoint(transform.position));
        GameUI.Instance.ShowPointer(2, Camera.main.WorldToScreenPoint(transform.position));
        //ReadyUI.Instance.HidePanel();
    }

    private bool _playerReady1 = false;
    private bool _playerReady2 = false;

    private bool _isStart = false;

    /// <summary>
    /// 玩家准备.
    /// </summary>
    /// <param name="playerId"></param>
    public void GetReady(int playerId)
    {
        if (playerId == 1 && !_playerReady1)
        {
            _playerReady1 = true;
        }
        else if (playerId == 2 && !_playerReady2)
        {
            _playerReady2 = true;
        }

        if (_playerReady1 && _playerReady2 && !_isStart)
        {
            //倒计时.
            ReadyUI.Instance.HidePanel();
            StartCoroutine(WaitToStart());
            _isStart = true;
        }
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(3.0f);
        StartGame();
    }

    /// <summary>
    /// 场上存在的层数.
    /// </summary>
    private int _floorNum1 = 0;
    private int _floorNum2 = 0;

    public List<float> FloorList1 = new List<float>();
    public List<float> FloorList2 = new List<float>();

    /// <summary>
    /// 板子间距.
    /// </summary>
    private float _floorGap = 5;

    public float FloorGap
    {
        get { return _floorGap; }
    }
    

    //每次生成的数量.
    private int _createPerTime = 10;

    /// <summary>
    /// 生成10块板.
    /// </summary>
    /// <param name="id">需生成的第一块的ID</param>
    public void CreateFloor(int playerId, int floorId)
    {
        int createId = floorId;
        if (playerId == 1)
        {
            for (int i = 0; i < _createPerTime; i++)
            {
                //TODO:改变生成的板子类型.
                int pickNum = Random.Range(0, 100);
                //Debug.Log(pickNum + GetBelt(pickNum));
                int propNum = Random.Range(0, 100);
                GameObject prefab = Resources.Load<GameObject>(GetBelt(pickNum));
                GameObject clone = ObjectPool.Instance.GetPool(prefab, new Vector3(Random.Range(-7.35f, -1.0f), (createId - 1) * _floorGap, 0), Quaternion.identity);
                //clone.transform.position = new Vector3(Random.Range(-6.0f, -3.0f), clone.transform.position.y, clone.transform.position.z);
                clone.GetComponent<BeltBase>().Id = createId;
                clone.GetComponent<BeltBase>().playerId = playerId;
                //TODO:改概率.
                //是否显示道具.
                if (propNum < 42)
                {
                    clone.transform.Find("Prop").gameObject.SetActive(true);
                }
                FloorList1.Add(clone.transform.position.y);
                createId++;
            }
            _floorNum1 += _createPerTime;
        }
        if (playerId == 2)
        {
            for (int i = 0; i < _createPerTime; i++)
            {
                //TODO:改变生成的板子类型.
                int pickNum = Random.Range(0, 100);
                int propNum = Random.Range(0, 100);
                //Debug.Log(pickNum + GetBelt(pickNum));
                GameObject prefab = Resources.Load<GameObject>(GetBelt(pickNum));
                GameObject clone = ObjectPool.Instance.GetPool(prefab, new Vector3(Random.Range(5.6f, 12.12f), (createId - 1) * _floorGap, 0), Quaternion.identity);
                //clone.transform.position = new Vector3(Random.Range(-6.0f, -3.0f), clone.transform.position.y, clone.transform.position.z);
                clone.GetComponent<BeltBase>().Id = createId;
                clone.GetComponent<BeltBase>().playerId = playerId;
                if (propNum < 42)
                {
                    clone.transform.Find("Prop").gameObject.SetActive(true);
                }
                FloorList2.Add(clone.transform.position.y);
                createId++;
                
            }
            _floorNum2 += _createPerTime;
        }
    }

    /// <summary>
    /// 获取一个带
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private string GetBelt(int num)
    {
        int lowLimit = 0;
        string name = "CommanBelt";
        for (int i = 0; i < 6; i++)
        {
            if (num >= lowLimit && num < TableAgent.Instance.GetInt("BeltGenerate", i.ToString(), "Probability"))
            {
                name = TableAgent.Instance.GetString("BeltGenerate", i.ToString(), "PrefabName");
                return name;
            }

            lowLimit = TableAgent.Instance.GetInt("BeltGenerate", i.ToString(), "Probability");
        }

        return name;
    }
    

    public int GetFloorNum(int id)
    {
        if (id == 1)
        {
            return _floorNum1;
        }
        else if (id == 2)
        {
            return _floorNum2;
        }
        return 0;
    }

    public void DeCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    private int _playerScore1;
    private int _playerScore2;

    public void PlayerDie(int id)
    {
        if (id == 1)
        {
            _playerScore2++;
        }
        else if (id == 2)
        {
            _playerScore1++;
        }
    }

    public void Restart()
    {
        Init();
    }

    private GameObject _winEffect;
    private GameObject _emojiEffect;

    private bool _gameFinished = false;

    /// <summary>
    /// 玩家胜利.
    /// </summary>
    /// <param name="playerId"></param>
    public void Win(int playerId)
    {
        if (_gameFinished)
        {
            return;
        }
        Vector2 pos = new Vector2();
        Vector2 emojiPos = new Vector2();
        if (playerId == 1)
        {
            pos = Camera.main.transform.position;
            emojiPos = Player1.transform.position;
        }
        else if (playerId == 2)
        {
            pos = GameObject.Find("Camera2").transform.position;
            emojiPos = Player2.transform.position;
        }
        ObjectPool.Instance.GetPool(_winEffect, pos, Quaternion.Euler(-90, 0, 0));
        ObjectPool.Instance.GetPool(_emojiEffect, emojiPos, Quaternion.Euler(-90, 0, 0));
        _gameFinished = true;
        AudioManager.Instance.PlaySound(16);
    }
}
