using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class bullet0 : MonoBehaviour
{
    // Use this for initialization
    public float bulletspeed = 10;

    public bool isPlayerBullet0;    //用来判断玩家和敌方子弹 //默认是敌人的子弹
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * bulletspeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D col)   //Stay Enter Exit 时间要求精度很高   触发检测函数
    {
        switch(col.tag)
        {
            case "tank":
                if (!isPlayerBullet0)
                {
                    col.SendMessage("Die");
                    Destroy(this.gameObject);
                }
                break;
            case "home":  
                col.SendMessage("Die");
                Destroy(this.gameObject);
                break;
            case "enemy":
                if (isPlayerBullet0)
                {
                    col.SendMessage("Die");
                    PlayerManager0._instance.playerScore0++;
                    Destroy(this.gameObject);
                }
                break;
            case "wall":  
                Destroy(col.gameObject);//销毁木墙
                Destroy(this.gameObject);//销毁子弹
                break;
            case "barrier":
                Destroy(this.gameObject);
                col.SendMessage("PlayerAudio",SendMessageOptions.DontRequireReceiver);
                break;
            default:
                break;
        }
    }
}
