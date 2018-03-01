using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackColorChange : MonoBehaviour {

	public Image back;

	float H = 0.0f;

	void Update () {
		if (1.0f <= H)
			H = 0.0f;
		back.color = Color.HSVToRGB (H, 0.4f, 1.0f);
		H += 0.1f * Time.deltaTime;
	}

}