using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 音声全体の再生速度を制御します
/// </summary>
public class Tempo : MonoBehaviour {

	public static bool isPLAY = true;

	public float isBPM = 120;
	public float BeatTime;

	public float isTimelate;

	// サウンドのデータ
	public int Measure_val = 0;
	public int Measure_SetValue = 4;


	public AudioSource[] metoronomeAudio;


	void Awake () {
		isTimelate = BeatTime;
		Measure_val = 0;
	}

	void Start() {
		metoronomeAudio = this.GetComponents<AudioSource>();

		defaultTempo ();
	}

	void FixedUpdate() {
		if (isPLAY) {
			isTimelate -= Time.deltaTime;
			if (isTimelate <= 0) {
				if (Measure_val == 0) {
					metoronomeAudio [1].Play ();
				} else {
					metoronomeAudio [0].Play ();
				}
				Measure_val++;
				isTimelate = BeatTime;
				if (Measure_val >= Measure_SetValue)
					Measure_val = 0;
			}
		}
	}

	// BPM,テンポの調整と調査
	public void ResettingTempo (){
		// 1拍子の値が異常
		if (BeatTime <= 0 || BeatTime >= 999){
			defaultTempo ();
		}
	}

	// 初期設定
	private void defaultTempo (){
		BeatTime = 0.5f;
		isBPM = Mathf.Floor(60 / BeatTime);
	}
}
