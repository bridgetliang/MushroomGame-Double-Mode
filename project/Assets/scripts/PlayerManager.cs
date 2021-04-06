using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//引入UI的命名空间
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    //属性值
    public int lifeValue = 3;   //给三条命
    public int playerScore = 0;   //得分
    public bool isDead;      //玩家是否死亡，默认没有死亡

    //引用
    public GameObject born;
    

    //单例
    public static PlayerManager _instance;     //ctrl+R+E可以自动出现下面的东西
    public bool isDefeat;
    public Text playerScoreText;
    public Text playerLifeValueText;


    public GameObject isDefeatUI;//获取结束界面的引用

    public static PlayerManager Instance
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
        if(isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheStartScene",3);
                return;
        }
		if(isDead)   //做监听
        {
            Recover();
           
        }

        playerLifeValueText.text = lifeValue.ToString();
        playerScoreText.text = playerScore.ToString();

      
    }

    //复活方法
    void Recover()
    {
        if (lifeValue <= 0)     //游戏结束
        {
            isDefeat = true;
            Invoke("ReturnToTheStartScene", 3);
            //游戏失败，返回主界面
        }
        else
        {
            lifeValue--;
            GameObject go = GameObject.Instantiate(born,new Vector3(4,-8,0),Quaternion.identity);
            go.GetComponent<born>().isCreatePlayer = true;
            isDead = false;
        }
    }

    //返回开始场景的方法
    void ReturnToTheStartScene()
    {
        SceneManager.LoadScene(0);
    }
}
