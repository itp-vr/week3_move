using UnityEngine;
using System.Collections;

public class AutoKill : MonoBehaviour {
	public float maxDist = 50;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
		if (dist > maxDist) Destroy(gameObject);
	}
}
