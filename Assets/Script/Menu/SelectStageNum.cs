using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStageNum : MonoBehaviour
{
    private Dropdown StageNumList = null;

    void Start(){
        StageNumList = GetComponent<Dropdown>();

        int maxStageNum = 5;//PlayerPrefs.GetInt("StageNum", 1);
        StageNumList.options.Clear();

        for(int i = 1; i <= maxStageNum; i++){
            string newStageName = "�X�e�[�W " + i;

            StageNumList.options.Add(new Dropdown.OptionData(newStageName));
        }
    }
}
