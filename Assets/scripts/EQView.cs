﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EQView : MonoBehaviour {

	private Slider[] eqBars;
	private float max = 0.0f;
	private float groupValues;
	private int startValue;
	private int endValue;

	// Use this for initialization
	void Start () {
		eqBars = GetComponentsInChildren<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void UpdateEQ (float[] values) {
		//Debug.Log("Length: "+values.Length.ToString()+" Start: "+startValue.ToString()+" End: "+endValue.ToString()+"groupValues: "+groupValues.ToString());
		for (int i = 0; i < eqBars.Length; i++) {
			float tempValue = 0.0f;
			int iGroups = Mathf.RoundToInt(i * groupValues);
			for (int j = 0; j < groupValues; j++) {
				tempValue += values[startValue + iGroups + j];
			}
			if (groupValues >= 2.0f) {
				tempValue /=  Mathf.FloorToInt(groupValues);
			}
			//boost values to make them more visible
			tempValue = Mathf.Sqrt(Mathf.Sqrt(tempValue));

			if (tempValue > max) {
				max = tempValue;
				//Debug.Log("Max: "+max.ToString());
			}
			eqBars[i].value = (tempValue/max);
		}
	}

	public void UpdateEQClamped (float[] values, float start, float end) {
		startValue = Mathf.RoundToInt(start*values.Length);
		endValue = Mathf.RoundToInt(end*values.Length);	
		groupValues = (endValue-startValue)*1.0f/eqBars.Length;
		UpdateEQ(values);
	}

	public void UpdateEQHzRange (float[] values, int fTarget, int fWidth, float fMax) {
		startValue = Mathf.FloorToInt((fTarget - (fWidth / 2)) * values.Length / fMax);
		endValue = Mathf.FloorToInt((fTarget + (fWidth / 2)) * values.Length / fMax);
		groupValues = (endValue-startValue)*1.0f/eqBars.Length;
		UpdateEQ(values);
	}

}