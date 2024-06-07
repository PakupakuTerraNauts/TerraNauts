using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;
    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // 現在の床の位置が目的地に非常に近い場合
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            StartCoroutine(WaitTime());
            // 目的地を次のポイントにセットする
            currentWaypointIndex++;
            // 最後まで行ったら，一番最初のポイントを目的地とする
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        // 現在の床の位置から，目的地の位置まで移動する
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }

    IEnumerator WaitTime()
    {
        // 移動を停止する
        rb.isKinematic = true;
        // 0.5秒間待機する
        yield return new WaitForSecondsRealtime(3f);
        // 移動を再開する
        rb.isKinematic = false;
       /*Pauser.Pause ();
// 技タイトルカットイン
ShowSpTitle(sp.spNo);
//1秒後に再開させる
Timer timer = new Timer ();
timer.Start (1f, 1f);
timer.Finished += delegate() {
	Pauser.Resume ();
    
}
*/
        Debug.Log("Finish WaitTime");
    }
}