using UnityEngine;
using System.Collections;

public class PictureGenerator : MonoBehaviour {

	public PerlinNoisePath noisePath;
	public int frequency = 100;
	public float xOffset = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount % frequency == 0){
			//generate pic
			Vector3 pos = noisePath.GetLastPoint();
			float rnd = Random.value;
			if (rnd > 0.5) xOffset = xOffset * -1;
			pos += new Vector3 (xOffset,0,0);
			Sprite[] pics = Resources.LoadAll<Sprite>("pics");
			int pick = Random.Range(0, pics.Length);
			GameObject pic = (GameObject)Instantiate(Resources.Load("pic"), pos, Quaternion.identity);
			pic.GetComponent<SpriteRenderer>().sprite = pics[pick];
			pic.GetComponent<FaceObject>().targetObj = Camera.main.transform;

		}
	}
}
