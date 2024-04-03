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
        Camera.main.orthographicSize = targetSize; // �^�[�Q�b�g�T�C�Y�ɓ��B������m���ɃT�C�Y��ݒ肷��
    }

    // StageCamera�̃C���X�^���X�ւ̎Q�Ƃ�񋟂���ÓI�t�B�[���h
    private static StageCamera instance;
    public static StageCamera Instance
    {
        get
        {
            if (instance == null)
            {
                // �V�[������StageCamera�R���|�[�l���g���������ăC���X�^���X��ݒ肷��
                instance = FindObjectOfType<StageCamera>();
            }
            return instance;
        }
    }
}
