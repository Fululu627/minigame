using System.Collections;
using System.Collections.Generic;
using QxFramework.Utilities;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏界面UI.
/// </summary>
public class GameUI : MonoSingleton<GameUI>
{
    private void Awake()
    {
        InitWidget();
    }

    private void Update()
    {

    }

    //指针.
    private GameObject _pointer1;
    private GameObject _pointer2;

    private Text _floorText1;
    private Text _floorText2;

    private Image _propIcon1;
    private Image _propIcon2;
    public Sprite NullIcon;

    private Text _propTitle1;
    private Text _propTitle2;

    private Text _propIntro1;
    private Text _propIntro2;

    private void InitWidget()
    {
        _pointer1 = transform.Find("Pointer1").gameObject;
        _floorText1 = transform.Find("FloorText1").GetComponent<Text>();
        _pointer2 = transform.Find("Pointer2").gameObject;
        _floorText2 = transform.Find("FloorText2").GetComponent<Text>();
        _propIcon1 = transform.Find("Player1Tool/Image").GetComponent<Image>();
        _propIcon2 = transform.Find("Player2Tool/Image").GetComponent<Image>();
        _propTitle1 = transform.Find("IconTitle1").GetComponent<Text>();
        _propTitle2 = transform.Find("IconTitle2").GetComponent<Text>();
        _propIntro1 = transform.Find("PropIntro1").GetComponent<Text>();
        _propIntro2 = transform.Find("PropIntro2").GetComponent<Text>();
    }

    /// <summary>
    /// 显示指针.
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="pos">位置</param>
    public void ShowPointer(int id, Vector2 pos)
    {
        GameObject pointer = null;
        switch (id)
        {
            case 1:
                pointer = _pointer1;
                break;
            case 2:
                pointer = _pointer2;
                break;
        }

        if (pointer == null) return;
        pointer.SetActive(true);
        pointer.transform.position = pos;
    }

    public void HidePointer(int id)
    {
        GameObject pointer = null;
        switch (id)
        {
            case 1:
                pointer = _pointer1;
                break;
            case 2:
                pointer = _pointer2;
                break;
        }

        if (pointer == null) return;
        pointer.SetActive(false);
    }

    /// <summary>
    /// 更新指针的旋转.
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="angle">角度</param>
    public void UpdatePointerRot(int id, float angle)
    {
        GameObject pointer = null;
        switch (id)
        {
            case 1:
                pointer = _pointer1;
                break;
            case 2:
                pointer = _pointer2;
                break;
        }

        if (pointer == null) return;
        pointer.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    /// <summary>
    /// 更新指针位置.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pos">UI坐标系</param>
    public void UpdatePointerPos(int id, Vector2 pos)
    {
        GameObject pointer = null;
        switch (id)
        {
            case 1:
                pointer = _pointer1;
                break;
            case 2:
                pointer = _pointer2;
                break;
        }

        if (pointer == null) return;
        pointer.transform.position = pos;
    }

    /// <summary>
    /// 更新玩家层数.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="floorNum">层数</param>
    public void UpdatePlayerFloor(int id, int floorNum)
    {
        Text floorText = null;
        switch (id)
        {
            case 1:
                floorText = _floorText1;
                break;
            case 2:
                floorText = _floorText2;
                break;
        }

        if (floorText == null) return;
        floorText.text = floorNum.ToString();
    }

    /// <summary>
    /// 显示道具信息.
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="propId"></param>
    public void ShowPropInfo(int playerId, int propId)
    {
        string iconName = TableAgent.Instance.GetString("Prop", propId.ToString(), "IconName");
        Sprite iconSprite = Resources.Load<Sprite>("Prop/Icon/" + iconName);
        string propTitle = TableAgent.Instance.GetString("Prop", propId.ToString(), "Name");
        string propIntro = TableAgent.Instance.GetString("Prop", propId.ToString(), "Description");
        if (playerId == 1)
        {
            _propIcon1.sprite = iconSprite;
            _propTitle1.text = propTitle;
            _propIntro1.text = propIntro;
        }
        else if (playerId == 2)
        {
            _propIcon2.sprite = iconSprite;
            _propTitle2.text = propTitle;
            _propIntro2.text = propIntro;
        }
    }

    public void ShowNullPropInfo(int playerId)
    {
        if (playerId == 1)
        {
            _propIcon1.sprite = NullIcon;
            _propTitle1.text = "";
            _propIntro1.text = "";
        }
        else if (playerId == 2)
        {
            _propIcon2.sprite = NullIcon;
            _propTitle2.text = "";
            _propIntro2.text = "";
        }
    }
}
