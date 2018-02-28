using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour {


	public GameObject flashHolder;
	public Sprite[] flashSprite;
	public SpriteRenderer[] spriteRenderer; 



	public float flashTime;

	// Use this for initialization
	void Start () {
	
		Deactivate();


	}

	public void Activate() {

		flashHolder.SetActive(true);

		int flashSpriteIndex = Random.Range (0 , flashSprite.Length);
		for(int i = 0; i < spriteRenderer.Length; i++){
			spriteRenderer[i].sprite = flashSprite[flashSpriteIndex];


		}


		Invoke("Deactivate" , flashTime);
	}


	
	// Update is called once per frame
	void Deactivate () {
		flashHolder.SetActive(false);

	}
}
