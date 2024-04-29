using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������O�ł̏���
public class KittenGet:MonoBehaviour
{
    public bool isKittenLoaded = false;
    bool trigger = false;
    Collider2D _collider;
    GameObject _cookInfoCanvas;

    private void Start()
    {
        _cookInfoCanvas = GameObject.Find("CookInfoCanvas");
        _cookInfoCanvas.SetActive(false);
    }

    //������ɐG��Ă��鎞
    private void OnTriggerStay2D(Collider2D collision)
    {
        trigger = true;
        _collider = collision;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger = true;
        _collider = collision;
    }

    //�����䂩��o����
    private void OnTriggerExit2D()
    {
        trigger = false;
        _collider = null;
    }

    [System.Obsolete]
    private void Update()
    {

        if(isKittenLoaded && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.UnloadLevel("CookingScean");
            Resources.UnloadUnusedAssets();
            isKittenLoaded = false;
        }


        //�v���C���[��������O��"E"����������
        if(trigger && _collider.gameObject.tag == ("Player") && Input.GetKeyDown(KeyCode.E) && !MenuChange.isMenuOpen)
        {
            isKittenLoaded = !isKittenLoaded;
            if(isKittenLoaded)
            {
                Application.LoadLevelAdditive("CookingScean");
            }
            else
            {
                Application.UnloadLevel("CookingScean");
                Resources.UnloadUnusedAssets();
            }
        }

        if(trigger && _collider.gameObject.tag == ("Player") && !MenuChange.isMenuOpen)
        {
            _cookInfoCanvas.SetActive(true);
        }
        else
        {
            _cookInfoCanvas.SetActive(false);
        }
    }

    public bool GetIsKittenLoaded()
    {
        return isKittenLoaded;
    }
}
