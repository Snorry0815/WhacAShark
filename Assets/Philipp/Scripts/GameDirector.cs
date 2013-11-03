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
	
	public delegate void MalletHitEvent(float x,float y);
	public static event MalletHitEvent MalletHit;
	public static void TriggerMalletHit(float x, float y) {
		if(MalletHit != null) {
			MalletHit(x, y);
		}
	}
}
