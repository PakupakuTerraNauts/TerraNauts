using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredBossRoom : MonoBehaviour
{
    public bool isEnter = false;
    public Debidora debidora;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            // BossCamera1のフェーズ2のフラグ
            isEnter = true;
            // １層ボスのHPバー操作
            debidora.BossHPCountUp();
            Destroy(gameObject, 3f);
        }
    }
}
