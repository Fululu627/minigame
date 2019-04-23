using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class IceRayProp
{
    public static void Release(PlayerCharacter player)
    {
        GameObject prefab = Resources.Load<GameObject>("LineRay");
        
        if (player.PlayerId == 1)
        {
            GameObject clone = ObjectPool.Instance.GetPool(prefab,
                new Vector3(5.1f, GameObject.Find("Camera2").transform.position.y + 8.5f), Quaternion.identity);
            clone.transform.SetParent(GameObject.Find("Camera2").transform);
            //Random.Range(5.1f, 12.9f)
            //从左到右发射射线.
            var hit = Physics2D.Raycast(clone.transform.position, Vector2.down);
            clone.GetComponent<LineRenderer>().SetPosition(0, clone.transform.position);
            clone.GetComponent<LineRenderer>().SetPosition(1, hit.point);
        }
        else if (player.PlayerId == 2)
        {
            
        }
    }
}
