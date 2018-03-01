using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	public void OnStartButtonClicked () {
		SceneManager.LoadScene ("Main");
	}
	public void OnQuitButtonClicked () {
		Application.Quit();
	}
}
