using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debidora : MonoBehaviour
{
    #region // variables
    [SerializeField] private string Name;
    [SerializeField] private GameObject FirePrefab;
    [SerializeField] private GameObject PainPrefab;
    [SerializeField] private GameObject FramePrefab;
    [SerializeField] private GameObject LightPrefab;
    private Animator _animator;
    private float nowhp = 0.0f;
    private float maxhp = 0.0f;
    public bool isStart = true;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isDying = false;
    //private bool isNoHP = true;
    private Vector3 nowPosition;
    
    private float ATK_player = 0.0f;
    public bool isEntered = false;
    public EnteredBossRoom HPCountUpTrigger;
    [SerializeField] private ExitDoor exitDoor;
    [SerializeField] private BossCamera1 bossCamera;
    [SerializeField] public HPBar HP;
    [SerializeField] public Canvas HP_canvas;
    [SerializeField] private BGMReset_BOSS BossBGM;
    [SerializeField] private AudioClip RoarClip;

    private bossData Data;
    #endregion

    void Awake(){
        var bossData = Resources.Load<BossData>("BossData");
        foreach(var data in bossData.BossDataList){
            if(data.Name == Name)
                Data = data;
        }
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("IsWaiting", true);

        //HP = GetComponent<HPBar>();
        ATK_player = StatusManager.ATK;
        maxhp = Data.maxHP;
        nowhp = maxhp / 10.0f; // 登場時にカウントアップするため
        HP.SetHP(maxhp);

        HPCountUpTrigger.onEnteredBossRoom.AddListener(BossHPCountUp);
    }

    // Update is called once per frame
    void Update()
    {
        if(isEntered){
            
            if(nowhp <= 0)
            {
                //if nowhp is 0, stop Battle Coroutine
                nowhp = 0;
                BossBGM.BossFightBGM_Stop();
                isDead = true;
                _animator.SetTrigger("Dead");
                StopCoroutine("Battle");
                if(isDying == false)
                {
                    isDying = true;
                    gameObject.tag = "DeadEnemy";
                    StartCoroutine("Dead");
                }
            }
            else if(isStart)
            {
                //Start Battle Coroutine
                isStart = false;
                StartCoroutine("Battle");
            }
        }
    }

    IEnumerator Battle()
    {
        //reperat forever
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsRoaring", true);
            yield return new WaitForSeconds(3.0f);
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsRoaring", false);

            for (int i = 0; i < 110; i++)
            {
                transform.Translate(-0.2f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.5f);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 110; i++)
            {
                transform.Translate(-0.2f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.5f);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            yield return new WaitForSeconds(0.5f);
            
            for (int i = 0; i < 40; i++)
            {
                transform.Translate(0f, -0.2f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.5f);
            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsDashing", true);
            for (int i = 0; i < 40; i++)
            {
                transform.Translate(-0.6f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            if (PainPrefab != null)
            {
                GameObject newPain = Instantiate(PainPrefab, new Vector3(transform.position.x - 0.5f, transform.position.y + 0.2f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                Destroy(newPain, 0.1f);
            }
            yield return new WaitForSeconds(0.1f);
            _animator.SetBool("IsPiyopiying", true);
            _animator.SetBool("IsDashing", false);
            yield return new WaitForSeconds(3.0f);
            _animator.SetBool("IsPiyopiying", false);
            _animator.SetBool("IsWaiting", true);
            yield return new WaitForSeconds(1.0f);
            transform.Translate(2f, 0f, 0f);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            for (int i = 0; i < 40; i++)
            {
                transform.Translate(0f, 0.2f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(1.0f);
            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsBreathing", true);
            yield return new WaitForSeconds(1.5f);
            if (FirePrefab != null)
            {
                GameObject newFire = Instantiate(FirePrefab, new Vector3(transform.position.x+7.8f, transform.position.y-6.6f, 0f), new Quaternion(0f, 180f, 0f, 0f));
                Destroy(newFire, 3.5f);
            }
            yield return new WaitForSeconds(3.5f);
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsBreathing", false);
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 20; i++)
            {
                transform.Translate(-0.275f, -0.4f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 20; i++)
            {
                transform.Translate(-0.275f, 0.4f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 20; i++)
            {
                transform.Translate(-0.275f, -0.4f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 20; i++)
            {
                transform.Translate(-0.275f, 0.4f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(1.0f);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        nowPosition = transform.position;
        for (int i = 0; i <= 30; i++)
        {
            transform.position += new Vector3((0f - nowPosition.x) / 30f, (2.6f - nowPosition.y) / 30f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 20; i >= 0; i--)
        {
            transform.localScale = new Vector3(i / 40f, i / 40f, i / 40f);
            yield return new WaitForSeconds(0.05f);
        }
        if(FramePrefab != null)
        {
            GameObject newFrame = Instantiate(FramePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0f, 0f, 0f, 0f));
            GameObject newLight = Instantiate(LightPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0f, 0f, 0f, 0f));
            newFrame.name = "FrameDebidora";
            newLight.name = "FrameLight";
        }
        yield return new WaitForSeconds(0.05f);
        gameObject.SetActive(false);

        BossBGM.BossFightBGM_Stop();
        exitDoor.OpenDoor();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Sword" && !isDead){
            HP.UpdateHP(ATK_player);
            nowhp = nowhp - ATK_player;
        }
    }

    ///<summary>
    /// ボス登場時 HPバーを表示する  BossCamera1フェーズ2で呼ぶ
    ///</summary>
    public void BossHPCountUp(){
        isEntered = true;
        bossCamera.isBossRoom = true;

        BossBGM.StageBGM_Stop();
        
        HP_canvas.gameObject.SetActive(true);
        HP.UpdateHP(maxhp - nowhp);
        StartCoroutine(BossHPStart());
    }

    private IEnumerator BossHPStart(){
        float countUpHP = nowhp;
        Player.RestrainedByEvent(); // プレイヤーの動きを固定

        GameManager.instance.PlaySE(RoarClip);

        while(nowhp < maxhp){   // nowhpはここで使うのでBossHPCountUpでも更新する
            HP.UpdateHP(-countUpHP);        
            nowhp = nowhp + countUpHP;
            yield return new WaitForSeconds(0.5f);
        }
        Player.UnRestrainedByEvent(); // プレイヤーを開放
        BossBGM.BossFightBGM_Start();
    }
}
