using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyNekomata : MonoBehaviour
{
    public int num;
    private void Start()
    {
        StartCoroutine("MoveFamily");
    }
    IEnumerator MoveFamily()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.localScale = new Vector3(i / 40f, i / 40f, i / 40f);
            yield return new WaitForSeconds(0.05f);
        }
        switch(num) {
            case 1:
                    yield return new WaitForSeconds(2.0f);
                    break;
            case 2:
                    yield return new WaitForSeconds(1.5f);
                    break;
            default:
                yield return new WaitForSeconds(2.5f);
                break;
        }
        for (int i = 0; i < 40; i++)
        {
            transform.Translate(0f, 0.2f, 0f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2.5f);
        switch (num)
        {
            case 1:
                transform.position = new Vector3(11f, -2.5f);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                for (int i = 0; i < 120; i++)
                {
                    transform.Translate(0.2f, 0f, 0f);
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
                for (int i = 0; i < 70; i++)
                {
                    transform.Translate(0f, -0.1f, 0f);
                    yield return new WaitForSeconds(0.05f);
                }
                break;
            case 2:
                transform.position = new Vector3(-11f, 0f);
                for (int i = 0; i < 120; i++)
                {
                    transform.Translate(0.2f, 0f, 0f);
                    yield return new WaitForSeconds(0.05f);
                }
                transform.position = new Vector3(-7.5f, 8f);
                transform.eulerAngles = new Vector3(0f, 0f, 225f);
                for (int i = 0; i < 120; i++)
                {
                    transform.Translate(0f, 0.2f, 0f);
                    yield return new WaitForSeconds(0.05f);
                }
                transform.position = new Vector3(5f, -8f);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                for (int i = 0; i < 80; i++)
                {
                    transform.Translate(0f, 0.2f, 0f);
                    yield return new WaitForSeconds(0.05f);
                }
                transform.position = new Vector3(-7f, 7.5f);
                transform.eulerAngles = new Vector3(0f, 0f, 180f);
                for (int i = 0; i < 80; i++)
                {
                    transform.Translate(0f, 0.2f, 0f);
                    yield return new WaitForSeconds(0.05f);
                }
                transform.position = new Vector3(2f, 7f);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                yield return new WaitForSeconds(1.25f);
                for (int i = 0; i < 45; i++)
                {
                    transform.Translate(0f, -0.1f, 0f);
                    yield return new WaitForSeconds(0.05f);
                }
                break;
            default:
                transform.position = new Vector3(0f, -8f);
                for (int i = 0; i < 120; i++)
                {
                    transform.Translate(0f, 0.2f, 0f);
                    yield return new WaitForSeconds(0.05f);
                }
                transform.position = new Vector3(-11f, 0f);
                for (int i = 0; i < 120; i++)
                {
                    transform.Translate(0.2f, 0f, 0f);
                    yield return new WaitForSeconds(0.05f);
                }
                transform.position = new Vector3(0f, 7f);
                transform.eulerAngles = new Vector3(0f, 0f, 135f);
                for (int i = 0; i < 80; i++)
                {
                    transform.Translate(0f, 0.2f, 0f);
                    yield return new WaitForSeconds(0.05f);
                }
                transform.position = new Vector3(0f, -7f);
                transform.eulerAngles = new Vector3(0f, 0f, 315f);
                for (int i = 0; i < 80; i++)
                {
                    transform.Translate(0f, 0.2f, 0f);
                    yield return new WaitForSeconds(0.05f);
                }
                transform.position = new Vector3(-2f, 7f);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                yield return new WaitForSeconds(1.25f);
                for (int i = 0; i < 45; i++)
                {
                    transform.Translate(0f, -0.1f, 0f);
                    yield return new WaitForSeconds(0.05f);
                }
                break;
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 10; i > 0; i--)
        {
            transform.localScale = new Vector3(i / 40f, i / 40f, i / 40f);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
}
