using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace MyProject
{
	/// <summary>
	/// Class for displaying the health of the player in the interface
	/// </summary>
	public class HealthBar : MonoBehaviour
	{
		/// <summary>
		/// Slider UI object that displays the player's health
		/// </summary>
		public Slider slider;
		/// <summary>
		/// Color gradient to change the color of the health bar
		/// </summary>
		public Gradient gradient;
		/// <summary>
		/// Fill image
		/// </summary>
		public Image fill;
		/// <summary>
		/// Setting the maximum value of health and, depending on its amount, changes colors according to a given gradient
		/// </summary>
		/// <param name="health">max health</param>
		public void SetMaxHealth(int health)
		{
			slider.maxValue = health;
			slider.value = health;
			fill.color = gradient.Evaluate(slider.normalizedValue);
		}
		/// <summary>
		/// Setting current health
		/// </summary>
		/// <param name="health">Current health</param>
		public void SetHealth(int health)
		{
			slider.value = health;
			fill.color = gradient.Evaluate(slider.normalizedValue);
		}
	}
}