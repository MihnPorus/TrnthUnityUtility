﻿using UnityEngine;
using System.Collections;
public class OnewayPlatformRigidbody : MonoBehaviour {
	void toggle(Collider self,Collider other,bool yes){
		if(self==other)return;
		var trigger=other.GetComponent<OnewayPlatformTrigger>();
		if(!trigger)return;
		other=trigger.platformSolid;
		if(!self.gameObject.activeInHierarchy)return;
		if(!other.gameObject.activeInHierarchy)return;
		Physics.IgnoreCollision(self,other,yes);
	}
	void OnTriggerStay(Collider other){
		toggle(collider,other,true);
	}
	void OnTriggerExit(Collider other){
		toggle(collider,other,false);
	}
}
