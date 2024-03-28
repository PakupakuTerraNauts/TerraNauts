using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredBossRoom : MonoBehaviour
{
    public bool isEnter = false;
    public Debidora debidora;
    public EntranceDoor entrance;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isEnter){
            if(collision.tag == "Player"){
                Debug.Log("入った");
                // BossCamera1のフェーズ２のフラグ
                isEnter = true;
                entrance.CloseDoor();
                // １層ボスのHPバー操作
                debidora.BossHPCountUp();
                entrance.JudgeDestory();
                Destroy(gameObject, 3f);
            }
        }
    }
}
