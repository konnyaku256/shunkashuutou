using UnityEngine;
using System.Collections;

public class Graze : MonoBehaviour {

	public int graze;

	public Player player;

	AudioSource grazeSound;

	public int Grazer () {
		return graze;
	}

	void Start () {
		grazeSound = GetComponent<AudioSource> ();
	}

	void Update () {
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, 0);
	}

	void OnTriggerEnter2D (Collider2D c) {
		string layerName = LayerMask.LayerToName (c.gameObject.layer);
		if (layerName == "Bullet(Enemy)" || layerName == "Enemy") {
			graze++;
			grazeSound.Play ();
		}
	}
}
