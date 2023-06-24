using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{

    public GameObject HPObject;
    public GameObject ATKObject;
    public GameObject DEFObject;
    public GameObject SPDObject;
    public GameObject CRITRATEObject;
    public GameObject CRITDMGObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text HPText = HPObject.GetComponent<Text>();
        string HP_S = Status.HP.ToString("d");
        HPText.text = HP_S;

        Text ATKText = ATKObject.GetComponent<Text>();
        string ATK_S = Status.ATK.ToString("d");
        ATKText.text = ATK_S;

        Text DEFText = DEFObject.GetComponent<Text>();
        string DEF_S = Status.DEF.ToString("d");
        DEFText.text = DEF_S;

        Text SPDText = SPDObject.GetComponent<Text>();
        string SPD_S = Status.SPD.ToString("d");
        SPDText.text = SPD_S;

        Text CRITRATEText = CRITRATEObject.GetComponent<Text>();
        string CRITRATE_S = Status.CRITRATE.ToString("d");
        CRITRATEText.text = CRITRATE_S;

        Text CRITDMGText = CRITDMGObject.GetComponent<Text>();
        string CRITDMG_S = Status.CRITDMG.ToString("d");
        CRITDMGText.text = CRITDMG_S;

    }
}
