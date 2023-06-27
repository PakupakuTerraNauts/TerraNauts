using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
<<<<<<< HEAD
    private string groundTag = "ground";
    private bool isGround;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    public bool IsGround()
     {    
          if (isGroundEnter || isGroundStay)
          {
              isGround = true;
          }
          else if (isGroundExit)
          {
              isGround = false;
          }

          isGroundEnter = false;
          isGroundStay = false;
          isGroundExit = false;
          return isGround;
     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.tag == groundTag)
          {
              isGroundEnter = true;
          }
     }

     private void OnTriggerStay2D(Collider2D collision)
     {
          if (collision.tag == groundTag)
          {
              isGroundStay = true;
          }
     }

     private void OnTriggerExit2D(Collider2D collision)
     {
          if (collision.tag == groundTag)
          {
              isGroundExit = true;
          }
     }
=======
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
>>>>>>> 75a25fae69896b08d2dadff28967600e838859a5
}
