﻿using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

[RequireComponent(typeof(Rigidbody), typeof(VRInteractiveItem))]
public class PushableItem : MonoBehaviour {

  private Camera m_sceneCamera;
  private Rigidbody m_Rigibody;
  private VRInteractiveItem m_InteractiveItem;
  private LevelManager m_LevelManager;

  [SerializeField] private float m_Force = 100f;
  [SerializeField] private AudioClip[] m_CollisionSounds;
  [SerializeField, Range(0.5f, 3f)] private float m_MinCollisionPitch = 0.9f;
  [SerializeField, Range(0.5f, 3f)] private float m_MaxCollisionPitch = 1.1f;

	// Use this for initialization
	void Start () {
    m_sceneCamera = Camera.main;
    m_Rigibody = this.GetComponent<Rigidbody>();
    m_InteractiveItem = this.GetComponent<VRInteractiveItem>();
    m_InteractiveItem.OnClick += this.Push;
    m_LevelManager = GameObject.FindObjectOfType<LevelManager>();
	}
    
  void Push() {    
    m_Rigibody.isKinematic = false;
    var direction = this.transform.forward * m_Force;
    direction = m_sceneCamera.transform.TransformDirection(direction);
    m_Rigibody.AddForce(direction);

    ScoringManager.instance.PlayerRemovedItem(this.gameObject);
    if (m_LevelManager != null) {
      m_LevelManager.BeltItemRemoved(this.gameObject.GetComponent<BeltItem>());
		}
  }

  void OnCollisionEnter() {
	if (m_CollisionSounds.Length > 0) {
  	  var randomClipIndex = Random.Range(0, m_CollisionSounds.Length - 1);
  	  var randomClip = m_CollisionSounds[randomClipIndex];
   	 SoundManager.instance.Play(randomClip, this.transform.position, m_MinCollisionPitch, m_MaxCollisionPitch);
	}
  }
}
