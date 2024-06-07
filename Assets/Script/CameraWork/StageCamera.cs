using System.Collections;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    /// <summary>
    /// �W�����v�ŃY�[���A�E�g
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="maxVision">maxVision��傫������Ǝ��삪�L����</param>
    public void JumpZoomOut(float duration, float maxVision)
    {
        StartCoroutine(VisionChangeCoroutine(Camera.main.orthographicSize, maxVision, duration));
    }

    /// <summary>
    /// �W�����v�ŃY�[���C��
    /// </summary>
    /// <param name="duration"></param>
    public void JumpZoomIn(float duration)
    {
        StartCoroutine(VisionChangeCoroutine(Camera.main.orthographicSize, 5f, duration));
    }

    /// <summary>
    /// �Y�[���C�� �A�E�g ������R���[�`��
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
        Camera.main.orthographicSize = targetSize; // �^�[�Q�b�g�T�C�Y�ɓ��B������m���ɃT�C�Y��ݒ肷��
    }
}
