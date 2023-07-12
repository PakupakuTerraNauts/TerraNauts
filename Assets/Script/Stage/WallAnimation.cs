using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallAnimation : MonoBehaviour
{
    public GameObject wall;
    //[SerializeField] private Vector3 startPos;
    //[SerializeField] private Vector3 endPos;
    private float distance;
    private float speed = 3f;
    private void Start() {
        //Transform transform = wall.transform;
        Debug.Log("できたよ");
    
    }
    public void Update() {
         //Vector3 pos = transform.position;
    }

/*    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            //distance = endPos.y - startPos.y;
            //float present_Location = (Time.timeSinceLevelLoad * speed) / distance;
            Transform transform = wall.transform;
            //float start = Time.frameCount;
            
            Debug.Log("当たったよ!!!");
            while(true) {
                transform = wall.transform;
                Vector3 pos = transform.position;
                Debug.Log("動いてるよ!!");
                transform.Translate (0, 1.0f, 0);
                //transform.position = Vector3.Lerp(startPos, endPos, present_Location);
                if(pos.y >= -27) break;
                
            //}
        }
    }
    
}
*/
}