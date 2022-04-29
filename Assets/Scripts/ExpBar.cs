using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
	public Slider slider;
	public Text text;

	public void SetExp(int exp)
	{
		slider.value = exp;
	}
	public void SetLevel(int level)
	{
		text.text = level.ToString();
	}
}
