using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRespawn : MonoBehaviour
{
    public GameObject Player;
    static public Vector2 TutorialStartPos;

    void Start(){
        TutorialStartPos = new Vector2(0f, 4f);
    }
    
    void OnTriggerEnter2D(Collider2D collision){
        Player.transform.position = TutorialStartPos;
    }
}
