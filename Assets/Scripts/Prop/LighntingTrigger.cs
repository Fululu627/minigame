using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// 暂时不用了
/// </summary>
public class LighntingTrigger : MonoBehaviour {

    private Transform camera_1;
    private Transform camera_2;

    
	// Use this for initialization
	void Start () {
        StartCoroutine(GoDisable());
	}
	
	// Update is called once per frame
	void Update () {
	
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<PlayerCharacter>().PlayerId == 1)
            {
                PlayerController.Instance.canMove1 = false;
                var clone = ObjectPool.Instance.GetPool(PropManager.Instance.CircleStar, collision.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                
                StartCoroutine(GoBack(1));
            }
            if (collision.GetComponent<PlayerCharacter>().PlayerId == 2)
            {
                PlayerController.Instance.canMove2 = false;
                var clone = ObjectPool.Instance.GetPool(PropManager.Instance.CircleStar, collision.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                StartCoroutine(GoBack(2));
            }
        }
    }

    private IEnumerator GoBack(int i)
    {
        yield return new WaitForSeconds(1);
        if (i == 1)
        {
            PlayerController.Instance.canMove1 = true;
        }
        if(i == 2)
        {
            PlayerController.Instance.canMove2 = true;
        }
    }

    private IEnumerator GoDisable()
    {
        yield return new WaitForSeconds(1f);
        transform.gameObject.SetActive(false);
    }
}
