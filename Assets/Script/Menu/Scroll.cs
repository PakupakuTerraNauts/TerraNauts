using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
public class Scroll:MonoBehaviour
{
    RectTransform scrollRectTransform;
    RectTransform contentPanel;
    RectTransform selectedRectTransform;
    GameObject lastSelected;
    void Start()
    {
        scrollRectTransform = GetComponent<RectTransform>();
        contentPanel = GetComponent<ScrollRect>().content;
    }
    void Update()
    {
        // ���ݑI������Ă��� UI �v�f��EventSystem����擾
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        // ���݂��Ȃ��ꍇ
        if(selected == null)
        {
            return;
        }
        // �I�������Q�[���I�u�W�F�N�g���X�N���[���̈���ɂȂ��ꍇ
        if(selected.transform.parent != contentPanel.transform)
        {
            return;
        }
        // �I�������Q�[���I�u�W�F�N�g���Ō�̃t���[���Ɠ������ǂ���
        if(selected == lastSelected)
        {
            return;
        }
        // �I�������Q�[���I�u�W�F�N�g�̎l�p�`�ϊ����擾
        selectedRectTransform = selected.GetComponent<RectTransform>();
        // �I������ UI �v�f�̈ʒu�͐�΃A���J�[�ʒu�ł��B
        // �܂�B�X�N���[���l�p�`���̃��[�J���ʒu + ���̍��� (���̏ꍇ)
        // ���ɃX�N���[�����܂��B��ɃX�N���[�����Ă���ꍇ�A����͒P�Ȃ��ΓI�ȃA���J�[�ʒu
        float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;
        // �X�N���[�� �r���[�̏���́A�X�N���[�����Ă���R���e���c�̃A���J�[�ʒu
        float scrollViewMinY = contentPanel.anchoredPosition.y;
        // �����̓A���J�[�ʒu + �X�N���[���l�p�`�̍���
        float scrollViewMaxY = contentPanel.anchoredPosition.y + scrollRectTransform.rect.height;
        // �I�������ʒu���X�N���[�� �r���[�̌��݂̉�����艺�ɂ���ꍇ�́A���ɃX�N���[��
        if(selectedPositionY > scrollViewMaxY)
        {
            float newY = selectedPositionY - scrollRectTransform.rect.height;
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
        }
        // �I�������ʒu���X�N���[�� �r���[�̌��݂̏������ɂ���ꍇ�́A��ɃX�N���[��
        else if(Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY)
        {
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, Mathf.Abs(selectedRectTransform.anchoredPosition.y) - 50);
        }
        lastSelected = selected;
    }
}
