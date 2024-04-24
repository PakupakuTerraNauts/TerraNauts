using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ステータス表示
public class StatusManager:MonoBehaviour
{

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
}
