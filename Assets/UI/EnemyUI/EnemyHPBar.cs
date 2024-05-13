using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Canvas canvas;
    public int E_maxHP;
    public int E_nowHP;
    public GameObject _HPSlider;

    void Update()
    {
        // EnemyCanvasをMain Cameraに向かせる
        if (canvas != null && Camera.main != null)
        {
            canvas.transform.rotation = Camera.main.transform.rotation;
        }

        SetHpBar();
    }

    public void SetHpBar()
    {
        if (_HPSlider != null)
        {
            Slider HPSlider_S = _HPSlider.GetComponent<Slider>();

            if (HPSlider_S != null)
            {
                HPSlider_S.maxValue = E_maxHP;
                HPSlider_S.value = E_nowHP;
            }
            else
            {
                Debug.LogError("_HPSlider GameObjectにSliderコンポーネントが見つかりませんでした。");
            }
        }
        else
        {
            Debug.LogError("_HPSlider GameObjectが割り当てられていません。");
        }
    }
}
