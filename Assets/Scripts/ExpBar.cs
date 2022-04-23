using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
	public Slider slider;
	public Color color;
	public Image fill;
	public Text text;
	//public void SetMaxHealth(int exp)
	//{
	//	slider.maxValue = exp;
	//	slider.value = exp;

	//	fill.color = color;
	//}

	public void SetExp(int exp)
	{
		slider.value = exp;
	}
	public void SetLevel(int level)
	{
		text.text = level.ToString();
	}
}
