﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
//using System;

namespace TRNTH{
	public static class TrnthExtensions
	{
		static public void ResetOnClick(this Button button,UnityEngine.Events.UnityAction onClick){
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(onClick);
		}
		public static IEnumerable<T> Page<T>(this IEnumerable<T> list,int page,int sizePerPage){
			var startIndex=page*sizePerPage;
			if(list.Count() >=startIndex){
				list=list.GetRange(startIndex,Mathf.Min(sizePerPage,list.Count()-startIndex));
			}
			return list;
		}
		public static IEnumerable<T> Intersection<T>(this IEnumerable<T> a,IEnumerable<T> b) {
			return a.FindAll(t=>{
				bool yes=false;
				foreach(var tt in b){
					if(tt is System.IEquatable<T>){
						var camparer=(System.IEquatable<T>)tt;
						yes=camparer.Equals(t);
						if(yes)return yes;
					}
					yes= tt.Equals(t);
					if(yes)return yes;
				}
				return false;
			})
			;
		}
		public static IEnumerable<T> GetUniqueFlags<T>(this System.Enum flags) 
		{
			
			ulong flag = 1;
			foreach (var value in System.Enum.GetValues(flags.GetType()).ConvertAll<T>())
			{
				ulong bits = System.Convert.ToUInt64(value);
				while (flag < bits)
				{
					flag <<= 1;
				}
				
				if (flag == bits && (System.Convert.ToUInt64(flags) & System.Convert.ToUInt64(value))!=0)
				{
					yield return value;
				}
			}
		}
		public static IEnumerable<T> GetRange<T>(this  IEnumerable<T> container,int start,int count){
			var list=new List<T>(container);
			if(list.Count<count+start){
				return list;
			}
			return list.GetRange(start,count);
		}
		public static T Find<T>(this IEnumerable<T> list,T value) where T : class
		{
			foreach(T e in list){
				if(value==(e))return e;
			}
			return default(T);
		}
		public static T Find<T>(this IEnumerable<T> collect,System.Predicate<T> predicate){
			return new List<T>(collect).Find(predicate);
		}
		public static IEnumerable<T> FindAll<T>(this IEnumerable<T> collect,System.Predicate<T> predicate){
			return new List<T>(collect).FindAll(predicate);
		}
		public static bool Exists <T>(this IEnumerable<T> collect,System.Predicate<T> predicate){
			return new List<T>(collect).Exists(predicate);
		}
		public static int Count <T>(this IEnumerable<T> collect){
			return new List<T>(collect).Count;
		}
		public static T[] ToArray<T>(this IEnumerable<T> list){
			return new List<T>(list).ToArray();
		}
		public static T RandomChoose<T>(this IEnumerable<T> container){
			var list=new List<T>(container);
			return RandomChooseNonAlloc(list);
		}
		public static T RandomChooseNonAlloc<T>(this IList<T> list){
			if(list.Count<1)return default(T);
			return list[Random.Range(0,list.Count)];
		}
		public static IList<T> Shuffle<T>(this IList<T> arrOrin){
//			if(arrOrin.Count<1)return arrOrin;
			var list=arrOrin;
			var rng = new System.Random();  
			int n = arrOrin.Count;
			while (n > 1) {
				n--;  
				int k = rng.Next(n + 1);  
				T value = list[k];  
				list[k] = list[n];  
				list[n] = value;  
			} 
			return list;
		}
//		public static IEnumerable<T> GetShuffled<T>(this IEnumerable<T> list){
//			return new List<T>(U.shuffle<T>(new List<T>(list).ToArray()));
//		}
//		public static List<T> GetShuffled<T>(this List<T> list){
//			return new List<T>(U.shuffle<T>(list.ToArray()));
//		}
//		public static IList<T> GetShuffled<T>(this IList<T> list){
//			return new List<T>(U.shuffle<T>(list.ToArray()));
//		}
		public static void send(this MonoBehaviour monoBehaviour,TrnthHVSCondition condition){
			if(condition)condition.send();
		}
		public static IList<T> CastComponent<T>(this Component component){
			return component.transform.ConvertAll<Transform>().CastComponent<T>();
		}
		public static IList<T> CastComponent<T>(this IEnumerable components){
			var list=new List<T>();
			foreach(Component e in components){
				var c=e.GetComponent<T>();
				if(c!=null)list.Add(c);
			}
			return list;
		}
		public static IEnumerable<T2> ConvertAll<T,T2>(this IEnumerable<T> container,System.Converter<T,T2> converter){
			return new List<T>(container).ConvertAll<T2>(converter);
		}
		public static IEnumerable<T> ConvertAll<T>(this IEnumerable container){
			var list=new List<T>();
			var e=container.GetEnumerator();
			while(e.MoveNext()){
				if(e.Current is T)list.Add((T)(e.Current));
			}
			return list;
		}
	}   
}