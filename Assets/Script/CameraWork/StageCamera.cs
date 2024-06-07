using System.Collections;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    /// <summary>
    /// ジャンプでズームアウト
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="maxVision">maxVisionを大きくすると視野が広がる</param>
    public void JumpZoomOut(float duration, float maxVision)
    {
        StartCoroutine(VisionChangeCoroutine(Camera.main.orthographicSize, maxVision, duration));
    }

    /// <summary>
    /// ジャンプでズームイン
    /// </summary>
    /// <param name="duration"></param>
    public void JumpZoomIn(float duration)
    {
        StartCoroutine(VisionChangeCoroutine(Camera.main.orthographicSize, 5f, duration));
    }

    /// <summary>
    /// ズームイン アウト をするコルーチン
    /// </summary>
    /// <param name="startSize"></param>
    /// <param name="targetSize"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator VisionChangeCoroutine(float startSize, float targetSize, float duration)
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
}
