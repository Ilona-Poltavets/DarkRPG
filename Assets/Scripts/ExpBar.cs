using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyProject
{
	/// <summary>
	/// Object class for drawing a slider on the UI that reacts to changes in the amount of player experience
	/// </summary>
	public class ExpBar : MonoBehaviour
	{
		/// <summary>
		/// Обьект UI слайдер 
		/// </summary>
		public Slider slider;
		/// <summary>
		/// UI object text field to display character level in it
		/// </summary>
		public Text text;
		/// <summary>
		/// Method for setting the received player experience value to the slider
		/// </summary>
		/// <param name="exp">Amount of experience</param>
		public void SetExp(int exp)
		{
			slider.value = exp;
		}
		/// <summary>
		/// Method for changing the text that displays the player's level
		/// </summary>
		/// <param name="level">Player Level</param>
		public void SetLevel(int level)
		{
			text.text = level.ToString();
		}
	}
}