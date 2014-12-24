﻿using UnityEngine;
using System.Collections;

public class TrnthFSMManager : MonoBehaviour {
	public GameObject stateNow;
	[ContextMenu("update")]
	public virtual void update(){
		foreach(Transform e in transform){
			// if(e.gameObject.activeInHierarchy)e.gameObject.SetActive(false);
			e.gameObject.SetActive(e.gameObject==stateNow);
		}
		// if(!stateNow.activeInHierarchy)stateNow.SetActive(true);
	}
	void Awake(){
		update();
	}
}
