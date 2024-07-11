using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    public bool cheat = false;
    private PlayerStatusData statusData;

    void Awake()
    {
        statusData = Resources.Load<PlayerStatusData>("PlayerStatusData");
    }

    void Start()
    {
        if(cheat){
            statusData.ATK = 5240;
            statusData.DEF = 600;
            statusData.SPD = 550;
            statusData.CRITRATE = 500;
            statusData.CRITDMG = 1200;
        }
    }
}
