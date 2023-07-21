using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulNekomata : MonoBehaviour
{
    private Vector3 _targetPosition;
    private Vector3 nowPosition;

    private void Start()
    {
        StartCoroutine("MoveSoul");
        nowPosition = transform.position;
    }
    IEnumerator MoveSoul()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.localScale = new Vector3(i/40f, i/40f, i/40f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2.5f);
        _targetPosition = GameObject.Find("PlayerBossScene").transform.position;
        for (int i = 0; i < 40; i++)
        {
            transform.position += new Vector3((_targetPosition.x - nowPosition.x) / 15f, (_targetPosition.y - nowPosition.y) / 15f);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
}
