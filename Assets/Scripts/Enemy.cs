using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	IEnumerator routine;

	public bool unit;

	public BulletManager[] bulletManagers;

	public BulletManagerUnit unit1;
	public BulletManagerUnit unit2;
	public BulletManagerUnit unit3;
	public BulletManagerUnit unit4;
	public BulletManagerUnit unit5;
	public BulletManagerUnit unit6;
	public BulletManagerUnit unit7;
	public BulletManagerUnit unit8;

	public GameObject enemy;

	public GameObject typeChange;

	public Slider slider;

	public int hp;
	public int type;

	Spaceship spaceship;

	float x;
	float y;

	public int changeSeason = 1;

	public int Hp () {
		return hp;
	}

	public int Type () {
		return type;
	}

	void Start () {
		spaceship = GetComponent<Spaceship> ();
		routine = Move ();
		StartCoroutine (routine);
	}

	IEnumerator Move () {
		while (true) {
			if (type == 1) {
				x = Random.Range (-4.0f, 4.0f);
				unit1.Shot ();
				yield return new WaitForSeconds (1.0f);
			}
			if (type == 2) {
				x = Random.Range (-4.0f, 4.0f);
				unit2.Shot ();
				yield return new WaitForSeconds (2.0f);
			}
			if (type == 3) {
				x = Random.Range (-4.0f, 4.0f);
				unit3.Shot ();
				yield return new WaitForSeconds (9.0f);
			}
			if (type == 4) {
				x = 0;
				unit4.Shot ();
				yield return new WaitForSeconds (3.0f);
			}
			if (type == 5) {
				if (changeSeason == 1) unit5.Shot ();
				if (changeSeason == 2) unit6.Shot ();
				if (changeSeason == 3) unit7.Shot ();
				if (changeSeason == 4) unit8.Shot ();
				changeSeason++;
				if (5 <= changeSeason) changeSeason = 1;
				yield return new WaitForSeconds (10.0f);
			}
		}
	
	}

	void OnDisable () {
		StopCoroutine (routine);
	}

	void OnEnable () {
		if (routine != null) {
			StartCoroutine (routine);
		}
	}

	void Update () {
		if (slider.value == 0) {
			slider.value = 2000;
		}
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(x,3.0f), spaceship.speed *Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D c) {
		string layerName = LayerMask.LayerToName (c.gameObject.layer);
		if (layerName == "Bullet(Player)") {

			Transform playerBulletTransform = c.transform.parent;
			PlayerBullet bullet = playerBulletTransform.GetComponent<PlayerBullet> ();

			FindObjectOfType<GameController> ().AddPoint (bullet.power);

			hp -= bullet.power;
			slider.value = hp % 2000;

			Destroy (c.gameObject);

			if (hp % 2000 == 0 && type != 5) {
				type++;
				Instantiate (typeChange, transform.position, transform.rotation);
				EnemyBulletDestroy ();
				enemy.SetActive (false);
				enemy.SetActive (true);
			}

			if (hp <= 0) {
				spaceship.Explosion ();
				enemy.SetActive (false);
			}
		}

		if (layerName == "Bomb") {
			Bomb bomb = c.transform.GetComponent<Bomb> ();

			FindObjectOfType<GameController> ().AddPoint (bomb.power);

			hp -= bomb.power;
			slider.value = hp % 2000;

			Destroy (c.gameObject);

			if (hp % 2000 == 0 && type != 5) {
				type++;
				Instantiate (typeChange, transform.position, transform.rotation);
				EnemyBulletDestroy ();
				enemy.SetActive (false);
				enemy.SetActive (true);
			}

			if (hp <= 0) {
				spaceship.Explosion ();
				enemy.SetActive (false);
			}
		}
	}

	void EnemyBulletDestroy () {
		GameObject[] enemybullets = GameObject.FindGameObjectsWithTag ("EnemyBullet");
		foreach (GameObject enemybullet in enemybullets) {
			Destroy (enemybullet);
		}
	}
}

