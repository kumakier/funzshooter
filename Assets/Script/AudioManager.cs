using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public enum AudioChannel {Master , Sfx, Music};

	public float masterVolumePercent {get; private set; }
	public float sfxVolumePercent  {get; private set; }
	public float musicVolumePercent  {get; private set; }

	AudioSource sfx2Dsource;
	AudioSource[] musicSource;
	int activeMusicSourceIndex;

	public static AudioManager instance;

	Transform audioListerner;
	Transform playerT;

	SoundLibary Library;

	void Awake(){

		if(instance != null){

			Destroy(gameObject);
		}
		else{
		instance = this;
		DontDestroyOnLoad(gameObject);

		Library = GetComponent<SoundLibary>();

		musicSource = new AudioSource[2];
		for(int i = 0; i < 2; i++){
			GameObject newMusicSource = new GameObject ("Music source" + (i + 1));
			musicSource[i] = newMusicSource.AddComponent<AudioSource>();
			newMusicSource.transform.parent = transform;
		}
				

			GameObject newsfx2Dsource = new GameObject ("2D sfx source");
			sfx2Dsource = newsfx2Dsource .AddComponent<AudioSource>();
			newsfx2Dsource.transform.parent = transform;
		
			audioListerner = FindObjectOfType<AudioListener>().transform;
			if(FindObjectOfType<Player>() != null){
				
			playerT = FindObjectOfType<Player>().transform;
			}
			masterVolumePercent = PlayerPrefs.GetFloat("master vol" , 1);
			sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol" ,  1);
			musicVolumePercent = PlayerPrefs.GetFloat("music vol" , 1 );
		}




	}

	void Update(){

		if(playerT != null){
			audioListerner.position = playerT.position;
		}

	}

	public void SetVolume(float volumePercent, AudioChannel channel){

		switch(channel){

		case AudioChannel.Master:
			masterVolumePercent = volumePercent;
			break;
		case AudioChannel.Sfx:
			sfxVolumePercent = volumePercent;
			break;
		case AudioChannel.Music:
			musicVolumePercent = volumePercent;
			break;
	}

		musicSource[0].volume = musicVolumePercent * masterVolumePercent;
		musicSource[1].volume = musicVolumePercent * masterVolumePercent;	

		PlayerPrefs.SetFloat("master vol" , masterVolumePercent);
		PlayerPrefs.SetFloat("sfx vol" , sfxVolumePercent);
		PlayerPrefs.SetFloat("music vol" , musicVolumePercent);
		PlayerPrefs.Save();
}

	public void PlayMusic(AudioClip clip, float fadeDuration = 1){

		activeMusicSourceIndex = 1  -  activeMusicSourceIndex;
		musicSource[activeMusicSourceIndex].clip = clip;
		musicSource[activeMusicSourceIndex].Play();


		StartCoroutine(AnimateMusicCrossfade(fadeDuration));
	}


	public void PlaySound(AudioClip clip, Vector3 pos) {
		if(clip != null){
		AudioSource.PlayClipAtPoint(clip , pos, sfxVolumePercent * masterVolumePercent);

		}
	}

	public void PlaySound(string soundName, Vector3 pos){

		PlaySound(Library.GetClipFromName(soundName), pos);
	}

	public void PlaySound2D(string soundName) {
		sfx2Dsource.PlayOneShot (Library.GetClipFromName (soundName), sfxVolumePercent * masterVolumePercent);
	}

	void OnLevelWasLoaded(int index) {
		if (playerT == null) {
			if (FindObjectOfType<Player> () != null) {
				playerT = FindObjectOfType<Player> ().transform;
			}
		}
	}
	IEnumerator AnimateMusicCrossfade(float duration){

		float percent = 0;

		while(percent < 1){

			percent += Time.deltaTime * 1 / duration;
			musicSource[activeMusicSourceIndex].volume = Mathf.Lerp(0,musicVolumePercent * masterVolumePercent,percent);
			musicSource[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent,0 ,percent);
			yield return null;

	}
}
}