using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player0 : MonoBehaviour {

    // Use this for initialization
    //属性值
    public float moveSpeed = 3f;//移动速度

    //引用
    //----坦克---
    private SpriteRenderer spriteRenderer;//持有精灵渲染器的引用
    public Sprite[] tankSprite;//精灵数组   0上 1右 2下 3左

    //----子弹----
    public GameObject bullet;//获取子弹的引用
    private Vector3 bulletEulerAugles;//获取子弹旋转角度的变量
    public float timeVal;//子弹发射的时间间隔

    public GameObject explosion;//爆炸特效
    public float defendTimeval = 3f;//无敌计时器，3s无敌

    public bool isDefended = true;//无敌状态
    public GameObject defendEffect;//无敌特效
	void Start () {
        spriteRenderer = this.GetComponent<SpriteRenderer>();//获取组件  //this指的是这个当前的文件绑定的对象

    }
	
	// Update is called once per frame
    void Update()              //一秒钟调用30次，每帧。但是每秒钟里面不确定，
    {
        if (isDefended)      //保护是否处于无敌状态
        {
            defendEffect.SetActive(true);
            defendTimeval -= Time.deltaTime;
            if (defendTimeval <= 0)
            {
                isDefended = false;
                defendEffect.SetActive(false);
            }
        }
    }
    void FixedUpdate()    //属于U3D的内嵌函数,此函数不宜放太多
    {
        if(PlayerManager0._instance.isDefeat0)  //如果被打败
        {
            return;
        }

        Move();

        //攻击CD
        if (timeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }

    //坦克的移动函数
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");//水平轴，取值范围x方向（-1，1）
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime,Space.World);   //乘以h就可以左右两边都行  重载函数，有六个参数设置

        if (h < 0)   //向左移动的图片
        {
            spriteRenderer.sprite = tankSprite[3];
            bulletEulerAugles = new Vector3(0, 0, 90);  //作用：在四个if里面写是根据坦克方向来变化了子弹的方向，动态跟随变化
        }
        else if (h > 0)
        {
            spriteRenderer.sprite = tankSprite[1];
            bulletEulerAugles = new Vector3(0, 0, -90);
        }

        if (h != 0)     //等于0就执行下面，不等于0就跳出去
        {
            return;
        }

        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime,Space.World);
        if (v < 0)
        {
            spriteRenderer.sprite = tankSprite[2];  //向下移动的图片
            bulletEulerAugles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            spriteRenderer.sprite = tankSprite[0];//向上移动的图片
            bulletEulerAugles = new Vector3(0, 0, 0);
        }
    }

    //坦克的攻击方法
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))  //GatKeyDown(获取某个键的按下)
        {

            GameObject.Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAugles));//调用欧拉旋转函数
            timeVal = 0;    //timeVal = 0;//加在这里是因为坦克攻击停止了那么时间开始重新读秒
        }
    }

    //坦克的死亡方法
    void Die()
    {
        if (isDefended)
        {
            return;
        }

        PlayerManager0._instance.isDead0 = true;//利用单利模式进行数据访问

        //产生爆炸特效
        GameObject.Instantiate(explosion, transform.position, transform.rotation);
        //死亡,销毁自身
        Destroy(this.gameObject);
    }
}
