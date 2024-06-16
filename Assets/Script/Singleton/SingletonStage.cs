using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonStage : MonoBehaviour
{
    public static SingletonStage instance;

    public List<Transform> enemies;

    void Awake(){
        // シングルトン 敵、アイテムを子にして管理する
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            enemies = new List<Transform>();
            foreach(Transform child in transform){
                if(child.gameObject.tag == "Enemy" || child.gameObject.tag == "EnemySleep"){
                    enemies.Add(child);
                }
            }
        }
        else{
            Destroy(this.gameObject);
        }
    }

/// <summary>
/// ゲームオーバー時に呼ばれ 倒れている敵を復活させる
/// </summary>
    public void RespawnDeadEnemy(){
        foreach(Transform t in enemies){
            Enemy enemy = t.GetComponent<Enemy>();
            if(enemy != null){
                enemy.Spawn();
            }
        }
    }

/// <summary>
/// セーブ時に呼ばれ 倒された敵はリストから削除する
/// </summary>
    public void SaveDeadEnemy(){
        for (int i = enemies.Count - 1; i >= 0; i--){
            Transform t = enemies[i];
            Enemy enemy = t.GetComponent<Enemy>();
            if(enemy != null){
                bool deleted = enemy.DeleteDead();
                if(deleted){
                    t = null;
                    enemies.RemoveAt(i);
                }
            }
        }
    }
}