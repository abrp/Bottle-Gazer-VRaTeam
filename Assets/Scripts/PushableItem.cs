using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

[RequireComponent(typeof(Rigidbody), typeof(VRInteractiveItem))]
public class PushableItem : MonoBehaviour {

  private Rigidbody m_Rigibody;
  private VRInteractiveItem m_InteractiveItem;

  [SerializeField] private float m_Speed = 5f;

	// Use this for initialization
	void Start () {
    m_Rigibody = this.GetComponent<Rigidbody>();
    m_InteractiveItem = this.GetComponent<VRInteractiveItem>();
    m_InteractiveItem.OnClick += this.Push;
	}
    
  void Push() {
    m_Rigibody.isKinematic = false;
    //m_Rigibody.velocity = new Vector3(0, 0, m_Speed);
    m_Rigibody.AddForce(0, 0, m_Speed);
  }
}
