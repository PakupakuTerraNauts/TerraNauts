using System.Collections;
using UnityEngine;

public class StageCamera : MonoBehaviour
{
    public static void JumpZoomOut(float duration)
    {
        Instance.StartCoroutine(VisionChangeCoroutine(Camera.main.orthographicSize, 8f, duration));
    }

    public static void JumpZoomIn(float duration)
    {
        Instance.StartCoroutine(VisionChangeCoroutine(Camera.main.orthographicSize, 5f, duration));
    }

    private static IEnumerator VisionChangeCoroutine(float startSize, float targetSize, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float newSize = Mathf.Lerp(startSize, targetSize, elapsedTime / duration);
            Camera.main.orthographicSize = newSize;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Camera.main.orthographicSize = targetSize; // ターゲットサイズに到達したら確実にサイズを設定する
    }

    // StageCameraのインスタンスへの参照を提供する静的フィールド
    private static StageCamera instance;
    public static StageCamera Instance
    {
        get
        {
            if (instance == null)
            {
                // シーン内でStageCameraコンポーネントを検索してインスタンスを設定する
                instance = FindObjectOfType<StageCamera>();
            }
            return instance;
        }
    }
}
