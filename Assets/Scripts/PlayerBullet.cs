using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	public int speed = 10;
	public int power = 1;

	void Start () {
		GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
	}
}
