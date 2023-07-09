using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debidora : MonoBehaviour
{
    [SerializeField] private GameObject DebidoraFire;
    [SerializeField] private GameObject DebidoraPain;
    private Animator _animator;
    public float hp = 50;
    public bool isStart = true;

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
            _animator.SetTrigger("Dead");
            StopCoroutine(Battle());
        }
        else if(isStart)
        {
            //Start Battle Coroutine
            isStart = false;
            StartCoroutine("Battle");
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
            if (DebidoraPain != null)
            {
                DebidoraPain.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);
            if (DebidoraPain != null)
            {
                DebidoraPain.SetActive(false);
            }
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
            if (DebidoraFire != null)
            {
                DebidoraFire.SetActive(true);
            }
            yield return new WaitForSeconds(3.5f);
            if (DebidoraFire != null)
            {
                DebidoraFire.SetActive(false);
            }
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsBreathing", false);
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 20; i++)
            {
                transform.Translate(-0.12f, -0.22f, 0f);
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
