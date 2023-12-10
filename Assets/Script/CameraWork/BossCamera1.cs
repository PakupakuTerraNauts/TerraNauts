using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera1 : MonoBehaviour
{
    public Transform player;  // PlayerのTransform
    public Transform Entrance;  // ボス部屋のTransform
    public Transform Exit;  // 開く壁のTransform
    public EnteredBossRoom entranceBossRoom;
    public EntranceDoor entranceDoor;

    public float hallwaySpeed = 5f;  // 廊下の速さ
    public float bossRoomSpeed = 0.1f;  // ボス部屋での速さ
    public float fixedCameraDistance = 10f;  // フェーズ2でのカメラの固定距離

    private int phase = 1;
    private bool isEnter = false;
    private bool isBoss = false;

    void Update()
    {
        switch (phase)
        {
            case 1:
                // フェーズ1: 廊下の追従
                FollowPlayerInHallway();
                break;

            case 2:
                // フェーズ2: カメラの固定とボス部屋の追従
                FixedCameraInBossRoom();
                break;

            case 3:
                // フェーズ3: ボスを倒した後の追従
                FollowPlayerAfterBossDefeated();
                break;
        }
    }

    void FollowPlayerInHallway()
{
    // Playerを追従
    float targetX = Mathf.Min(player.position.x, Entrance.position.x - 8.5f);  // 右端の制限
    transform.position = new Vector3(targetX, transform.position.y, player.position.z - 20f);

    // ボス部屋に近づいたらフェーズ2に移行
    if (entranceBossRoom.isEnter)
    {
        phase = 2;
        isBoss = true;
    }
}


    void FixedCameraInBossRoom()
    {
        if(isBoss){
            // カメラを部屋の中央に配置
            float targetX = Entrance.transform.position.x + (Exit.position.x - Entrance.transform.position.x) / 2;
            Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
            isBoss = false;

            // ドアが開いている場合はカメラを移動し、開いていない場合はプレーヤーに追従
            if (entranceDoor.isOpen){
                // Playerを追従
                transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
                isBoss = true;
            } else {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * bossRoomSpeed);
                if(transform.position != targetPosition){
                    isBoss = true;
                }
                Camera.main.orthographicSize = 9f;
            }
        }

        // 壁が開いたらフェーズ3に移行
        if (Vector3.Distance(transform.position, Exit.position) < 0.1f)
        {
            phase = 3;
            Camera.main.orthographicSize = 5f;
        }
    }

    void FollowPlayerAfterBossDefeated()
    {
        // プレーヤーを追従
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), Time.deltaTime * hallwaySpeed);
    }

    // ボスを倒したときに呼び出すメソッド
    public void BossDefeated()
    {
        // フェーズ3に移行
        phase = 3;
    }
}
