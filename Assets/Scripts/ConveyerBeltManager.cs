using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRStandardAssets.Utils;

[RequireComponent(typeof(MicrophoneInput))]
public class ConveyerBeltManager : MonoBehaviour {

  [SerializeField] private GameObject[] m_Controllables;
  [SerializeField, Range(0, 30)] private float m_MinStopGraceSeconds = 2f;
  [SerializeField, Range(0, 30)] private float m_MaxStopGraceSeconds = 10f;
  [SerializeField] private GameObject m_TriggerObject;
  [SerializeField] private GameObject[] m_StartedSounds;
  [SerializeField] private GameObject[] m_StoppedSounds;

  private IList<IMotionControllable> m_MotionControllables;
  private MicrophoneInput m_MicrophoneInput;

	// Use this for initialization
	void Start () {      
    // Find all things to motion control from non belt items
    m_MotionControllables = new List<IMotionControllable>();
    foreach(var controllable in m_Controllables) {
      var scripts = controllable.GetComponents<IMotionControllable>();
      foreach(var script in scripts) {
        m_MotionControllables.Add(script);
      }
    }
 
    // Hook up to microphone
    m_MicrophoneInput = this.GetComponent<MicrophoneInput>();
    m_MicrophoneInput.OnMicActivated += this.StartBelt;

    // Hook up to trigger object if one is given
    if(m_TriggerObject != null) {
      m_TriggerObject.GetComponent<VRInteractiveItem>().OnClick += this.StartBelt;
    }

    // Belt starts running, stop after random time when Henry falls asleep
    StopBeltAtRandomTime();
	}

  private void StartBelt() {
    foreach(var controllable in CurrentMotionControllables()) {
      controllable.StartMotion();
    }
    ChangeSounds(true);
    StopBeltAtRandomTime();
  }

  private void StopBeltAtRandomTime() {
    CancelInvoke("StopBeltNow");
    var graceSeconds = Random.Range(m_MinStopGraceSeconds, m_MaxStopGraceSeconds);
    Invoke("StopBeltNow", graceSeconds);
  }

  private void StopBeltNow() {
    foreach(var controllable in CurrentMotionControllables()) {
      controllable.StopMotion();
    }
    ChangeSounds(false);
  }

  private IList<IMotionControllable> CurrentMotionControllables() {
    var listCopy = new List<IMotionControllable>(m_MotionControllables);

    // Find all things to motion control from belt items
    var instances = GameObject.FindObjectsOfType<BeltItem>();
    foreach(var instance in instances) {
      var scriptList = new List<IMotionControllable>();
      scriptList.AddRange(instance.GetComponents<IMotionControllable>());
      scriptList.AddRange(instance.GetComponentsInChildren<IMotionControllable>());
      foreach(var script in scriptList) {
        listCopy.Add(script);
      }
    }

    return listCopy;
  }

  private void ChangeSounds(bool beltIsStarted) {
    foreach(var instance in m_StoppedSounds) {
      var source = instance.GetComponent<AudioSource>();
      if(beltIsStarted) {
        source.Stop();
      } else {
        source.Play();
      }
    }

    foreach(var instance in m_StartedSounds) {
      var source = instance.GetComponent<AudioSource>();
      if(beltIsStarted) {
        source.Play();
      } else {
        source.Stop();
      }
    }
  }

}
