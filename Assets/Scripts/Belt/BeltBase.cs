using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltBase : MonoBehaviour {


    public float MoveSpeed = 3;

  public float speedBase = 3;   

    private GameObject wall_1;
    private GameObject wall_2;

    /// <summary>
    /// 层数
    /// </summary>
    public int Id;

    /// <summary>
    /// 玩家
    /// </summary>
    public int playerId;

    private float rightPoint1 = -1f;
    private float leftPoint1 = -7.35f;

    private float rightPoint2 = 12.12f;
    private float leftPoint2 = 5.6f;

    private void Awake()
    {
        wall_1 = GameObject.Find("Wall1");
        wall_2 = GameObject.Find("Wall2");
        ChooseDirection();
    }


    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        OnMove(playerId);
	}

    public virtual void OnMove(int playerId)
    {
        if (playerId == 1)
        {
            transform.Translate(new Vector3(1, 0, 0) * MoveSpeed * Time.deltaTime);
            if (transform.position.x > rightPoint1)
            {
                MoveSpeed = -speedBase;
            }
            if (transform.position.x < leftPoint1)
            {
                MoveSpeed = speedBase;
            }
        }
        if (playerId == 2)
        {
            transform.Translate(new Vector3(1, 0, 0) * MoveSpeed * Time.deltaTime);
            if (transform.position.x > rightPoint2)
            {
                MoveSpeed = -speedBase;
            }
            if (transform.position.x < leftPoint2)
            {
                MoveSpeed = speedBase;
            }
        }
    }



    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public virtual  void OnCollisionStay2D(Collision2D collision)
    {

    }

    private void ChooseDirection()
    {
        var random = Random.Range(0, 100);
        if (random < 50)
        {
            MoveSpeed *= - 1;
        }


    }

    /// <summary>
    /// 玩家落到板子上时执行.
    /// </summary>
    public virtual void OnGround(PlayerCharacter player)
    {

    }

    private void OnDisable()
    {
        transform.Find("Prop").gameObject.SetActive(false);
    }
    
}
