using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{

    private string groundTag = "ground";
    private bool isGround = false;
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
            Debug.Log("何かが判定に入りました");
              isGroundEnter = true;
          }
     }

     private void OnTriggerStay2D(Collider2D collision)
     {
          if (collision.tag == groundTag)
          {
            Debug.Log("何かが判定に入り続けています");
              isGroundStay = true;
          }
     }

     private void OnTriggerExit2D(Collider2D collision)
     {
          if (collision.tag == groundTag)
          {
            Debug.Log("何かが判定をでました");
              isGroundExit = true;
          }
     }
}
