using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	IEnumerator routine;

	public int life;
	public int spel;

	public int count;

	public GameObject player;
	public GameObject bomb;
	public GameObject graze;

	Spaceship spaceship;
	AudioSource shotSound;
	AudioSource bombSound;
	AudioSource grazeSound;

	private Vector3 pos;
	private Vector3 WorldPointPos = new Vector3(0.0f,-3.0f,0.0f);

//	private Vector3 offset= new Vector3(0.0f,0.0f,0.0f);

	private Vector3 limit;

	public int Life () {
		return life;
	}

	public int Bomb () {
		return spel;
	}

	void Start() {
		spaceship = GetComponent<Spaceship> ();
		//shotSound = GetComponent<AudioSource> ();
		AudioSource[] audioSources = GetComponents<AudioSource>();
		shotSound = audioSources [0];
		bombSound = audioSources [1];
		routine = Shot ();
		StartCoroutine (routine);
	}

	void OnDisable () {
		StopCoroutine (routine);
	}

	void OnEnable () {
		if (routine != null) {
			StartCoroutine (routine);
		}
	}
	                                                                  
	IEnumerator Shot () {
		while (true) {
			if (Input.GetMouseButton (0) && 0 < spel ) {
				count++;
				bombSound.PlayOneShot(bombSound.clip);  
				spel--;
				for (int i = 0; i < transform.childCount; i++) {

					Transform shotPosition = transform.GetChild(i);

					// ShotPositionの位置/角度で弾を撃つ
					Bomb(shotPosition);
				}

				yield return new WaitForSeconds (1.0f);
			}
			spaceship.Shot (transform);
			shotSound.PlayOneShot(shotSound.clip);                                                                                                             			shotSound.PlayOneShot (shotSound.clip);
			yield return new WaitForSeconds (spaceship.shotDelay);
		}
	}

	void Bomb (Transform origin) {
		Instantiate (bomb, origin.position, origin.rotation);
	}

	void Update () {


		/*
		if (Input.GetMouseButton (0)) {
			//マウス位置座標をVector3で取得
			pos = Input.mousePosition;
			// マウス位置座標をスクリーン座標からワールド座標に変換する
			WorldPointPos = Camera.main.ScreenToWorldPoint (pos);

			if (Input.GetMouseButtonDown (0)) {
				offset = transform.position - WorldPointPos;
			}

			limit.x = WorldPointPos.x + offset.x;
			limit.y = WorldPointPos.y + offset.y;

			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0.03125f, 0.05f));
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (0.59375f, 0.95f));

			limit.x = Mathf.Clamp (limit.x, min.x, max.x);
			limit.y = Mathf.Clamp (limit.y, min.y, max.y);

			limit.z = 0.0f;

			transform.position = limit;
		}
		*/

		//マウス位置座標をVector3で取得
		pos = Input.mousePosition;
		// マウス位置座標をスクリーン座標からワールド座標に変換する
		WorldPointPos = Camera.main.ScreenToWorldPoint (pos);
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0.03125f, 0.05f));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (0.59375f, 0.95f));
		WorldPointPos.x = Mathf.Clamp (WorldPointPos.x, min.x, max.x);
		WorldPointPos.y = Mathf.Clamp (WorldPointPos.y, min.y, max.y);

		WorldPointPos.z = 0.0f;

		transform.position = WorldPointPos;

	}

	void OnTriggerEnter2D (Collider2D c) {
		string layerName = LayerMask.LayerToName (c.gameObject.layer);
		if (layerName == "Bullet(Enemy)") {
			Destroy (c.gameObject);
		}
		if (layerName == "Bullet(Enemy)" || layerName == "Enemy") {
			life--;
			spaceship.Explosion ();
			EnemyBulletDestroy ();
			player.SetActive(false);
			graze.SetActive (false);
			Invoke ("Resurrection", 2.0f);
		}
	}

	void EnemyBulletDestroy () {
		GameObject[] enemybullets = GameObject.FindGameObjectsWithTag ("EnemyBullet");
		foreach (GameObject enemybullet in enemybullets) {
			Destroy (enemybullet);
		}
	}

	public void Resurrection () {
		player.SetActive (true);
		graze.SetActive (true);
		spel = 1;
	}
}
