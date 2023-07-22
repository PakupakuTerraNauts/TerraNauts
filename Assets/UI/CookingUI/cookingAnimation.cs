using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cookingAnimation : MonoBehaviour
{
    private GameObject _animationObj;
    private GameObject _textObj;
    private Animator animator1;
    private Animator animator2;
    private bool cook;
    private GameObject _animationToggleObj;
    private Toggle animationToggle;

    private GameObject _animationBack;
         

    // Start is called before the first frame update
    void Start()
    {
        _animationObj = GameObject.Find("CookingAnimation");
        _textObj = GameObject.Find("Complete");
        animator1 = _animationObj.GetComponent<Animator>();
        animator2 = _textObj.GetComponent<Animator>();
        _animationObj.SetActive(false);
        _textObj.SetActive(false);

        _animationToggleObj = GameObject.Find("AnimationToggle");
        animationToggle = _animationToggleObj.GetComponent<Toggle>();


        _animationBack = GameObject.Find("AnimationBack");
        _animationBack.SetActive(false);
  
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PushButton()
    {
        if(PushCookButton.cookOK == true && animationToggle.isOn == true)
        {
            _animationObj.SetActive(true);
            _animationBack.SetActive(true);
            animator1.SetBool("push", true);
            StartCoroutine(Textstart());
        }

        
    }

    IEnumerator Textstart()
    {
        yield return new WaitForSeconds(2);
        _textObj.SetActive(true);
        animator2.SetBool("en", true);
        yield return new WaitForSeconds(3);
        _animationObj.SetActive(false);
        _textObj.SetActive(false);
        _animationBack.SetActive(false);

        yield break;
    }
}
