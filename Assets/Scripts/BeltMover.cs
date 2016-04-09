using UnityEngine;
using System.Collections;

public class BeltMover : MonoBehaviour, IMotionControllable {

  [SerializeField] private float m_ScrollSpeed = 0.5f;
  [SerializeField] private bool u = false;
  [SerializeField] private bool v = false;

  private bool m_IsInMotion = true;
  private int tick = 0;

	// Update is called once per frame
	void Update () {
    if(!m_IsInMotion) {
      return;
    }

    tick++;
    if(tick % 2 != 0) {
      return;
    }

    var offset = Time.time * m_ScrollSpeed % 1;
    if(u && v) {
      this.GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", new Vector2(offset,offset));
    } else if (u) {
      GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", new Vector2(offset,0));
    }
    else if (v)
    {
      GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", new Vector2(0,offset));
    }

    tick = tick % 50;
	}

  // Sets the object in motion
  public void StartMotion() {
    m_IsInMotion = true;
  }

  // Halts the object motion
  public void StopMotion() {
    m_IsInMotion = false;
  }
}
