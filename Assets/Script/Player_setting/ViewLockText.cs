using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class ViewLockText : MonoBehaviour
{
    private bool viewLock = false;

    public GameObject viewLockCanvas;
    public GameObject viewLockObj;
    private Text viewLockText;

    void Start(){
        viewLockText = viewLockObj.GetComponent<Text>();

        string SceneName = SceneManager.GetActiveScene().name;
        if(!Regex.IsMatch(SceneName, @"^Stage\d+$")){   // StageX �V�[���ł̂݃X�C�b�`��\��
            viewLockCanvas.SetActive(false);
        }

        viewLock = Player.viewLock;
        SwitchViewLock();
    }

    void Update(){
        bool vKey = Input.GetKeyDown("v");

        if(vKey){
            SwitchViewLock();
        }
    }

    private void SwitchViewLock(){  // ���E�Œ� �I���I�t�̐؂�ւ�
        if(!viewLock){
            viewLock = true;
            viewLockText.text = "���E���Œ�I�t";
        }
        else{
            viewLock = false;
            viewLockText.text = "���E���Œ�I��";
        }
    }
}
