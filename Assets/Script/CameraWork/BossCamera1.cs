using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera1 : MonoBehaviour
{
    public Transform player;  // Player��Transform
    public Transform Entrance;  // �{�X������Transform
    public Transform Exit;  // �J���ǂ�Transform
    public EnteredBossRoom entranceBossRoom;
    public EntranceDoor entranceDoor;
    public ExitDoor exitDoor;

    public float hallwaySpeed = 5f;  // �L���̑���
    public float bossRoomSpeed = 0.1f;  // �{�X�����ł̑���
    public float fixedCameraDistance = 10f;  // �t�F�[�Y2�ł̃J�����̌Œ苗��
    private Vector3 targetPosition = new Vector3(0.0f, 0.0f, 0.0f);

    private int phase = 1;
    private bool isBossRoom = false;

    public Debidora debidora;

    public BossBGM bgm;
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
                // �t�F�[�Y1: �L���̒Ǐ]
                FollowPlayerInHallway();
                break;

            case 2:
                // �t�F�[�Y2: �J�����̌Œ�ƃ{�X�����̒Ǐ]
                FixedCameraInBossRoom();
                break;

            case 3:
                // �t�F�[�Y3: �{�X��|������̒Ǐ]
                FollowPlayerAfterBossDefeated();
                break;
        }
    }

    void FollowPlayerInHallway()
{
    // Player��Ǐ]
    float targetX = Mathf.Min(player.position.x, Entrance.position.x - 8.5f);  // �E�[�̐��� �L������{�X������`���Ȃ�
    transform.position = new Vector3(targetX, y, player.position.z - 20f);

    // �{�X�����ɋ߂Â�����t�F�[�Y2�Ɉڍs
    if (entranceBossRoom.isEnter)
    {
        phase = 2;
        isBossRoom = true;
    }
}


    void FixedCameraInBossRoom()
    {
        if(isBossRoom){
            // �J�����𕔉��̒����ɔz�u
            float targetX = Entrance.transform.position.x + (Exit.position.x - Entrance.transform.position.x) / 2;
            targetPosition = new Vector3(targetX, Entrance.transform.position.y + 6.0f, transform.position.z);
            isBossRoom = false;
        }

        // �h�A���J���Ă���ꍇ�̓v���[���[�ɒǏ]�A�܂�����Œ�
        if (entranceDoor.isOpen){
            // Player��Ǐ]
            transform.position = new Vector3(player.position.x, y, z);
            isBossRoom = true;
        } else {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * bossRoomSpeed);
            if(transform.position != targetPosition){
                isBossRoom = true;
            }
            Camera.main.orthographicSize = 9f;
            if(!isPlayed){
                bgm.BossFightBGMStart();
                isPlayed = true;
            }
        }

        // �ǂ��J������t�F�[�Y3�Ɉڍs
        if (exitDoor.isOpen)
        {
            phase = 3;
            Camera.main.orthographicSize = 5f;
            bgm.BossFightBGMStop();
        }
    }

    void FollowPlayerAfterBossDefeated()
    {
        // �v���[���[��Ǐ]
        float targetX = Mathf.Min(player.position.x, 60f);  // �E�[�̐���
        transform.position = new Vector3(targetX, y, player.position.z - 20f);
    }
}
