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
            Debug.Log("‰½‚©‚ª”»’è‚É“ü‚è‚Ü‚µ‚½");
              isGroundEnter = true;
          }
     }

     private void OnTriggerStay2D(Collider2D collision)
     {
          if (collision.tag == groundTag)
          {
            Debug.Log("‰½‚©‚ª”»’è‚É“ü‚è‘±‚¯‚Ä‚¢‚Ü‚·");
              isGroundStay = true;
          }
     }

     private void OnTriggerExit2D(Collider2D collision)
     {
          if (collision.tag == groundTag)
          {
            Debug.Log("‰½‚©‚ª”»’è‚ð‚Å‚Ü‚µ‚½");
              isGroundExit = true;
          }
     }
}
