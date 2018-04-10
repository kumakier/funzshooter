using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip mainTheme;
	public AudioClip menuTheme;

	string SceneName;

	void  Start(){

		OnLevelWasLoaded (0);


	}

	void OnLevelWasLoaded( int sceneIndex){

		string newSceneName = SceneManager.GetActiveScene().name;

		if(newSceneName != SceneName){
			SceneName = newSceneName;
			Invoke("PlayMusic", .2f);

		}

	}


	void PlayMusic(){

		AudioClip clipToPlay = null;


		if(SceneName == "Menu"){
			clipToPlay = menuTheme;

		}else if(SceneName == "Game"){
			clipToPlay = mainTheme;
		}

		if(clipToPlay != null) {
			AudioManager.instance.PlayMusic(clipToPlay , 2);
			Invoke("PlayMusic" , clipToPlay.length);
		}

	}

}
