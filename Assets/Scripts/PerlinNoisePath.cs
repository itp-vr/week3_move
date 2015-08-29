using UnityEngine;
using System;
using System.Collections;

public class PerlinNoisePath: MonoBehaviour {
	public float heightScale = 1.0f;
	public float increment = 0.1f;
	private float noisePoint = 0;
	public float xScale = 1.0f;
	public float speed = 0.1f;
	private float bufferSize = 200;
	private Queue buffer = new Queue();
	private int count = 0;
	private Vector3 initPos;
	private Vector3 lastPoint;
	public LineRenderer linerender;
	public bool induceNausea;

	// Use this for initialization
	void Start () {
		initPos = transform.position;
		//fill buffer
		float last = 0;
		for (int i = 0; i < bufferSize; i++){
			last = getNextNoise();
			buffer.Enqueue(last);
		}
	}

	private void resetRotation(){
		transform.localEulerAngles = Vector3.zero;
	}

	public Vector3 GetLastPoint(){
		return lastPoint;
	}

	private float getNextNoise() {
		float noise =  heightScale * Mathf.PerlinNoise(noisePoint * xScale, 0.0f);
		noisePoint += increment;
		return noise;
	}

	void Update() {
		//take the first point and add the last point
		float height = (float) buffer.Dequeue();
		buffer.Enqueue(getNextNoise());
		Vector3 pos = transform.position;
		pos.y = initPos.y + height;
		pos.z += speed;
		transform.position = pos;
		//orient toward next point
		float nextHeight = (float) buffer.Peek();
		Vector3 nextPos = new Vector3(pos.x, initPos.y + nextHeight, pos.z+speed);
		//angle camera to direction of hill (vomit city)
		if (induceNausea) transform.LookAt(nextPos);
		//draw debug path
		Vector3 start = pos;
		System.Object[] bufArray = buffer.ToArray();
		if (linerender != null)linerender.SetVertexCount(bufArray.Length);
		for (int i = 0; i < bufArray.Length ; i++){
			float y = (float) bufArray[i];
			Vector3 end = new Vector3 (start.x, initPos.y + y, start.z+speed);
			if (linerender != null && linerender.enabled){
				linerender.SetPosition(i, end+new Vector3(0,-0.5f,0));
			}
			Debug.DrawLine(start, end);
			start = end;
			if (i == bufArray.Length-1) lastPoint = end;
		}
		count++;
	}
}
