using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour 
{

	public Calculation Maths;

	//************************************************************************    Camera     *************************************************************************

	public GameObject Cam;
	protected Transform Camera_X;
	protected Transform Parent_X;


	protected Vector3 Camera_LocalRotation;
	protected float Camera_Offset ;
	protected Vector3 Camera_LocalPosition;
	protected Quaternion Parent_LocalRotation;

	public float Mouse_Sensitivity = 2f;
	public float Scroll_Sensitivity = 4f;
	public float Orbit_Damping = 10f;
	public float Scroll_Damping = 6f;

	public bool Camera_Disabled = true; 

	void Start () 
	{
		Maths = GameObject.FindObjectOfType (typeof(Calculation))as Calculation;

		//************************************************************************    Camera     *************************************************************************
		Camera_Offset=Maths._Total_Head;
		Camera_X = Cam.transform;
		Parent_X = Cam.transform.parent;
		Parent_LocalRotation = Cam.transform.parent.localRotation;
		Camera_LocalPosition = Cam.transform.localPosition;

	}


	void LateUpdate () 
	{
		//************************************************************************    Camera     *************************************************************************

		if (Input.GetKeyDown (KeyCode.V))
			Camera_Disabled = !Camera_Disabled;

		if (Camera_Disabled) 
		{
			Maths.Cam.enabled = false;
		}


		if (!Camera_Disabled) 
		{
			if (Input.GetAxis ("Mouse X") != 0 || Input.GetAxis ("Mouse Y") != 0) 
			{
				Camera_LocalRotation.x += Input.GetAxis ("Mouse X") * Mouse_Sensitivity;
				Camera_LocalRotation.y -= Input.GetAxis ("Mouse Y") * Mouse_Sensitivity;
			}

			if (Input.GetAxis ("Mouse ScrollWheel") != 0f)
			{
				float Scroll_Amount = Input.GetAxis ("Mouse ScrollWheel") * Scroll_Sensitivity;
				Scroll_Amount *= (Camera_Offset * 0.3f);
				Camera_Offset += Scroll_Amount * -1f;
				Camera_Offset = Mathf.Clamp (Camera_Offset, 0f, Maths._Total_Head+100f);
			}

		}

		Quaternion QT = Quaternion.Euler (0,Camera_LocalRotation.x,Camera_LocalRotation.y);
		Parent_X.rotation = Quaternion.Lerp (Parent_X.rotation, QT, Time.deltaTime * Orbit_Damping);

		if (Camera_X.localPosition.x != Camera_Offset * -1f)
		{
			Camera_X.localPosition = new Vector3 (Mathf.Lerp (Camera_X.localPosition.x, Camera_Offset * -1f, Time.deltaTime * Scroll_Damping),Camera_X.localPosition.y,Camera_X.localPosition.z);

		}

	}


}
