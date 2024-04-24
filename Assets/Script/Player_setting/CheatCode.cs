using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    public bool cheat = false;
    void Start()
    {
        if(cheat){
            Player.ATK = 124;
            Player.DEF = 12;
            Player.SPD = 139;
            Player.CRITRATE = 200;
            Player.CRITDMG = 71;
        }
    }
}
