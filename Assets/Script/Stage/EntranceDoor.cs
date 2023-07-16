using UnityEngine;

public class EntranceDoor : MonoBehaviour
{
    //　ドアのアニメーター
    [SerializeField]
    [Tooltip("自動ドアのアニメーター")]
    private Animator automaticDoorAnimator;

    /// <summary>
    /// 自動ドア検知エリアに入った時
    /// </summary>
    /// <param name="other"></param>
	private void OnTriggerEnter2D(Collider2D other)
    {
        // アニメーションパラメータをtrueにする。(ドアが開く)
        Debug.Log("さわった!!");
        automaticDoorAnimator.SetBool("Open", true);
    }

    /// <summary>
    /// 自動ドア検知エリアを出た時
    /// </summary>
    /// <param name="other"></param>
	private void OnTriggerExit2D(Collider2D other)
    {
        // アニメーションパラメータをfalseにする。(ドアが閉まる)
        Debug.Log("判定から離れた!");
        Invoke("CloseDoor", 1.0f);
    }

    private void CloseDoor() {
        Debug.Log("ドア閉まるよ");
        automaticDoorAnimator.SetBool("Open", false);
    }
}