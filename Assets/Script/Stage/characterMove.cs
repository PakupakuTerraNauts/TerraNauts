using UnityEngine;

public class chara_move : MonoBehaviour
{
     private float speed = 0.01f; //floaÇÕè¨êîì_

    void Update()
    {
        Vector2 pos = transform.position;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed;//âEÇ…à⁄ìÆ
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= speed;
        }

        transform.position = pos;
    }
}