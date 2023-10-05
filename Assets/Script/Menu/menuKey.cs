using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class menuKey:MonoBehaviour
{
    public GameObject status_button;
    public GameObject item_button;
    public GameObject skill_button;
    public GameObject setting_button;
    public GameObject load_button;
    public GameObject exit_button;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject nowObj = EventSystem.current.currentSelectedGameObject;
            switch(nowObj.name)
            {
                case "StatusButton":
                    item_button.GetComponent<Button>().Select();
                    break;

                case "ItemButton":
                    skill_button.GetComponent<Button>().Select();
                    break;

                case "SkillButton":
                    setting_button.GetComponent<Button>().Select();
                    break;

                case "SettingButton":
                    load_button.GetComponent<Button>().Select();
                    break;

                case "LoadButton":
                    exit_button.GetComponent<Button>().Select();
                    break;

                case "ExitButton":
                    status_button.GetComponent<Button>().Select();
                    break;

                default:
                    if(GameObject.Find("ItemCanvas"))
                    {
                        skill_button.GetComponent<Button>().Select();
                    }
                    else
                    {
                        load_button.GetComponent<Button>().Select();
                    }

                    break;
            }


        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            GameObject nowObj = EventSystem.current.currentSelectedGameObject;
            switch(nowObj.name)
            {
                case "StatusButton":
                    exit_button.GetComponent<Button>().Select();
                    break;

                case "ItemButton":
                    status_button.GetComponent<Button>().Select();
                    break;

                case "SkillButton":
                    item_button.GetComponent<Button>().Select();
                    break;

                case "SettingButton":
                    skill_button.GetComponent<Button>().Select();
                    break;

                case "LoadButton":
                    setting_button.GetComponent<Button>().Select();
                    break;

                case "ExitButton":
                    load_button.GetComponent<Button>().Select();
                    break;

                default:
                    if(GameObject.Find("ItemCanvas"))
                    {
                        status_button.GetComponent<Button>().Select();
                    }
                    else
                    {
                        skill_button.GetComponent<Button>().Select();
                    }
                    break;
            }
        }
    }
}
