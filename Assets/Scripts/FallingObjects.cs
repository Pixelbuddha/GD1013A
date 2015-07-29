using UnityEngine;
using System.Collections;

public class FallingObjects : MonoBehaviour 
{
	public float minSpawnTime = 0.7f; 
	public float maxSpawnTime = 1.7f;
	public float minX = -5.5f;
	public float maxX = +5.5f;
	public float topY = 4.5f;
	public float z = 0.0f;
	public int count = 200;
	public GameObject prefab;
	
	public bool doSpawn = true;
	
	void Start() {

		StartCoroutine(Spawner());

	}
	
	IEnumerator Spawner() {
		while (doSpawn && count > 0) {
			Vector3 v = new Vector3(Random.Range (minX, maxX), topY, z);
			GameObject go = Instantiate(prefab, v + transform.localPosition, Random.rotation) as GameObject;
			count--;
			yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
			Destroy(go, 0.5f);
		}
	}
	
}

