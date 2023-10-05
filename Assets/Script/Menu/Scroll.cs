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
        // 現在選択されている UI 要素をEventSystemから取得
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        // 存在しない場合
        if(selected == null)
        {
            return;
        }
        // 選択したゲームオブジェクトがスクロール領域内にない場合
        if(selected.transform.parent != contentPanel.transform)
        {
            return;
        }
        // 選択したゲームオブジェクトが最後のフレームと同じかどうか
        if(selected == lastSelected)
        {
            return;
        }
        // 選択したゲームオブジェクトの四角形変換を取得
        selectedRectTransform = selected.GetComponent<RectTransform>();
        // 選択した UI 要素の位置は絶対アンカー位置です。
        // つまり。スクロール四角形内のローカル位置 + その高さ (次の場合)
        // 下にスクロールします。上にスクロールしている場合、それは単なる絶対的なアンカー位置
        float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;
        // スクロール ビューの上限は、スクロールしているコンテンツのアンカー位置
        float scrollViewMinY = contentPanel.anchoredPosition.y;
        // 下限はアンカー位置 + スクロール四角形の高さ
        float scrollViewMaxY = contentPanel.anchoredPosition.y + scrollRectTransform.rect.height;
        // 選択した位置がスクロール ビューの現在の下限より下にある場合は、下にスクロール
        if(selectedPositionY > scrollViewMaxY)
        {
            float newY = selectedPositionY - scrollRectTransform.rect.height;
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
        }
        // 選択した位置がスクロール ビューの現在の上限より上にある場合は、上にスクロール
        else if(Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY)
        {
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, Mathf.Abs(selectedRectTransform.anchoredPosition.y) - 50);
        }
        lastSelected = selected;
    }
}
