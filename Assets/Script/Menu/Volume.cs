using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Volume : MonoBehaviour
{
	public static float nowVolume = 1;
	private AudioSource audioSource;
	private Slider audioSlider;

	private void Start()
	{
		audioSource = GameObject.Find("BGM").GetComponent<AudioSource>();
		audioSlider = GameObject.Find("Volume").GetComponent<Slider>();

        audioSource.volume = nowVolume;
		audioSlider.value = nowVolume;
	}

	public void ChangeVolume()
	{
        audioSource.volume = audioSlider.value;
		nowVolume = audioSlider.value;
    }

}
