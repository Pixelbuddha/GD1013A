using UnityEngine;
using System.Collections;

public class ChangeMusic : MonoBehaviour {
	
	public AudioClip level2Music;
	
	
	private AudioSource source;
	
	
	// Use this for initialization
	void Awake () 
	{
		source = GetComponent<AudioSource>();
	}
	
	
	void OnLevelWasLoaded(int level)
	{

		if (level == 3)
		{

			source.clip = level2Music;
			source.Play ();
			source.volume = 0.5f;
			source.pitch = 0.9f;
		}
		
	}
}