using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debidora : MonoBehaviour
{
    [SerializeField] private GameObject FirePrefab;
    [SerializeField] private GameObject PainPrefab;
    private Animator _animator;
    public float hp = 50;
    public bool isStart = true;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("IsWaiting", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            //if hp is 0, stop Battle Coroutine
            hp = 0;
            isDead = true;
            _animator.SetTrigger("Dead");
            StopCoroutine("Battle");
            Destroy(gameObject, 5f);
        }
        else if(isStart)
        {
            //Start Battle Coroutine
            isStart = false;
            StartCoroutine("Battle");
        }
        if(Input.GetKeyDown(KeyCode.Space) && isDead == false)
        {
            hp -= 10;
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
                transform.Translate(-0.1f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.5f);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 110; i++)
            {
                transform.Translate(-0.1f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.5f);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 45; i++)
            {
                transform.Translate(0f, -0.1f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.5f);
            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsDashing", true);
            for (int i = 0; i < 40; i++)
            {
                transform.Translate(-0.3f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            if (PainPrefab != null)
            {
                GameObject newPain = Instantiate(PainPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                Destroy(newPain, 0.1f);
            }
            yield return new WaitForSeconds(0.1f);
            _animator.SetBool("IsPiyopiying", true);
            _animator.SetBool("IsDashing", false);
            yield return new WaitForSeconds(3.0f);
            _animator.SetBool("IsPiyopiying", false);
            _animator.SetBool("IsWaiting", true);
            yield return new WaitForSeconds(1.0f);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            for (int i = 0; i < 45; i++)
            {
                transform.Translate(0f, 0.1f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(1.0f);
            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsBreathing", true);
            yield return new WaitForSeconds(1.5f);
            if (FirePrefab != null)
            {
                GameObject newFire = Instantiate(FirePrefab, new Vector3(-2.05f, -0.92f, 0f), new Quaternion(0f, 180f, 0f, 0f));
                Destroy(newFire, 3.5f);
            }
            yield return new WaitForSeconds(3.5f);
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsBreathing", false);
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 20; i++)
            {
                transform.Translate(-0.15f, -0.22f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 20; i++)
            {
                transform.Translate(-0.15f, 0.22f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 20; i++)
            {
                transform.Translate(-0.15f, -0.22f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 20; i++)
            {
                transform.Translate(-0.15f, 0.22f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(1.0f);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            yield return new WaitForSeconds(3.0f);
        }
    }

}
