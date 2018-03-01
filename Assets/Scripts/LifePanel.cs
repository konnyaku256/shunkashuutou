using UnityEngine;
using System.Collections;

public class LifePanel : MonoBehaviour {

	public GameObject[] icons;

	public void UpdateLife (int life) {
		for (int i = 0; i < icons.Length; i++) {
			if (i < life - 1)
				icons[i].SetActive (true);
			else
				icons[i].SetActive (false);
		}
	}			
}