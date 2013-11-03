using UnityEngine;
using System.Collections;

public static class GameDirector {
	
	public delegate void WaterHitEvent(float x,float y);
	public static event WaterHitEvent WaterHit;
	public static void TriggerWaterHit(float x, float y) {
		if(WaterHit != null) {
			WaterHit(x, y);
		}
	}
	
	public delegate void WaveEvent(Wave wave);
	public static event WaveEvent Wave;
	public static void TriggerWave(Wave wave) {
		if(Wave != null) {
			Wave(wave);
		}
	}
	
	public delegate void MalletHitEvent(float x,float y, int coordX, int coordY);
	public static event MalletHitEvent MalletHit;
	public static void TriggerMalletHit(float x, float y, int coordX, int coordY) {
		if(MalletHit != null) {
			MalletHit(x, y, coordX, coordY);
		}
	}
	
	public delegate void EscapeEvent(int x,int y, GameObject escapy);
	public static event EscapeEvent Escape;
	public static void TriggerEscape(int x, int y,GameObject escapy) {
		if(Escape != null) {
			Escape(x, y, escapy);
		}
	}
	
	public delegate void WhacEvent(int x,int y);
	public static event WhacEvent Whac;
	public static void TriggerWhac(int x, int y) {
		if(Whac != null) {
			Whac(x, y);
		}
	}
	
	public delegate void WhacObjectEvent(int x,int y, GameObject whacedObject);
	public static event WhacObjectEvent WhacObject;
	public static void TriggerWhacObject(int x, int y, GameObject whacedObject) {
		if(WhacObject != null) {
			WhacObject(x, y, whacedObject);
		}
	}
}
