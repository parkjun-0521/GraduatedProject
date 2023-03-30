using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassMove : MonoBehaviour {

	void Update ()
	{
		transform.position += transform.forward * Time.deltaTime * 4;
		transform.Rotate(Vector3.up * Time.deltaTime * 64);
	}
}
