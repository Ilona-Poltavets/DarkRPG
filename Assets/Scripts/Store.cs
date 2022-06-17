using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Store : MonoBehaviour
{
	[SerializeField] private GameObject uiStore;
	private bool onShop = false;
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
            if (Keyboard.current[Key.E].wasPressedThisFrame)
            {
				Pause();
            }
		}
	}
	//public void Resume()
	//{
	//	uiStore.gameObject.SetActive(false);
	//	onShop = false;
	//	Time.timeScale = 1f;
	//}
	void Pause()
	{
		uiStore.gameObject.SetActive(true);
		onShop = true;
		Time.timeScale = 0f;
		MenuPause.GamePause();
	}
}
