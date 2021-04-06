using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//引入UI的命名空间
using UnityEngine.SceneManagement;

public class PlayerManager1 : MonoBehaviour
{

    //属性值
    public int lifeValue1 = 3;   //给三条命
    public int playerScore1 = 0;   //得分
    public bool isDead1;      //玩家是否死亡，默认没有死亡

    //引用
    public GameObject born;


    //单例
    public static PlayerManager1 _instance;     //ctrl+R+E可以自动出现下面的东西
    public bool isDefeat1;
    public Text playerScoreText1;
    public Text playerLifeValueText1;


    public GameObject isDefeatUI;//获取结束界面的引用

    public static PlayerManager1 Instance
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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat1)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheStartScene", 3);
            return;
        }
        if (isDead1)   //做监听
        {
            Recover();

        }

        playerLifeValueText1.text = lifeValue1.ToString();
        playerScoreText1.text = playerScore1.ToString();


    }

    //复活方法
    void Recover()
    {
        if (lifeValue1 <= 0)     //游戏结束
        {
            isDefeat1 = true;
            Invoke("ReturnToTheStartScene", 3);
            //游戏失败，返回主界面
        }
        else
        {
            lifeValue1--;
            GameObject go = GameObject.Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<born1>().isCreatePlayer1 = true;
            isDead1 = false;
        }
    }

    //返回开始场景的方法
    void ReturnToTheStartScene()
    {
        SceneManager.LoadScene(0);
    }
}

