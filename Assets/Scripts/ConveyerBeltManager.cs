using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MicrophoneInput))]
public class ConveyerBeltManager : MonoBehaviour {

  [SerializeField] private GameObject[] m_Controllables;

  private IList<IMotionControllable> m_MotionControllables;
  private MicrophoneInput m_MicrophoneInput;

	// Use this for initialization
	void Start () {
    // Find all things to motion control
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
    m_MicrophoneInput.OnMicDeactivated += this.StopBelt;

    // Start with a stopped belt
    this.StopBelt();
	}

  private void StartBelt() {
    foreach(var controllable in m_MotionControllables) {
      controllable.StartMotion();
    }
  }

  private void StopBelt() {
    foreach(var controllable in m_MotionControllables) {
      controllable.StopMotion();
    }
  }

}
