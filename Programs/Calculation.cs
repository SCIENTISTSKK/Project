using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculation : MonoBehaviour 
{
	//************************************************************************    Reference Datas     *************************************************************************

	public float _Ref_Discharge;
	public float _Ref_Impeller_Dia;
	public float _Ref_Head;
	public float _Ref_Power;

	//************************************************************************    Warning Datas     *************************************************************************

	public float _Max_Head;
	public float _Max_Power;
	public float _Acc_Discharge;
	public float _Acc_Impeller_Dia;
	public float _Acc_Power;
	public Canvas _Warning;

	//************************************************************************    Unit Conversion     *************************************************************************

	 float _Lps_Gpm = 15.850323f;
	 float _Gpm_Lps = 0.063090196667f;
	 float _Ft_mm = 304.8f;
	 float _mm_Ft = 0.0032808399f;

	//************************************************************************    Normal Datas     *************************************************************************

	public Dropdown _Unit_SH;
	public Dropdown _Unit_DH;
	public Dropdown _Unit_Q;

	public Text _Details;

	public float _Mfactor;
	public float _SH_Scale_Factor;
	public float _DH_Scale_Factor;
	public float _SH_Radius;
	public float _DH_Radius;
	public float _Impeller_Dia;
	public float _SH_Input_Value;
	public float _DH_Input_Value;
	public float _Total_Head;
	public float _Discharge_Input_Value;
	public float _WHP;
	public float _Max_WHP;
	public float _Eff;
	public float _Max_Eff;

	public Vector3 _FullPump_NewScale;
	public Vector3 _SH_OldScale;
	public Vector3 _SH_NewScale;
	public Vector3 _DH_OldScale;
	public Vector3 _DH_NewScale;
	public Vector3 _DH_OldPosition;
	public Vector3 _DH_Lbend_OldPosition;
	public Vector3 _DH_Lbend_NewPosition;

	public GameObject _DH_Pipe;
	public GameObject _SH_Pipe;
	public GameObject _Graph;
	public GameObject _DH_Lbend;
	public GameObject _Full_Pump;
	public GameObject _Camera;

	public InputField _DH_Input;
	public InputField _SH_Input;
	public InputField _Discharge_Input;

	public Canvas _Optimize;
	public Canvas _Simulation;

	public Rotate Rotor;
	public Camera_Control Cam;

	//************************************************************************    Graph Datas     *************************************************************************

	public Dropdown _Curves;

	public Text _Scale;

	public float _Min_X;
	public float _Max_X;
	public float _Diff_X;
	public float _Min_0;
	public float _Max_0;
	public float _Diff_0;
	public float _Min_1;
	public float _Max_1;
	public float _Diff_1;
	public float _Min_2;
	public float _Max_2;
	public float _Diff_2;
	public float _Diff_3;
	public float _Min_3;
	public float _Max_3;
	public float _Diff_4;
	public float _Min_4;
	public float _Max_4;

	public TextMesh _X_Lable;
	public TextMesh _Y0_Lable;
	public TextMesh _Y1_Lable;
	public TextMesh _Y2_Lable;

	public TextMesh _Data00;
	public TextMesh _Data01;
	public TextMesh _Data02;
	public TextMesh _Data002;
	public TextMesh _Data1;
	public TextMesh _Data2;
	public TextMesh _Data3;
	public TextMesh _Data4;
	public TextMesh _Data5;
	public TextMesh _Data6;
	public TextMesh _Data7;
	public TextMesh _Data8;
	public TextMesh _Data9;
	public TextMesh _Data10;
	public TextMesh _Data11;
	public TextMesh _Data12;
	public TextMesh _Data13;
	public TextMesh _Data14;
	public TextMesh _Data15;
	public TextMesh _Data16;
	public TextMesh _Data17;
	public TextMesh _Data18;
	public TextMesh _Data19;
	public TextMesh _Data20;
	public TextMesh _Data21;
	public TextMesh _Data22;
	public TextMesh _Data_H;
	public TextMesh _Data_D;
	public TextMesh _Data_P;

	public string[] _Lables;
	public string[] _Datas;

	//************************************************************************    Line Renderer     *************************************************************************

	public LineRenderer _Axis_Y0;
	public LineRenderer _Axis_Y1;
	public LineRenderer _Axis_Y2;

	public LineRenderer _Line0;
	public LineRenderer _Line1;
	public LineRenderer _Line2;

	public int _Points_Length = 14;
	public float[] _X_Points; 
	public Vector3[] _Points_0;
	public Vector3[] _Points_1;
	public Vector3[] _Points_2;
	public Vector3[] _Points_3;
	public Vector3[] _Points_4;

	//************************************************************************    Water Flow     *************************************************************************

	float Zvalue=0f;
	public float Length;
	public float Length_1;
	public float Length_DH_0;
	public float Length_DH_2;
	public float Length_DH_3;

	public GameObject _Start_;
	public GameObject _Start_1_;
	public GameObject _End_;
	public GameObject _Start_DH_0_;
	public GameObject _Start_DH_1_;
	public GameObject _Start_DH_2_;
	public GameObject _Start_DH_3_;
	public GameObject _Water_Tank;
	public GameObject _Imp_;
	public GameObject _Origin_;

	public ParticleSystem _End;
	public ParticleSystem _Start_1;
	public ParticleSystem _Start;
	public ParticleSystem _Start_DH_0;
	public ParticleSystem _Start_DH_1;
	public ParticleSystem _Start_DH_2;
	public ParticleSystem _Start_DH_3;
	public ParticleSystem _Imp;
	public ParticleSystem _Origin;

	void Start () 
	{
		Cam = GameObject.FindObjectOfType (typeof(Camera_Control))as Camera_Control;
		Cam.enabled = false;
		Rotor = GameObject.FindObjectOfType (typeof(Rotate))as Rotate;
		Rotor.enabled = false;
		_SH_OldScale = _SH_Pipe.transform.localScale;
		_DH_OldScale = _DH_Pipe.transform.localScale;
		_DH_OldPosition = _DH_Pipe.transform.localPosition;
		_DH_Lbend_OldPosition = _DH_Lbend.transform.localPosition;
		_Optimize.enabled = true;
		_Simulation.enabled = false;
		_Warning.enabled = false;
		Rotor.enabled = false;
		_Graph.SetActive (false);
		_Full_Pump.SetActive (false); 
		_Curves.gameObject.SetActive(false);


		_Ref_Impeller_Dia *= _Ft_mm ;
		_Ref_Discharge *= _Gpm_Lps ;
		_Ref_Head *= _Ft_mm ;

		//************************************************************************    Line Renderer     *************************************************************************

		_Line0.positionCount = _Points_Length;
		_Line1.positionCount = _Points_Length;
		_Line2.positionCount = _Points_Length;
 
		_X_Points = new float[] { 0.5f , 1f , 1.5f , 2f , 2.5f , 3f , 3.5f , 4f , 4.5f , 5f , 5.5f , 6f , 6.5f , 7f };

		_Lables = new string[] { "Discharge ( L / s ) --->" ,  "Brake Horse Power ( HP ) --->" , "Impeller Dia ( mm ) --->" , "Head ( mm ) --->" , "Water Horse Power ( HP ) --->" , "Efficiency ( % ) --->" };

		_Datas = new string[45];

		_Points_0 = new Vector3[_Points_Length];
		_Points_1 = new Vector3[_Points_Length];
		_Points_2 = new Vector3[_Points_Length];
		_Points_3 = new Vector3[_Points_Length];
		_Points_4 = new Vector3[_Points_Length];

		//************************************************************************    Water Flow     *************************************************************************

		_Start_.SetActive (false);
		_Start_1_.SetActive (false);
		_End_.SetActive (false);
		_Start_DH_0_.SetActive (false);
		_Start_DH_1_.SetActive (false);
		_Start_DH_2_.SetActive (false);
		_Start_DH_3_.SetActive (false);
		_Imp_.SetActive (false);
		_Origin_.SetActive (false);

	}
	
	public void Apply () 
	{
		_Assign_Datas();

		_Mfactor = (_Discharge_Input_Value * _Ref_Impeller_Dia) / (_Ref_Discharge * _Impeller_Dia);

		_Max_Head = _Ref_Head / Mathf.Pow ((_Ref_Discharge / _Discharge_Input_Value), 2);

		_Max_Power = _Ref_Power / Mathf.Pow ((_Ref_Discharge / _Discharge_Input_Value), 3);

		_Total_Head = _SH_Input_Value + _DH_Input_Value;

		_Acc_Discharge = _Ref_Discharge / Mathf.Pow ((_Ref_Head / _Total_Head), 0.5f);

		_Acc_Impeller_Dia = _Ref_Impeller_Dia / Mathf.Pow ((_Ref_Head / _Total_Head), 0.5f);

		_Acc_Power = _Ref_Power / Mathf.Pow ((_Ref_Discharge / _Acc_Discharge), 3);

		_WHP = (_Discharge_Input_Value * _Total_Head * _mm_Ft * _Lps_Gpm) / 3960f;

		_Max_WHP = (_Discharge_Input_Value * _Max_Head * _mm_Ft * _Lps_Gpm) / 3960f;

		_Eff = (_WHP*100f)/_Max_Power;

		_Max_Eff = (_Max_WHP*100f)/_Max_Power;

		if (_Total_Head > _Max_Head)
			_Warning.enabled = true;
		
		else
			_Warning.enabled = false;
		
		_FullPump_NewScale = new Vector3(_Mfactor,_Mfactor,_Mfactor);
		_Full_Pump.transform.localScale = _FullPump_NewScale;
		_SH_Scale_Factor = ((_SH_Input_Value - (_SH_Radius * 1.8f * _Mfactor)) / (80.75f*_Mfactor));
		_DH_Scale_Factor = (((_DH_Input_Value / _Mfactor) - (_DH_Radius * 1.8f) - 250f) / 50f);
		_SH_NewScale = new Vector3(_SH_OldScale.x,_SH_Scale_Factor,_SH_OldScale.z);
		_SH_Pipe.transform.localScale = _SH_NewScale;
		_DH_NewScale = new Vector3(_DH_OldScale.x,_DH_Scale_Factor,_DH_OldScale.z);
		_DH_Pipe.transform.localScale = _DH_NewScale;
		_DH_Lbend_NewPosition = new Vector3 (_DH_Lbend_OldPosition.x, _DH_OldPosition.y + (_DH_NewScale.y * 50f), _DH_Lbend_OldPosition.z);
		_DH_Lbend.transform.localPosition = _DH_Lbend_NewPosition;

		_Full_Pump.SetActive (true);

		_Details.text = "\n Given : \n   H = " + (int)_Total_Head + " mm \n   Q = " + (int)_Discharge_Input_Value + " L / s \n   BHP = "
			+ _Max_Power.ToString("F2") + " HP \n   WHP = " + _WHP.ToString("F2") + " HP \n   D = "+ (int)(_Mfactor*_Impeller_Dia) 
			+ " mm \n   N = 1750 rpm \n   n = " + _Eff.ToString("F2") + " % "
			+ "\n \n Max : \n   H = " + (int)_Max_Head + " mm \n   WHP = " + _Max_WHP.ToString("F2") + " HP \n   n = " + _Max_Eff.ToString("F2") + " % ";		                    
	}


	public void Play()
	{
		Apply ();
		_Curves.gameObject.SetActive(false);
		_Optimize.enabled = false;
		_Simulation.enabled = true;
		Rotor.enabled = true;
		_Graph.SetActive (false);
		_Full_Pump.SetActive (true);
		Length = _SH_Scale_Factor * 96.75f * _Mfactor ;
		Length_1 = _Mfactor * 230f;
		Length_DH_0 = _DH_Input_Value - (100f * _Mfactor);
		Length_DH_2 = _Mfactor*235f;
		Length_DH_3 = 300f*_Mfactor;
		Time.timeScale = 1f;

		StartCoroutine (_Start_Sim ());
	}


	public void Stop()
	{
		_Curves.gameObject.SetActive(false);
		_Optimize.enabled = false;
		_Simulation.enabled = true;
		Rotor.enabled = false;
		_Graph.SetActive (false);
		_Full_Pump.SetActive (true);
		Time.timeScale = 0f;
	}

	public void Customize()
	{
		_Curves.gameObject.SetActive(false);
		_Optimize.enabled = true;
		_Simulation.enabled = false;
		Rotor.enabled = false;
		_Graph.SetActive (false);
		_Full_Pump.SetActive (false);
		_Start_.SetActive (false);
		_Start_1_.SetActive (false);
		_End_.SetActive (false);
		_Start_DH_0_.SetActive (false);
		_Start_DH_1_.SetActive (false);
		_Start_DH_2_.SetActive (false);
		_Start_DH_3_.SetActive (false);
		_Imp_.SetActive (false);
		_Origin_.SetActive (false);
	}


	public void Graph()
	{
		_Curves.gameObject.SetActive(true);
		_Curves.interactable = true;
		_Graph.SetActive (true);
		_Optimize.enabled = false;
		_Simulation.enabled = false;
		Rotor.enabled = false;
		_Full_Pump.SetActive (false);
	}


	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
		if (Input.GetKeyDown (KeyCode.V))
			Cam.enabled = true;
		if (!Cam.enabled) 
		{
			_Camera.transform.localPosition = new Vector3 (-_Total_Head-150f, -100f+((_DH_Input_Value-_SH_Input_Value)/2f), -200f);
			_Camera.transform.parent.eulerAngles = new Vector3(0f,0f,0f);
		}

		if(_Graph!=null)
			_Graphs_Plot ();
		if (_Optimize.enabled == true)
		{
			_Unit_Q.gameObject.SetActive (true);
			_Unit_SH.gameObject.SetActive (true);
			_Unit_DH.gameObject.SetActive (true);

		}
        else		
		{
			_Unit_Q.gameObject.SetActive (false);
			_Unit_SH.gameObject.SetActive (false);
			_Unit_DH.gameObject.SetActive (false);

		}

		if (_Simulation.enabled == false)
			_Reset_PS ();
	}


	public void Straight ()
	{
		_Start_.SetActive (true);
		var main = _Start.main;
		var vel = _Start.velocityOverLifetime;
		vel.enabled = true;
		vel.space = ParticleSystemSimulationSpace.Local;
		main.startSize = _Mfactor*_SH_Radius;
		for (float i = 0; i < Length; i+=0.1f) 
		{
			vel.z = new ParticleSystem.MinMaxCurve (i,i);

		}

	}


	public void Create_Curve ()
	{
		_End_.SetActive (true);
		var main = _End.main;
		var vel = _End.velocityOverLifetime;
		vel.enabled = true;
		vel.space = ParticleSystemSimulationSpace.Local;
		main.startSize = _Mfactor * 5f;

		vel.x = new ParticleSystem.MinMaxCurve (10f, Zvalue);

		AnimationCurve y = new AnimationCurve ();
		for (int i = 0; i < 20; i++) {
			float newtime = (i / 19f);
			float my = _Mfactor*2f*Mathf.Sin (-newtime * Mathf.PI / 2f);

			y.AddKey (newtime, my);
		}

		vel.y = new ParticleSystem.MinMaxCurve (10f, y);


		AnimationCurve z = new AnimationCurve ();
		for (int i = 0; i < 20; i++)
		{
			float newtime = (i / 19f);
			float my = _Mfactor*2f*Mathf.Cos (-newtime * Mathf.PI / 2f);

			z.AddKey (newtime, my);
		}

		vel.z = new ParticleSystem.MinMaxCurve (10f, z);
	}


	public void Straight_1 ()
	{
		_Start_1_.SetActive (true);
		var main = _Start_1.main;
		var vel = _Start_1.velocityOverLifetime;
		vel.enabled = true;
		vel.space = ParticleSystemSimulationSpace.Local;
		main.startSize = _Mfactor * _SH_Radius;
		for (float i = 0; i < Length_1; i += 0.1f) 
		{
			vel.z = new ParticleSystem.MinMaxCurve (i, i);

		}

	}


	public void Create_Curve_Imp ()
	{
		_Origin_.SetActive (true);
		var main_O = _Origin.main;
		main_O.startSize = _Mfactor * 50f;

		_Imp_.SetActive (true);
		var main = _Imp.main;
		var vel = _Imp.velocityOverLifetime;
		vel.enabled = true;
		vel.space = ParticleSystemSimulationSpace.Local;
		main.startSize = _Mfactor * 5f;

		vel.x = new ParticleSystem.MinMaxCurve (10f, Zvalue);

		AnimationCurve y = new AnimationCurve ();
		for (int i = 0; i < 20; i++) {
			float newtime = (i / 19f);
			float my = _Mfactor*2f*Mathf.Sin (-newtime * Mathf.PI * 2f);

			y.AddKey (newtime, my);
		}

		vel.y = new ParticleSystem.MinMaxCurve (10f, y);


		AnimationCurve z = new AnimationCurve ();
		for (int i = 0; i < 20; i++)
		{
			float newtime = (i / 19f);
			float my = _Mfactor*2f*Mathf.Cos (-newtime * Mathf.PI * 2f);

			z.AddKey (newtime, my);
		}

		vel.z = new ParticleSystem.MinMaxCurve (10f, z);
	}


	public void Straight_DH_0 ()
	{
		_Start_DH_0_.SetActive (true);
		var main = _Start_DH_0.main;
		var vel = _Start_DH_0.velocityOverLifetime;
		vel.enabled = true;
		vel.space = ParticleSystemSimulationSpace.Local;
		main.startSize = _Mfactor*_DH_Radius;
		for (float i = 0; i < Length_DH_0; i+=0.1f) 
		{
			vel.z = new ParticleSystem.MinMaxCurve (i,i);

		}

	}


	public void Create_Curve_DH_1 ()
	{
		_Start_DH_1_.SetActive (true);
		var main = _Start_DH_1.main;
		var vel = _Start_DH_1.velocityOverLifetime;
		vel.enabled = true;
		vel.space = ParticleSystemSimulationSpace.Local;
		main.startSize = _Mfactor * 5f;

		vel.x = new ParticleSystem.MinMaxCurve (10f, Zvalue);

		AnimationCurve y = new AnimationCurve ();
		for (int i = 0; i < 20; i++) {
			float newtime = (i / 19f);
			float my = _Mfactor*2f*Mathf.Sin (-newtime * Mathf.PI / 2f);
				
			y.AddKey (newtime, my);
		}

		vel.y = new ParticleSystem.MinMaxCurve (10f, y);


		AnimationCurve z = new AnimationCurve ();
		for (int i = 0; i < 20; i++)
		{
			float newtime = (i / 19f);
			float my = _Mfactor*2f*Mathf.Cos (-newtime * Mathf.PI / 2f);

			z.AddKey (newtime, my);
		}

		vel.z = new ParticleSystem.MinMaxCurve (10f, z);
	}


	public void Straight_DH_2 ()
	{
		_Start_DH_2_.SetActive (true);
		var main = _Start_DH_2.main;
		var vel = _Start_DH_2.velocityOverLifetime;
		vel.enabled = true;
		vel.space = ParticleSystemSimulationSpace.Local;
		main.startSize = _Mfactor * _DH_Radius;
		for (float i = 0; i < Length_DH_2; i += 0.1f)
		{
			vel.z = new ParticleSystem.MinMaxCurve (i, i);

		}

	}
		

	public void Straight_DH_3 ()
	{
		_Start_DH_3_.SetActive (true);
		var main = _Start_DH_3.main;
		var vel = _Start_DH_3.velocityOverLifetime;
		vel.enabled = true;
		vel.space = ParticleSystemSimulationSpace.Local;
		main.startSize = _Mfactor * _DH_Radius;
		main.gravityModifier = _Mfactor * 20f;
		for (float i = 0; i < Length_DH_3; i += 0.1f) 
		{
			vel.z = new ParticleSystem.MinMaxCurve (i, i);

		}

	}


	public void _Graphs_Plot()
	{
		//************************************************************************    Data Calculation     *************************************************************************

		_Max_X = _Discharge_Input_Value;
		_Min_X = _Acc_Discharge;

		_Max_0 = _Max_Power;
		_Min_0 = 0f;

		_Max_1 = _Mfactor * _Impeller_Dia;
		_Min_1 = _Acc_Impeller_Dia;

		_Max_2 = _Max_Head;
		_Min_2 = _Total_Head;

		_Max_3 = _Max_WHP;
		_Min_3 = 0f;

		_Max_4 = 100f;
		_Min_4 = 0f;

		_Diff_X = (_Max_X - _Min_X)/6f;
		_Diff_0 = (_Max_0 - _Min_0)/5f;
		_Diff_1 = (_Max_1 - _Min_1)/5f;
		_Diff_2 = (_Max_2 - _Min_2)/5f;
		_Diff_3 = (_Max_3 - _Min_3)/5f;
		_Diff_4 = (_Max_4 - _Min_4)/5f;

		for (int d = 0; d < 7; d++)
		{
			_Datas [d] = (_Min_X + (d * _Diff_X)).ToString("F4");
			_Datas [d + 8] = (_Min_0 + (d * _Diff_0)).ToString("F4");
			_Datas [d + 15] = (_Min_1 + (d * _Diff_1)).ToString("F4");
			_Datas [d + 22] = (_Min_2 + (d * _Diff_2)).ToString("F4");
			_Datas [d + 29] = (_Min_3 + (d * _Diff_3)).ToString("F4");
			_Datas [d + 36] = (_Min_4 + (d * _Diff_4)).ToString("F4");

		}
		_Datas [7] = (_Min_X + (7 * _Diff_X)).ToString("F4");

		//************************************************************************    Data Assign     *************************************************************************

		_X_Lable.text = _Lables[0];
		_Data002.text=_Datas[0];
		_Data16.text=_Datas[1];
		_Data17.text=_Datas[2];
		_Data18.text=_Datas[3];
		_Data19.text=_Datas[4];
		_Data20.text=_Datas[5];
		_Data21.text=_Datas[6];
		_Data22.text = _Datas[7];


		//************************************************************************    Line Renderer     *************************************************************************


		if(_Curves.value == 0)
		{
			_Scale.text = "Scale : \n  In X=" + _Diff_X.ToString("F4")
				+ " L/s\n\n  In Y= \n               " 
				+ _Diff_0.ToString("F4") + " HP\n               " 
				+ _Diff_1.ToString("F4") + " mm\n               " 
				+ _Diff_2.ToString("F4") + " mm";

			_Line0.enabled = true;
			_Line1.enabled = true;
			_Line2.enabled = true;

			_Axis_Y0.enabled = true;
			_Axis_Y1.enabled = true;
			_Axis_Y2.enabled = true;

			_Y0_Lable.text = _Lables[1];
			_Y1_Lable.text = _Lables[2];
			_Y2_Lable.text = _Lables[3];

			_Data00.text=_Datas[8];
			_Data11.text=_Datas[9];
			_Data12.text=_Datas[10];
			_Data13.text=_Datas[11];
			_Data14.text=_Datas[12];
			_Data15.text=_Datas[13];
			_Data_P.text=_Datas[14];

			_Data01.text=_Datas[15];
			_Data6.text=_Datas[16];
			_Data7.text=_Datas[17];
			_Data8.text=_Datas[18];
			_Data9.text=_Datas[19];
			_Data10.text=_Datas[20];
			_Data_D.text=_Datas[21];

			_Data02.text=_Datas[22];
			_Data1.text=_Datas[23];
			_Data2.text=_Datas[24];
			_Data3.text=_Datas[25];
			_Data4.text=_Datas[26];
			_Data5.text=_Datas[27];
			_Data_H.text=_Datas[28];

			for (int i = 0; i < _Points_Length; i++) 
			{
				_Points_0 [i] = new Vector3 (_X_Points [i], ((((_Ref_Power / Mathf.Pow (_Ref_Discharge / (_Min_X + (_X_Points[i]* _Diff_X)),3))-_Min_0)/_Diff_0)-3f), 0f);
				_Points_1 [i] = new Vector3 (_X_Points [i], ((((((_Min_X + (_X_Points[i]* _Diff_X))* _Ref_Impeller_Dia) / (_Ref_Discharge))-_Min_1)/_Diff_1)-3f), 0f);
				_Points_2 [i] = new Vector3 (_X_Points [i], ((((_Ref_Head / Mathf.Pow (_Ref_Discharge / (_Min_X + (_X_Points[i]* _Diff_X)),2))-_Min_2)/_Diff_2)-3f), 0f);

				_Line0.SetPosition (i , _Points_0[i]);
				_Line1.SetPosition (i , _Points_1[i]);
				_Line2.SetPosition (i , _Points_2[i]);
			}
		}


		if(_Curves.value == 1)
		{
			_Scale.text = "Scale : \n  In X=" + _Diff_X.ToString ("F4")+ " L/s\n\n  In Y="+ _Diff_2.ToString ("F4")+" mm ";

			_Line0.enabled = false;
			_Line1.enabled = false;
			_Line2.enabled = true;

			_Axis_Y0.enabled = false;
			_Axis_Y1.enabled = false;
			_Axis_Y2.enabled = true;

			_Y0_Lable.text = " ";
			_Y1_Lable.text = " ";
			_Y2_Lable.text = _Lables[3];

			_Data00.text= " ";
			_Data11.text= " ";
			_Data12.text= " ";
			_Data13.text= " ";
			_Data14.text= " ";
			_Data15.text= " ";
			_Data_P.text= " ";

			_Data01.text=" ";
			_Data6.text=" ";
			_Data7.text=" ";
			_Data8.text=" ";
			_Data9.text=" ";
			_Data10.text=" ";
			_Data_D.text=" ";

			_Data02.text=_Datas[22];
			_Data1.text=_Datas[23];
			_Data2.text=_Datas[24];
			_Data3.text=_Datas[25];
			_Data4.text=_Datas[26];
			_Data5.text=_Datas[27];
			_Data_H.text=_Datas[28];

			for (int i = 0; i < _Points_Length; i++) 
			{
				_Points_2 [i] = new Vector3 (_X_Points [i], ((((_Ref_Head / Mathf.Pow (_Ref_Discharge / (_Min_X + (_X_Points[i]* _Diff_X)),2))-_Min_2)/_Diff_2)-3f), 0f);

				_Line2.SetPosition (i , _Points_2[i]);
			}
		}


		if(_Curves.value == 2)
		{
			_Scale.text = "Scale : \n  In X=" + _Diff_X.ToString ("F4")+ " L/s\n\n  In Y="+ _Diff_0.ToString ("F4")+" mm ";

			_Line0.enabled = false;
			_Line1.enabled = false;
			_Line2.enabled = true;

			_Axis_Y0.enabled = false;
			_Axis_Y1.enabled = false;
			_Axis_Y2.enabled = true;

			_Y0_Lable.text = " ";
			_Y1_Lable.text = " ";
			_Y2_Lable.text = _Lables[2];

			_Data00.text= " ";
			_Data11.text= " ";
			_Data12.text= " ";
			_Data13.text= " ";
			_Data14.text= " ";
			_Data15.text= " ";
			_Data_P.text= " ";

			_Data01.text=" ";
			_Data6.text=" ";
			_Data7.text=" ";
			_Data8.text=" ";
			_Data9.text=" ";
			_Data10.text=" ";
			_Data_D.text=" ";

			_Data02.text=_Datas[15];
			_Data1.text=_Datas[16];
			_Data2.text=_Datas[17];
			_Data3.text=_Datas[18];
			_Data4.text=_Datas[19];
			_Data5.text=_Datas[20];
			_Data_H.text=_Datas[21];

			for (int i = 0; i < _Points_Length; i++) 
			{
				_Points_1 [i] = new Vector3 (_X_Points [i], ((((((_Min_X + (_X_Points[i]* _Diff_X))* _Ref_Impeller_Dia) / (_Ref_Discharge))-_Min_1)/_Diff_1)-3f), 0f);

				_Line2.SetPosition (i , _Points_1[i]);
			}
		}


		if(_Curves.value == 3)
		{

			_Scale.text = "Scale : \n  In X=" + _Diff_X.ToString ("F4")+ " L/s\n\n  In Y="+ _Diff_1.ToString ("F4")+" HP ";


			_Line0.enabled = false;
			_Line1.enabled = false;
			_Line2.enabled = true;

			_Axis_Y0.enabled = false;
			_Axis_Y1.enabled = false;
			_Axis_Y2.enabled = true;

			_Y0_Lable.text = " ";
			_Y1_Lable.text = " ";
			_Y2_Lable.text = _Lables[1];

			_Data00.text= " ";
			_Data11.text= " ";
			_Data12.text= " ";
			_Data13.text= " ";
			_Data14.text= " ";
			_Data15.text= " ";
			_Data_P.text= " ";

			_Data01.text=" ";
			_Data6.text=" ";
			_Data7.text=" ";
			_Data8.text=" ";
			_Data9.text=" ";
			_Data10.text=" ";
			_Data_D.text=" ";

			_Data02.text=_Datas[8];
			_Data1.text=_Datas[9];
			_Data2.text=_Datas[10];
			_Data3.text=_Datas[11];
			_Data4.text=_Datas[12];
			_Data5.text=_Datas[13];
			_Data_H.text=_Datas[14];

			for (int i = 0; i < _Points_Length; i++) 
			{
				_Points_0 [i] = new Vector3 (_X_Points [i], ((((_Ref_Power / Mathf.Pow (_Ref_Discharge / (_Min_X + (_X_Points[i]* _Diff_X)),3))-_Min_0)/_Diff_0)-3f), 0f);

				_Line2.SetPosition (i , _Points_0[i]);
			}
		}


		if(_Curves.value == 4)
		{
			_Scale.text = "Scale : \n  In X=" + _Diff_X.ToString ("F4")+ " L/s\n\n  In Y="+ _Diff_3.ToString ("F4")+" HP ";


			_Line0.enabled = false;
			_Line1.enabled = false;
			_Line2.enabled = true;

			_Axis_Y0.enabled = false;
			_Axis_Y1.enabled = false;
			_Axis_Y2.enabled = true;

			_Y0_Lable.text = " ";
			_Y1_Lable.text = " ";
			_Y2_Lable.text = _Lables[4];

			_Data00.text= " ";
			_Data11.text= " ";
			_Data12.text= " ";
			_Data13.text= " ";
			_Data14.text= " ";
			_Data15.text= " ";
			_Data_P.text= " ";

			_Data01.text=" ";
			_Data6.text=" ";
			_Data7.text=" ";
			_Data8.text=" ";
			_Data9.text=" ";
			_Data10.text=" ";
			_Data_D.text=" ";

			_Data02.text=_Datas[29];
			_Data1.text=_Datas[30];
			_Data2.text=_Datas[31];
			_Data3.text=_Datas[32];
			_Data4.text=_Datas[33];
			_Data5.text=_Datas[34];
			_Data_H.text=_Datas[35];

			for (int i = 0; i < _Points_Length; i++) 
			{
				_Points_3 [i] = new Vector3 (_X_Points [i], ((((((_Min_3 + (i * _Diff_3)) *(_Ref_Head / Mathf.Pow (_Ref_Discharge / (_Min_X + (_X_Points[i]* _Diff_X)),2)) * _mm_Ft * _Lps_Gpm) / 3960f)-_Min_3)/_Diff_3)-3f), 0f);

				_Line2.SetPosition (i , _Points_3[i]);
			}
		}
			

		if(_Curves.value == 5)
		{
			_Scale.text = "Scale : \n  In X=" + _Diff_X.ToString ("F4")+ " L/s\n\n  In Y="+ _Diff_4+" % ";

			_Line0.enabled = false;
			_Line1.enabled = false;
			_Line2.enabled = true;

			_Axis_Y0.enabled = false;
			_Axis_Y1.enabled = false;
			_Axis_Y2.enabled = true;

			_Y0_Lable.text = " ";
			_Y1_Lable.text = " ";
			_Y2_Lable.text = _Lables[5];

			_Data00.text= " ";
			_Data11.text= " ";
			_Data12.text= " ";
			_Data13.text= " ";
			_Data14.text= " ";
			_Data15.text= " ";
			_Data_P.text= " ";

			_Data01.text=" ";
			_Data6.text=" ";
			_Data7.text=" ";
			_Data8.text=" ";
			_Data9.text=" ";
			_Data10.text=" ";
			_Data_D.text=" ";

			_Data02.text=_Datas[36];
			_Data1.text=_Datas[37];
			_Data2.text=_Datas[38];
			_Data3.text=_Datas[39];
			_Data4.text=_Datas[40];
			_Data5.text=_Datas[41];
			_Data_H.text= " ";

			for (int i = 0; i < _Points_Length; i++) 
			{
				_Points_4 [i] = new Vector3 (_X_Points [i], ((((((_Min_3 + (i * _Diff_3)) * (_Ref_Head / Mathf.Pow (_Ref_Discharge / (_Min_X + (_X_Points [i] * _Diff_X)), 2)) * _mm_Ft * _Lps_Gpm) / 3960f)/(_Ref_Power / Mathf.Pow (_Ref_Discharge / (_Min_X + (_X_Points[i]* _Diff_X)),3)))/(_Diff_4/100f))-3f), 0f);
				_Line2.SetPosition (i , _Points_4[i]);
			}

		 }

	  }


	public void _Assign_Datas()
	{
		if (_SH_Input.text != "")
		{
			_SH_Input_Value = (float.Parse (_SH_Input.text.ToString ()));
			if (_Unit_SH.value == 0)
				_SH_Input_Value *= _Ft_mm;
		}
		else
		{
			if (_Unit_SH.value == 0) 
			{
				_SH_Input_Value = 2f * _Ft_mm;
				_SH_Input.text = "2";
			}
			if (_Unit_SH.value == 1) 
			{
				_SH_Input_Value = 609.6f;
				_SH_Input.text = "609.6";
			}
		}


		if (_DH_Input.text != "")
		{
			_DH_Input_Value = (float.Parse (_DH_Input.text.ToString ()));
			if (_Unit_DH.value == 0)
				_DH_Input_Value *= _Ft_mm;
		}
		else
		{
			if (_Unit_DH.value == 0) 
			{
				_DH_Input_Value = 2f * _Ft_mm;
				_DH_Input.text = "2";
			}
			if (_Unit_DH.value == 1) 
			{
				_DH_Input_Value = 609.6f;
				_DH_Input.text = "609.6";
			}

		}


		if (_Discharge_Input.text != "")
		{
			_Discharge_Input_Value = (float.Parse (_Discharge_Input.text.ToString()));
			if (_Unit_Q.value == 0)
				_Discharge_Input_Value *= _Gpm_Lps;
		}
		else
		{
			if (_Unit_Q.value == 0) 
			{
				_Discharge_Input_Value = 1000f * _Gpm_Lps;
				_Discharge_Input.text = "1000";
			}
			if (_Unit_Q.value == 1) 
			{
				_Discharge_Input_Value = 63.090196667f;
				_Discharge_Input.text = "63.090196667";
			}
		}
	}


	public void _Reset_PS()
	{
		_Start.Stop ();
		_End.Stop ();
		_Start_1.Stop ();
		_Start_DH_0.Stop ();
		_Start_DH_1.Stop ();
		_Start_DH_2.Stop ();
		_Start_DH_3.Stop ();
		_Imp.Stop ();
		_Origin.Stop ();
		_Start.Clear ();
		_End.Clear ();
		_Start_1.Clear ();
		_Start_DH_0.Clear ();
		_Start_DH_1.Clear ();
		_Start_DH_2.Clear ();
		_Start_DH_3.Clear ();
		_Imp.Clear ();
		_Origin.Clear ();
		_Start_.SetActive ( false );
		_End_.SetActive ( false );
		_Start_1_.SetActive ( false );
		_Start_DH_0_.SetActive ( false );
		_Start_DH_1_.SetActive ( false );
		_Start_DH_2_.SetActive ( false );
		_Start_DH_3_.SetActive ( false );
		_Imp_.SetActive ( false );
		_Origin_.SetActive ( false );
	}


	public IEnumerator _Start_Sim()
	{
		_Reset_PS ();
		yield return new WaitForSeconds (0.3f);
		Straight ();
		yield return new WaitForSeconds (1f);
		Create_Curve();
		yield return new WaitForSeconds (0.8f);
		Straight_1();
		yield return new WaitForSeconds (1f);
		Create_Curve_Imp();
		yield return new WaitForSeconds (1f);
		Straight_DH_0();
		yield return new WaitForSeconds (1f);
		Create_Curve_DH_1();
		yield return new WaitForSeconds (0.8f);
		Straight_DH_2();
		yield return new WaitForSeconds (1f);
		Straight_DH_3();

	}
		
}



