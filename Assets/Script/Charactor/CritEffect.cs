using System.Collections;
using UnityEngine;

public class CritEffect : MonoBehaviour {
    public static CritEffect instance = null;

    private GameObject CriticalEffect;
    [SerializeField] private GameObject CritPrefab;
    private ObjectPool pool;

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
        pool = ObjectPool.instance;
        if(pool != null)
            Debug.Log(pool);
            else
                Debug.Log("nuul + " + pool);
    }

/// <summary>
/// クリティカルエフェクト表示
/// </summary>
/// <param name="critposition">座標</param>
/// <param name="ratio">倍率</param>
    public void CriticalHit(Vector2 critposition, float ratio) {
        CriticalEffect = pool.GetObject(this.gameObject, CritPrefab);
        CriticalEffect.transform.position = new Vector2(critposition.x, critposition.y);
        CriticalEffect.transform.localScale = new Vector3(ratio, ratio, 0f);
        StartCoroutine(criticalHit());
    }

    private IEnumerator criticalHit() {
        SpriteRenderer criticalSr = CriticalEffect.GetComponent<SpriteRenderer>();
        criticalSr.enabled = true;
        yield return new WaitForSeconds(1.0f);
        criticalSr.enabled = false;
        pool.ReturnObject(CriticalEffect);
    }
}
