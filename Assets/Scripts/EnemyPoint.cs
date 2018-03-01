using UnityEngine;
using System.Collections;

public class EnemyPoint : MonoBehaviour {

	public Enemy enemy;

	void Update () {
		transform.position = new Vector3 (enemy.transform.position.x, -4.7f, 0);
	}
}
