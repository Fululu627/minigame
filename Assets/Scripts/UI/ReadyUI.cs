using System.Collections;
using System.Collections.Generic;
using QxFramework.Utilities;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReadyUI : MonoSingleton<ReadyUI>
{
    //等待加入时显示的图片.
    private Image _waitToJoinImage1;
    private Image _waitToJoinImage2;

    //加入后的图片.
    private Image _joinedImage1;
    private Image _joinedImage2;

    //倒计时
    private Text _text1;
    private Text _text2;

    private void InitWidget()
    {
        _waitToJoinImage1 = transform.Find("JoinImage/Player1").GetComponent<Image>();
        _waitToJoinImage2 = transform.Find("JoinImage/Player2").GetComponent<Image>();
        _joinedImage1 = transform.Find("ReadyImage/Player1").GetComponent<Image>();
        _joinedImage2 = transform.Find("ReadyImage/Player2").GetComponent<Image>();
        _text1 = transform.Find("Counter/Player1").GetComponent<Text>();
        _text2 = transform.Find("Counter/Player2").GetComponent<Text>();
    }

    private void Awake()
    {
        InitWidget();
    }

    public void ShowJoinedImage(int id)
    {
        if (id == 1)
        {
            _waitToJoinImage1.rectTransform.DOScaleX(0, 0.5f).SetEase(Ease.InBack); 
            //_waitToJoinImage1.gameObject.SetActive(false);
            _joinedImage1.gameObject.SetActive(true);
            _joinedImage1.rectTransform.DOScaleY(0.5f, 1.0f).SetEase(Ease.OutBack);
        }
        else if (id == 2)
        {
            _waitToJoinImage2.rectTransform.DOScaleX(0, 0.5f).SetEase(Ease.InBack);
            //_waitToJoinImage2.gameObject.SetActive(false);
            _joinedImage2.gameObject.SetActive(true);
            _joinedImage2.rectTransform.DOScaleY(0.5f, 1.0f).SetEase(Ease.OutBack);
        }
    }

    public void HidePanel()
    {
        StartCoroutine(WaitToHidePanel());
        
    }

    IEnumerator WaitToHidePanel()
    {
        for (int i = 0; i < 3; i++)
        {           
            _text1.text = (3-i).ToString();
            _text2.text = (3 - i).ToString();
            yield return new WaitForSeconds(1.0f);
        }
        gameObject.SetActive(false);
    }
}
