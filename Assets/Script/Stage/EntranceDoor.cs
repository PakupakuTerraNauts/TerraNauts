using UnityEngine;

public class EntranceDoor : MonoBehaviour
{
    //�h�A�̃A�j���[�^�[
    [SerializeField]
    [Tooltip("�����h�A�̃A�j���[�^�[")]
    private Animator automaticDoorAnimator;
    public bool isOpen = false;

    /// <summary>
    /// �����h�A���m�G���A�ɓ�������
    /// </summary>
    /// <param name="other"></param>
	private void OnTriggerEnter2D(Collider2D other)
    {
        //  �A�j���[�V�����p�����[�^��true�ɂ���B(�h�A���J��)
        Debug.Log("�������!!");
        automaticDoorAnimator.SetBool("Open", true);
        isOpen = true;
    }

    /// <summary>
    /// �����h�A���m�G���A���o����
    /// </summary>
    /// <param name="other"></param>
	private void OnTriggerExit2D(Collider2D other)
    {
        //  �A�j���[�V�����p�����[�^��false�ɂ���B(�h�A���܂�)
        Debug.Log("���肩�痣�ꂽ!");
        Invoke("CloseDoor", 1.0f);
    }

    private void CloseDoor() {
        Debug.Log("�h�A�܂��");
        automaticDoorAnimator.SetBool("Open", false);
        isOpen = false;
        Destroy(gameObject, 1f);
    }
}