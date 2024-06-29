using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ステータス表示
public class StatusManager:MonoBehaviour
{
    public PlayerFoodManager _playerFoodManager;
    
        #region // status
        public static int HP = 100;
        public static int nowHP = 100;
        public static int HPincrement = 0;

        public static int ATK = 100;
        public static int ATKincrement = 0;
        public static int DEF = 0;
        public static int DEFincrement = 0;
        public static int SPD = 100;
        public static int SPDincrement = 0;
        // ↓ Enemy.cs内で使用している
        public static int CRITRATE = 50;
        public static int CRITRATEincrement = 0;
        public static int CRITDMG = 50;
        public static int CRITDMGincrement = 0;
        #endregion

    public GameObject HPObject;
    public GameObject ATKObject;
    public GameObject DEFObject;
    public GameObject SPDObject;
    public GameObject CRITRATEObject;
    public GameObject CRITDMGObject;

    // Update is called once per frame
    void Update()
    {
        Text HPText = HPObject.GetComponent<Text>();
        string HP_S = HP.ToString("d");
        HPText.text = HP_S;

        Text ATKText = ATKObject.GetComponent<Text>();
        string ATK_S = ATK.ToString("d");
        ATKText.text = ATK_S;

        Text DEFText = DEFObject.GetComponent<Text>();
        string DEF_S = DEF.ToString("d");
        DEFText.text = DEF_S;

        Text SPDText = SPDObject.GetComponent<Text>();
        string SPD_S = SPD.ToString("d");
        SPDText.text = SPD_S;

        Text CRITRATEText = CRITRATEObject.GetComponent<Text>();
        string CRITRATE_S = CRITRATE.ToString("d");
        CRITRATEText.text = CRITRATE_S;

        Text CRITDMGText = CRITDMGObject.GetComponent<Text>();
        string CRITDMG_S = CRITDMG.ToString("d");
        CRITDMGText.text = CRITDMG_S;

    }

/// <summary>
/// ステータス上昇分をセーブ
/// </summary>
    public static void PlayerStatusSave(){
        HPincrement = 0;
        ATKincrement = 0;
        DEFincrement = 0;
        SPDincrement = 0;
        CRITRATEincrement = 0;
        CRITDMGincrement = 0;
    }

/// <summary>
/// セーブしていないステータスをリセット
/// </summary>
    public static void PlayerStatusReset(){
        HP -= HPincrement;
        ATK -= ATKincrement;
        DEF -= DEFincrement;
        SPD -= SPDincrement;
        CRITRATE -= CRITRATEincrement;
        CRITDMG -= CRITDMGincrement;
        
        PlayerStatusSave();
    }

/// <summary>
/// ステータスを初期化
/// </summary>
    public static void InitializePlayerStatus(){
        HP = 100;
        nowHP = 100;
        HPincrement = 0;

        ATK = 100;
        ATKincrement = 0;
        DEF = 0;
        DEFincrement = 0;
        SPD = 100;
        SPDincrement = 0;
        CRITRATE = 50;
        CRITRATEincrement = 0;
        CRITDMG = 50;
        CRITDMGincrement = 0;
    }

///<summary>
/// HP レベルアップ
///</summary>
    public static void HPincrease(int HPplus){
        HP += HPplus;       // 最大値アップ
        HPincrement += HPplus;
        Debug.Log("HP level up!! + " + HPplus);
    }
///<summary>
/// ATK レベルアップ
///</summary>
    public static void ATKincrease(int ATKplus){
        ATK += ATKplus;
        ATKincrement += ATKplus;
        Debug.Log("Attack level up!! + " + ATKplus);
    }
///<summary>
/// DEF レベルアップ
///</summary>
    public static void DEFincrease(int DEFplus){
        DEF += DEFplus;
        DEFincrement += DEFplus;
        Debug.Log("Defence level up!! + " + DEFplus);
    }
///<summary>
/// SPD レベルアップ
///</summary>
    public static void SPDincrease(int SPDplus){
        SPD += SPDplus;
        SPDincrement += SPDplus;
        Debug.Log("Speed level up!! + " + SPDplus);
    }
///<summary>
/// CRITRATE レベルアップ
///</summary>
    public static void CRITRATEincrease(int CRplus){
        CRITRATE += CRplus;
        CRITRATEincrement += CRplus;
        Debug.Log("CriticalRate level up!! + " + CRplus);
    }
///<summary>
/// CRITDMG レベルアップ
///</summary>
    public static void CRITDMGincrease(int CDplus){
        CRITDMG += CDplus;
        CRITDMGincrement += CDplus;
        Debug.Log("CriticalDamage level up!! + " + CDplus);
    }
}
