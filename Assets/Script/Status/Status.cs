using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ステータス管理
public class Status:MonoBehaviour
{
    public static int HP = 100;
    public static int ATK = 100;
    public static int DEF = 0;
    public static int SPD = 100;
    public static int CRITRATE = 50;
    public static int CRITDMG = 50;

    public static int nowHP = 100;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            LowHP();
        }
    }

    void LowHP()
    {
        nowHP -= 10;
        if(nowHP <= 0)
        {
            nowHP = HP;
        }
    }


}
