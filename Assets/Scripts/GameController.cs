using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	enum State{
		Play,
		Result
	}

	State state;

	public Player player;
	public Enemy enemy;
	public Graze graze;

	public Text enemyTypeLabel;
	public Text spelName;

	public Text highScoreLabel;
	public Text scoreLabel;
	public Text grazeLabel;
	public LifePanel lifePanel;
	public BombPanel bombPanel;

	public Image resultPanel;

	public Text result;
	public Text resultScore;
	public Text resultClear;
	public Text resultGraze;
	public Text resultPlayer;
	public Text resultBomb;
	public Text resultTotal;
	public Text resultHighTotal;

	private int highScore;
	private int score;

	private int clearPoint;
	private int grazePoint;
	private int playerPoint;
	private int bombPoint;
	private int totalPoint;
	private int highTotalPoint;

	void Start () {
		score = 0;
		highScore = PlayerPrefs.GetInt ("HighScorePoint", 0);
		highTotalPoint = PlayerPrefs.GetInt ("HighTotalPoint", 0);
		state = State.Play;
	}

	void Update () {
		if (highScore < score) {
			highScore = score;
		}

		highScoreLabel.text = "" + highScore;
		scoreLabel.text = "" + score;
		grazeLabel.text = "" + graze.Grazer ();
		lifePanel.UpdateLife (player.Life());
		bombPanel.UpdateBomb (player.Bomb());

		enemyTypeLabel.text = "第" + enemy.Type () + "段階";
		if (enemy.Type() == 4 ) {
			spelName.text = "禁忌:百花繚乱";
		}
		if (enemy.Type() == 5 ) {
			spelName.text = "禁忌:春夏秋冬";
		}
	}

	void LateUpdate () {
		switch (state) {
		case State.Play:
			if (player.Life () <= 0 || enemy.Hp () <= 0) Result ();
				break;
			case State.Result:
				if (Input.GetButtonDown ("Fire1")) ReturnToTitle ();
				break;
		}
	}

	void Result () {
		Pauser.Pause ();
		state = State.Result;
		if (PlayerPrefs.GetInt ("HighScorePoint") < score) {
			PlayerPrefs.SetInt ("HighScorePoint", score);
		}
		grazePoint = graze.Grazer () * 10;
		if (0 < player.Life()) {
			clearPoint = 5000;
		}
		playerPoint = player.Life () * 1000;
		bombPoint = player.count * 1000;
		totalPoint = score + grazePoint + clearPoint + playerPoint + bombPoint;

		if (highTotalPoint < totalPoint) {
			highTotalPoint = totalPoint;
		}

		if (PlayerPrefs.GetInt ("HighTotalPoint") < totalPoint) {
			PlayerPrefs.SetInt ("HighTotalPoint", totalPoint);
		}

		resultPanel.gameObject.SetActive (true);
		resultScore.text = "" + score;
		resultClear.text = "" + clearPoint;
		resultGraze.text = "" + grazePoint;
		resultPlayer.text = "" + playerPoint;
		resultBomb.text = "" + bombPoint;
		resultTotal.text = "" + totalPoint;
		resultHighTotal.text = "" + highTotalPoint;
	}

	public void AddPoint (int point) {
		score = score + point;
	}

	void ReturnToTitle () {
		SceneManager.LoadScene ("Title");
	}
}
