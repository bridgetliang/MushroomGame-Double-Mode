using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy0 : MonoBehaviour
{  //复制代码的时候，记得把类名改了

    // Use this for initialization
    public float moveSpeed = 3f;//移动速度

    private SpriteRenderer spriteRenderer;//持有精灵渲染器的引用
    public Sprite[] tankSprite;//精灵数组   0上 1右 2下 3左

    public GameObject bullet;//持有子弹的引用
    private Vector3 bulletEulerAugles;//子弹旋转角度
    public float timeVal;//子弹发射的时间间隔
    public GameObject explosion;//爆炸特效

    public float timeValChangeDirection;//用来改变方向的计时器
    private float v;
    private float h;

    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();//获取组件

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (PlayerManager._instance.isDefeat)  //如果被打败
        {
            return;
        }

        Move();

        //攻击的时间间隔
        if (timeVal >= 3f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }

    //坦克的移动方法
    void Move()
    {
        if (timeValChangeDirection >= 4)    //控制转向
        {
            int num = Random.Range(0, 8);   //为了控制朝着老家方向概率大
            if (num >= 5)//向下走
            {
                h = 0;
                v = -1;
            }
            else if (num == 0)//向上走
            {
                h = 0;
                v = 1;
            }
            else if (num > 0 && num <= 2)//向左走
            {
                h = -1;
                v = 0;
            }
            else if (num > 2 && num <= 4)//向右走
            {
                h = 1;
                v = 0;
            }
            timeValChangeDirection = 0;//转向后计时器归零
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            spriteRenderer.sprite = tankSprite[3];
            bulletEulerAugles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            spriteRenderer.sprite = tankSprite[1];
            bulletEulerAugles = new Vector3(0, 0, -90);
        }
        if (h != 0)
        {
            return;
        }
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime);
        if (v < 0)
        {
            spriteRenderer.sprite = tankSprite[2];
            bulletEulerAugles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            spriteRenderer.sprite = tankSprite[0];
            bulletEulerAugles = new Vector3(0, 0, 0);
        }
    }

    //攻击函数
    void Attack()
    {
        GameObject.Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAugles));
        timeVal = 0;
    }

    //死亡函数
    void Die()
    {
        GameObject.Instantiate(explosion, transform.position, transform.rotation);//死亡，摧毁自身
        Destroy(this.gameObject);
    }


    void OnCollisionEnter2D(Collision2D col)    //优化防止相撞推着走
    {
        if (col.gameObject.tag == "Enemy")
        {
            timeValChangeDirection = 4;
        }
    }
}
