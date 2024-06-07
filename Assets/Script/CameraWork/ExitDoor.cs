using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private Animator anim = null;
    [HideInInspector] public bool isOpen = false;

    void Awake(){
        anim = GetComponent<Animator>();
    }

/// <summary>
/// �{�X���� �o�����J����
/// </summary>
    public void OpenDoor(){
        anim.SetBool("Open", true);
        isOpen = true;
    }
}
