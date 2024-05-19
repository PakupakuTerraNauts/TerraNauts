using System.Collections;
using UnityEngine;

public class StageCamera : MonoBehaviour
{
    public void JumpZoomOut(float duration, float maxVision)
    {
        StartCoroutine(VisionChangeCoroutine(Camera.main.orthographicSize, maxVision, duration));
    }

    public void JumpZoomIn(float duration)
    {
        StartCoroutine(VisionChangeCoroutine(Camera.main.orthographicSize, 5f, duration));
    }

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
