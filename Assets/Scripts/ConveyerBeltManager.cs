using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MicrophoneInput))]
public class ConveyerBeltManager : MonoBehaviour {

  [SerializeField] private GameObject[] m_Controllables;
  [SerializeField, Range(0, 30)] private float m_MinStopGraceSeconds = 2f;
  [SerializeField, Range(0, 30)] private float m_MaxStopGraceSeconds = 10f;

  private IList<IMotionControllable> m_MotionControllables;
  private MicrophoneInput m_MicrophoneInput;
  private bool m_StoppingBelt = false;

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
    m_MicrophoneInput.OnMicDeactivated += this.StopBelt;

    // Start with a stopped belt
    m_StoppingBelt = true;
    this.CommitStopBelt();
	}

  private void StartBelt() {
    m_StoppingBelt = false;
    foreach(var controllable in CurrentMotionControllables()) {
      controllable.StartMotion();
    }
  }

  private void StopBelt() {
    if(!m_StoppingBelt) {
      m_StoppingBelt = true;
      var graceSeconds = Random.Range(m_MinStopGraceSeconds, m_MaxStopGraceSeconds);
      Debug.Log("StopBelt. graceSeconds = " + graceSeconds);
      Invoke("CommitStopBelt", graceSeconds);
    }
  }

  private void CommitStopBelt() {
    if(m_StoppingBelt) {
      foreach(var controllable in CurrentMotionControllables()) {
        controllable.StopMotion();
      }  
      m_StoppingBelt = false;
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
      }
    }

    return listCopy;
  }

}
