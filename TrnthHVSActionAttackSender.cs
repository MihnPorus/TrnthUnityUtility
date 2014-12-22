﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrnthHVSActionAttackSender : TrnthHVSAction {
	public TrnthHVSConditionPhysicsCast pc;
	public TrnthAttack attack;
	// public int damage;
	protected override void _execute(){
		base._execute();
		if(pc)colliders=pc.colliders;
		foreach(Collider e in colliders){
			var dr=e.GetComponent<TrnthAttackReceiver>();
			if(!dr)continue;
			dr.hurtWith(attack);
		}
	}
	Collider[] colliders;
	void OnTriggerEnter(Collider collider){
		// Debug.Log(collider.name);
		colliders=new Collider[]{collider};
		execute();
	}
	void OnCollisionEnter(Collision collision){
		colliders=new Collider[]{collision.collider};
		execute();
	}
}