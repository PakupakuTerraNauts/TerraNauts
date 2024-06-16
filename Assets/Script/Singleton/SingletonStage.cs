using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonStage : MonoBehaviour
{
    public static SingletonStage instance;

    public List<Transform> enemies;

    void Awake(){
        // �V���O���g�� �G�A�A�C�e�����q�ɂ��ĊǗ�����
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
/// �Q�[���I�[�o�[���ɌĂ΂� �|��Ă���G�𕜊�������
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
/// �Z�[�u���ɌĂ΂� �|���ꂽ�G�̓��X�g����폜����
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