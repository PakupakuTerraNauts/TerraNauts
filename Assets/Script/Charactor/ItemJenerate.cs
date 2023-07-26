using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJenerate : MonoBehaviour
{
    public GameObject items;
    public GameObject rareItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            int number = Random.Range(1, 100);
            Instantiate(items, this.transform.position, Quaternion.identity);
            if (number <= 30)
            {
                Instantiate(rareItem, this.transform.position, Quaternion.identity);
            }
        }

    }
}
