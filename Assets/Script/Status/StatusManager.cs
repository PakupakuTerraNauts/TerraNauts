using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ステータス表示
public class StatusManager:MonoBehaviour
{
    public PlayerFoodManager _playerFoodManager;
    
    public GameObject HPObject;
    public GameObject ATKObject;
    public GameObject DEFObject;
    public GameObject SPDObject;
    public GameObject CRITRATEObject;
    public GameObject CRITDMGObject;

    private int HP = Player.HP + Player.HPincrement;
    private int ATK = Player.ATK + Player.ATKincrement;
    private int DEF = Player.DEF + Player.DEFincrement;
    private int SPD = Player.SPD + Player.SPDincrement;
    private int CRITRATE = Player.CRITRATE + Player.CRITRATEincrement;
    private int CRITDMG = Player.CRITDMG + Player.CRITDMGincrement;

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
        Player.HP += Player.HPincrement;
        Player.HPincrement = 0;
        Player.ATK += Player.ATKincrement;
        Player.ATKincrement = 0;
        Player.DEF += Player.DEFincrement;
        Player.DEFincrement = 0;
        Player.SPD += Player.SPDincrement;
        Player.SPDincrement = 0;
        Player.CRITRATE += Player.CRITDMGincrement;
        Player.CRITRATEincrement = 0;
        Player.CRITDMG += Player.CRITDMGincrement;
        Player.CRITDMGincrement = 0;
    }

/// <summary>
/// セーブしていないステータスをリセット
/// </summary>
    public static void PlayerStatusReset(){
        Status.HP -= Player.HPincrement;
        Status.ATK -= Player.ATKincrement;
        Status.DEF -= Player.DEFincrement;
        Status.SPD -= Player.SPDincrement;
        Status.CRITRATE -= Player.CRITRATEincrement;
        Status.CRITDMG -= Player.CRITDMGincrement;
        Player.HPincrement = 0;
        Player.ATKincrement = 0;
        Player.DEFincrement = 0;
        Player.SPDincrement = 0;
        Player.CRITRATEincrement = 0;
        Player.CRITDMGincrement = 0;
    }
}
