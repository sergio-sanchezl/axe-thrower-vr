using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoStereoSwitcher : MonoBehaviour {

	public bool stereoActivated;
	void Start () {
		this.stereoActivated = (PlayerPrefs.GetInt("stereo_mode", 1) == 1);
		ChangeMonoStereo(this.stereoActivated);
	}
	
	public bool ToggleMonoStereo() {
		ChangeMonoStereo(!stereoActivated);
		return stereoActivated;
	}

	void ChangeMonoStereo(bool value) {
		this.stereoActivated = value;
		PlayerPrefs.SetInt("stereo_mode", value ? 1 : 0);
		AudioConfiguration audioConfiguration = AudioSettings.GetConfiguration();
		audioConfiguration.speakerMode = stereoActivated ? AudioSpeakerMode.Stereo : AudioSpeakerMode.Mono;
		AudioSettings.Reset(audioConfiguration);
	}
}
