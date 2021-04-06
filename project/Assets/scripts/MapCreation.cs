using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {

	// Use this for initialization
    //0.老家  1.墙  2.障碍  3.出生效果  4.河流    5.草  6.空气墙  7.出生效果1
    public GameObject[] item;
    private List<Vector3> itemPositionList = new List<Vector3>();//已经有东西的位置列表
	void Awake() {
        InitMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void InitMap()
    {
        //创建老家
        CreateItem(item[0], new Vector3(0, -8, -0), Quaternion.identity);
        //构建木墙
        CreateItem(item[1], new Vector3(-1, -8, -0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, -0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        //最下面一行
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        //最左边一行
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        //最右边一行
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }
          //初始化玩家
        GameObject go = GameObject.Instantiate(item[3], new Vector3(4, -8, 0), Quaternion.identity);
        go.GetComponent<born>().isCreatePlayer = true;
        GameObject go1 = GameObject.Instantiate(item[7], new Vector3(-2, -8, 0), Quaternion.identity);
        go1.GetComponent<born1>().isCreatePlayer1 = true;

        //产生敌人
        CreateItem(item[3], new Vector3(-10,8,0),Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 4, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);

        //第一次调用CreateEnemy用4s，每隔5s开调用
        InvokeRepeating("CreateEnemy",4,5);

        //实例化地图
        for (int i=0;i<60;i++)
        {
            CreateItem(item[1], CreateRamdomPosition(), Quaternion.identity);
        }
        for(int i=0;i<20;i++)
        {
            CreateItem(item[2], CreateRamdomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[4], CreateRamdomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[5], CreateRamdomPosition(), Quaternion.identity);
        }
    }

    //构建物品
    void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotate)
    {
        GameObject go=GameObject.Instantiate(createGameObject, createPosition, createRotate);
        go.transform.SetParent(this.gameObject.transform);
        itemPositionList.Add(createPosition);
    }

    //构建外围空气墙
    //最上面一行
     private Vector3 CreateRamdomPosition()
    {
        //不生成x=-10，10的两列，y=-8和8这两行的位置
        while(true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8),0);
            if(HasThePosition(createPosition))
            {
                return createPosition;
            }
          
        }
    }

    //用来判断位置列表中是否有这个位置
    private bool HasThePosition(Vector3 createPos)
    {
        for(int i=0;i<itemPositionList.Count;i++)
        {
            if(createPos==itemPositionList[i])
            {
                return false;
            }
        }
        return true;
    }

    //产生敌人的方法
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if(num==0)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }else if(num==1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else
        {
            EnemyPos = new Vector3(10, 8, 0);
        }

        CreateItem(item[3],EnemyPos,Quaternion.identity);
    }

}

