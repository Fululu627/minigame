using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BackGroundControler : MonoBehaviour {

    public GameObject bgBack;
    GameObject bgFront;
    Material bgBackMaterial;
    Material bgFrontMaterial;
    public Color beginColor; 
    public Color endColor; 
    public float dTime;
    public float flushTime;

    void Init()
    {
        bgBack = transform.Find("bgBack").gameObject;
        //bgFront = transform.Find("bgFront").gameObject;
        //Debug.Log(bgBack.name);
        bgBackMaterial = bgBack.GetComponent<SpriteRenderer>().material;
        //bgFrontMaterial = bgFront.GetComponent<SpriteRenderer>().material;
        //bgBackMaterial.EnableKeyword("_NORMALMAP");
        //beginColor = new Color(1.0,1.0,1.0,1.0);
        //endColor = new Color(0.0, 1.0, 1.0, 1.0);
}

	void Start () {
        Init();
        StartCoroutine(TimeContralFade());
	}
	
    IEnumerator TimeContralFade()
    {
        Color tempColor = beginColor;
        DOTween.To(() => tempColor, x =>tempColor = x, endColor, dTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCirc); ;
        //DOTween.To(() => tempColor, x => bgBackMaterial.SetColor("_Color1", new Color(x.r,x.g, x.b, x.a)), endColor, dTime);

        while (true)
        {
            bgBackMaterial.SetColor("_Color1", new Color(tempColor.r, tempColor.g, tempColor.b, tempColor.a));
            //Debug.Log(tempColor);
            yield return new WaitForSeconds(flushTime);
        }
    }
}
