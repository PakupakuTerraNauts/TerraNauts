using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int nowStage = 1;  // セーブ機能に使用

    public int ninzinEXP = 80;

    private AudioSource _audio = null;
    [HideInInspector] public float nowVolumeSE = 0.5f;
    
    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    void Start(){
        _audio = GetComponent<AudioSource>();
        _audio.volume = nowVolumeSE;
    }

///<summary>
/// SEを鳴らす
///</summary>
    public void PlaySE(AudioClip clip){

        if(_audio != null){
            _audio.PlayOneShot(clip);
        }
        else{
            Debug.Log("GM にAudioSourceがアタッチされていない");
        }
    }

/// <summary>
/// SE音量を変更したとき VolumeSE から呼ぶ
/// </summary>
/// <param name="vol">音量</param>
    public void ChangeVolumeSE(float vol){
        _audio.volume = vol;
        nowVolumeSE = vol;
    }

/// <summary>
/// ステージクリアシーン に遷移する
/// </summary>
    public void callLoadingClear(){
        // 動きを止める
        Time.timeScale = 0;
        Player.RestrainedByEvent();
        
        StartCoroutine(LoadingClear());
    }

    IEnumerator LoadingClear(){
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene("Clear", LoadSceneMode.Additive);
    }

/// <summary>
/// ダメージ計算機
/// </summary>
/// <returns>(整数)ダメージ</returns>
    public int CalculateDamage(criticalEffect onCriticalEffect){
        var playerStatusData = Resources.Load<PlayerStatusData>("PlayerStatusData");    // スクリプタブルオブジェクトで設定している

        int atk = playerStatusData.ATK + playerStatusData.ATKincrement;
        if(Rand.RandomTF((playerStatusData.CRITRATE + playerStatusData.CRITRATEincrement) / 5)){
            atk += (playerStatusData.CRITDMG + playerStatusData.CRITDMGincrement) * 2;
            criticalEffectCallBack = onCriticalEffect;
            criticalEffectCallBack();
        }
        return atk;
    }

    // クリティカルヒットしたときにエネミーに通知する
    public delegate void criticalEffect();
    private criticalEffect criticalEffectCallBack;
    public void InitializeCallBack(criticalEffect onCriticalEffect){
        criticalEffectCallBack = onCriticalEffect;
    }
}
