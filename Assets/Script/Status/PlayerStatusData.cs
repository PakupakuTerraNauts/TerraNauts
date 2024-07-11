using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PlayerStatusData", menuName = "ScriptableOjbects/CreatePlayerStatusData")]
public class PlayerStatusData : ScriptableObject
{
    public int HP = 100;
    public int nowHP = 100;
    public int HPincrement = 0;

    public int ATK = 100;
    public int ATKincrement = 0;
    public int DEF = 0;
    public int DEFincrement = 0;
    public int SPD = 100;
    public int SPDincrement = 0;
    public int CRITRATE = 50;
    public int CRITRATEincrement = 0;
    public int CRITDMG = 50;
    public int CRITDMGincrement = 0;
}

