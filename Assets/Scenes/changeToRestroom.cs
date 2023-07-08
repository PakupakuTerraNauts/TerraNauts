using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change : MonoBehaviour 
{
 
    private void OnTriggerStay2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey (KeyCode.UpArrow))
            {
                SceneManager.LoadScene("restroom");
            }
        }   
    }
}