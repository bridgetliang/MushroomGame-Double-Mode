using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//引入UI的命名空间
using UnityEngine.SceneManagement;

public class PlayerManager0 : MonoBehaviour {

    //属性值
    public int lifeValue0 = 3;   //给三条命
    public int playerScore0 = 0;   //得分
    public bool isDead0;      //玩家是否死亡，默认没有死亡

    //引用
    public GameObject born;
    

    //单例
    public static PlayerManager0 _instance;     //ctrl+R+E可以自动出现下面的东西
    public bool isDefeat0;
    public Text playerScoreText0;
    public Text playerLifeValueText0;


    public GameObject isDefeatUI;//获取结束界面的引用

    public static PlayerManager0 Instance
    {
        get
        {
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isDefeat0)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheStartScene",3);
                return;
        }
		if(isDead0)   //做监听
        {
            Recover();
           
        }

        playerLifeValueText0.text = lifeValue0.ToString();
        playerScoreText0.text = playerScore0.ToString();

      
    }

    //复活方法
    void Recover()
    {
        if (lifeValue0 <= 0)     //游戏结束
        {
            isDefeat0 = true;
            Invoke("ReturnToTheStartScene", 3);
            //游戏失败，返回主界面
        }
        else
        {
            lifeValue0--;
            GameObject go = GameObject.Instantiate(born,new Vector3(-2,-8,0),Quaternion.identity);
            go.GetComponent<born0>().isCreatePlayer = true;
            isDead0= false;
        }
    }

    //返回开始场景的方法
    void ReturnToTheStartScene()
    {
        SceneManager.LoadScene(0);
    }
}
