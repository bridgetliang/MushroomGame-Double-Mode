using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class born1 : MonoBehaviour
{

    // Use this for initialization
    public GameObject player1;//玩家坦克的引用
    public bool isCreatePlayer1;//是否产生玩家，默认产生敌人
    public GameObject[] enemyPrefabList;//两种敌人

    void Start()
    {
        if (PlayerManager._instance.isDefeat)  //如果被打败
        {
            return;
        }
        Invoke("BornTank", 1f);//延迟调用的函数，程序休眠一段时间然后再执行
        Destroy(this.gameObject, 1f);//延时销毁出生特效
    }

    // Update is called once per frame
    void Update()
    {

    }

    //坦克的生成
    void BornTank()
    {
        if (isCreatePlayer1)   //获取坦克的生成条件
        {

            GameObject.Instantiate(player1, transform.position, Quaternion.identity);

        }
        else
        {
            int num = Random.Range(0, 2);
            GameObject.Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }
    }
}
