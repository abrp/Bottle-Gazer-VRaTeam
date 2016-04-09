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
    // Find all things to motion control from non belt items
    m_MotionControllables = new List<IMotionControllable>();
    foreach(var controllable in m_Controllables) {
      var scripts = controllable.GetComponents<IMotionControllable>();
      foreach(var script in scripts) {
        m_MotionControllables.Add(script);
        Debug.Log("Added script " + script.GetType().ToString());
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
    foreach(var controllable in CurrentMotionControllables()) {
      controllable.StartMotion();
    }
  }

  private void StopBelt() {
    foreach(var controllable in CurrentMotionControllables()) {
      Debug.Log("Stopping motion on " + controllable.GetType().ToString());
      controllable.StopMotion();
    }
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
        Debug.Log("Added script " + script.GetType().ToString());
      }
    }

    return listCopy;
  }

}
