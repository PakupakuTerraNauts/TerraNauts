using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusChange : MonoBehaviour
{
    private PlayerStatusData statusData;

    void Awake()
    {
        statusData = Resources.Load<PlayerStatusData>("PlayerStatusData");
    }

/// <summary>
/// ステータス上昇分をセーブ
/// </summary>
    public void PlayerStatusSave(){
        statusData.HPincrement = 0;
        statusData.ATKincrement = 0;
        statusData.DEFincrement = 0;
        statusData.SPDincrement = 0;
        statusData.CRITRATEincrement = 0;
        statusData.CRITDMGincrement = 0;
    }

/// <summary>
/// セーブしていないステータスをリセット
/// </summary>
    public void PlayerStatusReset(){
        statusData.HP -= statusData.HPincrement;
        statusData.nowHP = statusData.HP;
        statusData.ATK -= statusData.ATKincrement;
        statusData.DEF -= statusData.DEFincrement;
        statusData.SPD -= statusData.SPDincrement;
        statusData.CRITRATE -= statusData.CRITRATEincrement;
        statusData.CRITDMG -= statusData.CRITDMGincrement;
        
        PlayerStatusSave();
    }

/// <summary>
/// ステータスを初期化
/// </summary>
    public void InitializePlayerStatus(){
        statusData.HP = 100;
        statusData.nowHP = 100;
        statusData.HPincrement = 0;

        statusData.ATK = 100;
        statusData.ATKincrement = 0;
        statusData.DEF = 0;
        statusData.DEFincrement = 0;
        statusData.SPD = 100;
        statusData.SPDincrement = 0;
        statusData.CRITRATE = 50;
        statusData.CRITRATEincrement = 0;
        statusData.CRITDMG = 50;
        statusData.CRITDMGincrement = 0;
    }

///<summary>
/// HP レベルアップ
///</summary>
    public void HPincrease(int HPplus){
        statusData.HP += HPplus;       // 最大値アップ
        statusData.HPincrement += HPplus;
        Debug.Log("HP level up!! + " + HPplus);
    }
///<summary>
/// ATK レベルアップ
///</summary>
    public void ATKincrease(int ATKplus){
        statusData.ATK += ATKplus;
        statusData.ATKincrement += ATKplus;
        Debug.Log("Attack level up!! + " + ATKplus);
    }
///<summary>
/// DEF レベルアップ
///</summary>
    public void DEFincrease(int DEFplus){
        statusData.DEF += DEFplus;
        statusData.DEFincrement += DEFplus;
        Debug.Log("Defence level up!! + " + DEFplus);
    }
///<summary>
/// SPD レベルアップ
///</summary>
    public void SPDincrease(int SPDplus){
        statusData.SPD += SPDplus;
        statusData.SPDincrement += SPDplus;
        Debug.Log("Speed level up!! + " + SPDplus);
    }
///<summary>
/// CRITRATE レベルアップ
///</summary>
    public void CRITRATEincrease(int CRplus){
        statusData.CRITRATE += CRplus;
        statusData.CRITRATEincrement += CRplus;
        Debug.Log("CriticalRate level up!! + " + CRplus);
    }
///<summary>
/// CRITDMG レベルアップ
///</summary>
    public void CRITDMGincrease(int CDplus){
        statusData.CRITDMG += CDplus;
        statusData.CRITDMGincrement += CDplus;
        Debug.Log("CriticalDamage level up!! + " + CDplus);
    }
}
