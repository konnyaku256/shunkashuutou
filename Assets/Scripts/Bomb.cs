using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public int speed = 5;
	public int power = 100;

	void Start () {
		GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
	}

	void OnTriggerEnter2D (Collider2D c) {
		string layerName = LayerMask.LayerToName (c.gameObject.layer);
		if (layerName == "Bullet(Enemy)") {
			Destroy (c.gameObject);
		}
	}
}
