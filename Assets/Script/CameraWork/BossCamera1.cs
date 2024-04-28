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
    public ExitDoor exitDoor;

    public float hallwaySpeed = 5f;  // 廊下の速さ
    public float bossRoomSpeed = 0.1f;  // ボス部屋での速さ
    public float fixedCameraDistance = 10f;  // フェーズ2でのカメラの固定距離
    private Vector3 targetPosition = new Vector3(0.0f, 0.0f, 0.0f);

    private int phase = 1;
    private bool isBossRoom = false;

    public Debidora debidora;
    public BGMReset_BOSS BGM_BOSS;

    [SerializeField, Header("廊下からボス部屋を覗けないようにする")] private float entranceDoorLimit;
    [SerializeField, Header("ボス部屋でのカメラの中心点")] private float yHeightInBossRoom;
    [SerializeField, Header("ステージ左側の限界点")] private float startPosLeftLimit;
    [SerializeField, Header("ステージ右側の限界点")] private float endPosRightLimit;

    private bool isPlayed = false;

    private float y;
    private float z;

    void Start(){
        y = transform.position.y;
        z = transform.position.z;
    }

    

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
    float targetX = Mathf.Min(player.position.x, Entrance.position.x - entranceDoorLimit);  // 右端の制限 廊下からボス部屋を覗けない
    targetX = Mathf.Max(targetX, startPosLeftLimit);
    transform.position = new Vector3(targetX, y, player.position.z - 20f);

    // ボス部屋に近づいたらフェーズ2に移行
    if (entranceBossRoom.isEnter)
    {
        phase = 2;
        isBossRoom = true;

        BGM_BOSS.BossFightBGM_Start();
    }
}


    void FixedCameraInBossRoom()
    {
        if(isBossRoom){
            // カメラを部屋の中央に配置
            float targetX = Entrance.transform.position.x + (Exit.position.x - Entrance.transform.position.x) / 2;
            targetPosition = new Vector3(targetX, Entrance.transform.position.y + yHeightInBossRoom, transform.position.z);
            isBossRoom = false;
        }

        // ドアが開いている場合はプレーヤーに追従、閉まったら固定
        if (entranceDoor.isOpen){
            // Playerを追従
            transform.position = new Vector3(player.position.x, y, z);
            isBossRoom = true;
        } else {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * bossRoomSpeed);
            if(transform.position != targetPosition){
                isBossRoom = true;
            }
            Camera.main.orthographicSize = 9f;
            if(!isPlayed){
                isPlayed = true;
            }
        }

        // 壁が開いたらフェーズ3に移行
        if (exitDoor.isOpen)
        {
            phase = 3;
            Camera.main.orthographicSize = 5f;

            BGM_BOSS.BossFightBGM_Stop();
        }
    }

    void FollowPlayerAfterBossDefeated()
    {
        // プレーヤーを追従
        float targetX = Mathf.Min(player.position.x, endPosRightLimit);  // 右端の制限
        transform.position = new Vector3(targetX, y, player.position.z - 20f);
    }
}
