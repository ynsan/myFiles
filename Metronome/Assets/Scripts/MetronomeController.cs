using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// メトロノーム機能_UI操作
/// </summary>
public class MetronomeController : MonoBehaviour {


	// メトロノーム
	public bool TempoTap_timeBool = false;
	public bool Use_TempoTapAverage = false;
	public int Tap_val = 0;
	public float Tap_firstTime;
	public float Tap_lastTime;

	// UI要素
	public InputField Tempo_text;
	public AudioSource SE_tap;

	// メトロノームの音設定
	private int SE_num = 0;	// メトロノームのSE_number
	public Button[] button_SelectMetronomeSound;
	public AudioSource[] BeatSound;

	// 外部Script
	public Tempo tempoScript;

	void Start () {
		TempoTap_timeBool = false;
		Tempo_text.text = (tempoScript.isBPM).ToString();
		Tap_firstTime = Tap_lastTime = tempoScript.BeatTime;

		for (int i = 0; i < button_SelectMetronomeSound.Length; i++){
			int SelectMetronomeSound_Length = i;
			button_SelectMetronomeSound[i].GetComponent<Button> ().onClick.AddListener (() => onClick_BeatSoundSet (SelectMetronomeSound_Length));
		}
	}

	void FixedUpdate () {
		if (TempoTap_timeBool) {
			Tap_firstTime += Time.deltaTime;
		} else {
			Tap_firstTime = 0f;
		}
	}


	// 以下Button処理要素

	public void Play_Stop() {
		Tempo.isPLAY = !Tempo.isPLAY;
		// 拍子の再生時間をリセット
		tempoScript.isTimelate = 0;
	}

	public void TapTempo() {
		SE_tap.Play ();
		if (Tap_val == 0) {
			TempoTap_timeBool = true;
			Tap_val++;
		} else if (Use_TempoTapAverage == true) {
			Tap_val++;
		} else {
			Tap_lastTime = Tap_firstTime;
			tempoScript.BeatTime = Tap_lastTime;
			tempoScript.isBPM = Mathf.Floor(60 / Tap_firstTime);
			TempoTap_timeBool = false;
			Tap_val = 0;
		}
		Tempo_text.text = (tempoScript.isBPM).ToString();
	}

	public void onClick_BeatSoundSet(int num) {
		// 引数操作
		SE_num = num;
		Debug.Log ("メトロノームクロック_" + num);

		BeatSound = button_SelectMetronomeSound[SE_num].GetComponents<AudioSource>();
		tempoScript.metoronomeAudio[0] = BeatSound[0];
		tempoScript.metoronomeAudio[1] = BeatSound[1];
	}
}
