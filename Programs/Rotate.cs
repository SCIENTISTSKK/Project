using UnityEngine;

public class Rotate : MonoBehaviour 
{
	public int rpm;

	void Update () 
	{
		transform.Rotate (0f,0f,6f*rpm* Time.deltaTime);
	}

}
