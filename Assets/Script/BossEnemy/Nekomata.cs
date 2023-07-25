using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nekomata : MonoBehaviour
{
    [SerializeField] private GameObject SoulPrefab;
    [SerializeField] private GameObject FamilyPrefab;
    [SerializeField] private GameObject FamilyPrefab1;
    [SerializeField] private GameObject FamilyPrefab2;
    private Animator _animator;
    public float hp = 100;
    public bool isStart = true;
    public bool isDead = false;
    private Material _mat;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _mat = GetComponent<Renderer>().material;
        _animator.SetBool("IsWaiting", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            //if hp is 0, stop Battle Coroutine
            hp = 0;
            isDead = true;
            //_animator.SetTrigger("Dead");
            StopCoroutine("Battle");
            Destroy(gameObject, 5f);
        }
        else if (isStart)
        {
            //Start Battle Coroutine
            isStart = false;
            StartCoroutine("Battle");
        }
        if (Input.GetKeyDown(KeyCode.Space) && isDead == false)
        {
            hp -= 10;
        }

    }

    IEnumerator Battle()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsRoaring", true);
            yield return new WaitForSeconds(3.0f);
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsRoaring", false);

            for (int i = 0; i < 70; i++)
            {
                transform.Translate(-0.1f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(2.0f);
            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsRoaring", true);
            yield return new WaitForSeconds(1.0f);
            if (SoulPrefab != null)
            {
                GameObject newSoul = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul.transform.position = new Vector3(-2f, 2.5f);
                yield return new WaitForSeconds(0.5f);
                GameObject newSoul1 = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul1.transform.position = new Vector3(2f, 2.5f);
                yield return new WaitForSeconds(0.5f);
                GameObject newSoul2 = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul2.transform.position = new Vector3(-4f, 2.5f);
                yield return new WaitForSeconds(0.5f);
                GameObject newSoul3 = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul3.transform.position = new Vector3(4f, 2.5f);
                yield return new WaitForSeconds(0.5f);
            }
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsRoaring", false);
            yield return new WaitForSeconds(4f);

            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsAuring", true);
            _mat.color= new Color(0.7f, 0.95f, 1f, 1f);
            yield return new WaitForSeconds(5f);
            for (int i = 0; i < 60; i++)
            {
                transform.Translate(0f, 0.1f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            transform.position = new Vector3(11f, -2.5f);
            for (int i = 0; i < 120; i++)
            {
                transform.Translate(-0.2f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            transform.position = new Vector3(7.5f, 8f);
            transform.eulerAngles = new Vector3(0f, 0f, 135f);
            for (int i = 0; i < 120; i++)
            {
                transform.Translate(0f, 0.2f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            transform.position = new Vector3(-5f, 8f);
            transform.eulerAngles = new Vector3(0f, 0f, 180f);
            for (int i = 0; i < 80; i++)
            {
                transform.Translate(0f, 0.2f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            transform.position = new Vector3(7f, -7.5f);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            for (int i = 0; i < 80; i++)
            {
                transform.Translate(0f, 0.2f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            transform.position = new Vector3(0f, 7f);
            for (int i = 0; i < 45; i++)
            {
                transform.Translate(0f, -0.1f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(2f);
            _mat.color = new Color(1f, 1f, 1f, 1f);
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsAuring", false);
            yield return new WaitForSeconds(2f);

            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsRoaring", true);
            yield return new WaitForSeconds(1.0f);
            if (FamilyPrefab != null && FamilyPrefab1 != null && FamilyPrefab2 != null)
            {
                GameObject newFamily = Instantiate(FamilyPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newFamily.transform.position = new Vector3(-2f, 2.5f);
                yield return new WaitForSeconds(0.5f);
                GameObject newFamily1 = Instantiate(FamilyPrefab1, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newFamily1.transform.position = new Vector3(0f, 0f);
                yield return new WaitForSeconds(0.5f);
                GameObject newFamily2 = Instantiate(FamilyPrefab2, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newFamily2.transform.position = new Vector3(2f, 2.5f);
                yield return new WaitForSeconds(1.5f);
            }
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsRoaring", false);
            yield return new WaitForSeconds(0.01f);
            _mat.color = new Color(0.7f, 0.95f, 1f, 1f);
            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsAuring", true);
            for (int i = 0; i < 40; i++)
            {
                transform.Translate(0f, 0.2f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(30f);
            for (int i = 0; i < 40; i++)
            {
                transform.Translate(0f, -0.2f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(2f);
            _mat.color = new Color(1f, 1f, 1f, 1f);
            _animator.SetBool("IsAuring", false);
            _animator.SetBool("IsWaiting", true);
            yield return new WaitForSeconds(0.01f);
            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsRoaring", true);
            yield return new WaitForSeconds(1.0f);
            if (SoulPrefab != null)
            {
                GameObject newSoul = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul.transform.position = new Vector3(-2f, 2.5f);
                yield return new WaitForSeconds(0.5f);
                GameObject newSoul1 = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul1.transform.position = new Vector3(2f, 2.5f);
                yield return new WaitForSeconds(0.5f);
            }
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsRoaring", false);
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 28; i++)
            {
                transform.Translate(-0.25f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            transform.eulerAngles = new Vector3(0f, 180f, 0f);

            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsRoaring", true);
            yield return new WaitForSeconds(1.0f);
            if (SoulPrefab != null)
            {
                GameObject newSoul2 = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul2.transform.position = new Vector3(-7f, 0f);
                yield return new WaitForSeconds(0.5f);
                GameObject newSoul3 = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul3.transform.position = new Vector3(-5f, 2.5f);
                yield return new WaitForSeconds(0.5f);
            }
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsRoaring", false);
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 28; i++)
            {
                transform.Translate(-0.25f, -0.15f, 0f);
                yield return new WaitForSeconds(0.05f);
            }

            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsRoaring", true);
            yield return new WaitForSeconds(1.0f);
            if (SoulPrefab != null)
            {
                GameObject newSoul4 = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul4.transform.position = new Vector3(-2f, -1.7f);
                yield return new WaitForSeconds(0.5f);
                GameObject newSoul5 = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul5.transform.position = new Vector3(2f, -1.7f);
                yield return new WaitForSeconds(0.5f);
            }
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsRoaring", false);
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 28; i++)
            {
                transform.Translate(-0.25f, 0.15f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            transform.eulerAngles = new Vector3(0f, 0f, 0f);

            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsRoaring", true);
            yield return new WaitForSeconds(1.0f);
            if (SoulPrefab != null)
            {
                GameObject newSoul2 = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul2.transform.position = new Vector3(7f, 0f);
                yield return new WaitForSeconds(0.5f);
                GameObject newSoul3 = Instantiate(SoulPrefab, new Vector3(-5.8f, -1.41f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                newSoul3.transform.position = new Vector3(5f, 2.5f);
                yield return new WaitForSeconds(0.5f);
            }
            _animator.SetBool("IsWaiting", true);
            _animator.SetBool("IsRoaring", false);
            yield return new WaitForSeconds(4f);
        }
    }
}
