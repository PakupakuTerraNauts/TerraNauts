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
        //Rigidbody���擾
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // ���݂̏��̈ʒu���ړI�n�ɔ��ɋ߂��ꍇ
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            StartCoroutine(WaitTime());
            // �ړI�n�����̃|�C���g�ɃZ�b�g����
            currentWaypointIndex++;
            // �Ō�܂ōs������C��ԍŏ��̃|�C���g��ړI�n�Ƃ���
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        // ���݂̏��̈ʒu����C�ړI�n�̈ʒu�܂ňړ�����
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }

    IEnumerator WaitTime()
    {
        // �ړ����~����
        rb.isKinematic = true;
        // 0.5�b�ԑҋ@����
        yield return new WaitForSecondsRealtime(3f);
        // �ړ����ĊJ����
        rb.isKinematic = false;
       /*Pauser.Pause ();
// �Z�^�C�g���J�b�g�C��
ShowSpTitle(sp.spNo);
//1�b��ɍĊJ������
Timer timer = new Timer ();
timer.Start (1f, 1f);
timer.Finished += delegate() {
	Pauser.Resume ();
    
}
*/
        Debug.Log("Finish WaitTime");
    }
}