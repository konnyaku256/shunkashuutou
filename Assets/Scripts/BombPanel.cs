using UnityEngine;
using System.Collections;

public class BombPanel : MonoBehaviour {

	public GameObject[] icons;

	public void UpdateBomb (int spel) {
		for (int i = 0; i < icons.Length; i++) {
			if (i < spel)
				icons[i].SetActive (true);
			else
				icons[i].SetActive (false);
		}
	}			
}
