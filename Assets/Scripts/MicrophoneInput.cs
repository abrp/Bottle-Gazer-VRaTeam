using UnityEngine;
using System;
using System.Collections;  
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]  
public class MicrophoneInput : MonoBehaviour   {
	private const int FREQUENCY = 22000;    // Wavelength, I think.
	private const int SAMPLECOUNT = 128;   // Sample Count.
	private const float REFVALUE = 0.1f;    // RMS value for 0 dB.
	private const float THRESHOLD = 0.02f;  // Minimum amplitude to extract pitch (recieve anything)
	private const float ALPHA = 0.05f;      // The alpha for the low pass filter (I don't really understand this).

	public GameObject resultDisplay;   // GUIText for displaying results
	public GameObject blowDisplay;     // GUIText for displaying blow or not blow.
	public int recordedLength = 50;    // How many previous frames of sound are analyzed.
	public int requiedBlowTime = 4;    // How long a blow must last to be classified as a blow (and not a sigh for instance).
	public int clamp = 160;            // Used to clamp dB (I don't really understand this either).

	public float rmsValue;            // Volume in RMS
	public float dbValue;             // Volume in DB
	public float pitchValue;          // Pitch - Hz (is this frequency?)
	private int blowingTime;           // How long each blow has lasted
	public float sensitivity = 10;
	private float lowPassResults;      // Low Pass Filter result
	private float peakPowerForChannel; //

	private float[] samples;           // Samples
	private float[] spectrum;          // Spectrum
	private List<float> dbValues;      // Used to average recent volume.
	private List<float> pitchValues;   // Used to average recent pitch.
	private new AudioSource audio;

  private bool loud = false;

  [SerializeField] private GameObject m_TestObject;

  public event Action OnMicActivated; // Called when input sound goes above treshold
  public event Action OnMicDeactivated; // Called when input sound goes below treshold

	public void Start () {
		samples = new float[SAMPLECOUNT];
		spectrum = new float[SAMPLECOUNT];
		dbValues = new List<float>();
		pitchValues = new List<float>();
		audio = GetComponent<AudioSource> ();
		StartMicListener();
	}

	public void Update () {

		// If the audio has stopped plaing, this will restart the mic play the clip.
		if (!audio.isPlaying) {
			StartMicListener();
		}
	
		// Gets volume and pitch values
		AnalyzeSound();

		// Runs a series of algorithms to decide whether a blow is occuring.
		DeriveBlow();

		//m_TestObject.transform.position = new Vector3 (0,dbValue,1.75f);
		if (dbValue < sensitivity) {
			if (m_TestObject != null) {
				m_TestObject.GetComponent<Renderer> ().material.color = new Color (1, 0, 0);
			}
      if(loud == true && OnMicActivated != null) {
        OnMicActivated();
      }
			loud = false;
		} else {
			if (m_TestObject != null) {
				m_TestObject.GetComponent<Renderer> ().material.color = new Color (0, 1, 0);
			}
      if(loud == false && OnMicDeactivated != null) {
        OnMicDeactivated();
      }
			loud = true;
		}
	}

	/// Starts the Mic, and plays the audio back in (near) real-time.
	private void StartMicListener() {
		audio.clip = Microphone.Start("Built-in Microphone", true, 999, FREQUENCY);
		// HACK - Forces the function to wait until the microphone has started, before moving onto the play function.
		while (!(Microphone.GetPosition("Built-in Microphone") > 0)) {
		} audio.Play();
	}

	/// Credits to aldonaletto for the function, http://goo.gl/VGwKt
	/// Analyzes the sound, to get volume and pitch values.
	private void AnalyzeSound() {

		// Get all of our samples from the mic.
		audio.GetOutputData(samples, 0);

		// Sums squared samples
		float sum = 0;
		for (int i = 0; i < SAMPLECOUNT; i++){
			sum += Mathf.Pow(samples[i], 2);
		}

		// RMS is the square root of the average value of the samples.
		rmsValue = Mathf.Sqrt(sum / SAMPLECOUNT);
		dbValue = 20 * Mathf.Log10(rmsValue / REFVALUE);

		// Clamp it to {clamp} min
		if (dbValue < -clamp) {
			dbValue = -clamp;
		}

		// Gets the sound spectrum.
		audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
		float maxV = 0;
		int maxN = 0;

		// Find the highest sample.
		for (int i = 0; i < SAMPLECOUNT; i++){
			if (spectrum[i] > maxV && spectrum[i] > THRESHOLD){
				maxV = spectrum[i];
				maxN = i; // maxN is the index of max
			}
		}

		// Pass the index to a float variable
		float freqN = maxN;

		// Interpolate index using neighbours
		if (maxN > 0 && maxN < SAMPLECOUNT - 1) {
			float dL = spectrum[maxN-1] / spectrum[maxN];
			float dR = spectrum[maxN+1] / spectrum[maxN];
			freqN += 0.5f * (dR * dR - dL * dL);
		}

		// Convert index to frequency
		pitchValue = freqN * 24000 / SAMPLECOUNT;
	}

	private void DeriveBlow() {

		UpdateRecords(dbValue, dbValues);
		UpdateRecords(pitchValue, pitchValues);

		// Find the average pitch in our records (used to decipher against whistles, clicks, etc).
		float sumPitch = 0;
		foreach (float num in pitchValues) {
			sumPitch += num;
		}
		sumPitch /= pitchValues.Count;

		// Run our low pass filter.
		lowPassResults = LowPassFilter(dbValue);

		// Decides whether this instance of the result could be a blow or not.
		if (lowPassResults > -30 && sumPitch == 0) {
			blowingTime += 1;
		} else {
			blowingTime = 0;
		}

		// Once enough successful blows have occured over the previous frames (requiredBlowTime), the blow is triggered.
		// This example says "blowing", or "not blowing", and also blows up a sphere.
	}

	// Updates a record, by removing the oldest entry and adding the newest value (val).
	private void UpdateRecords(float val, List<float> record) {
		if (record.Count > recordedLength) {
			record.RemoveAt(0);
		}
		record.Add(val);
	}

	/// Gives a result (I don't really understand this yet) based on the peak volume of the record
	/// and the previous low pass results.
	private float LowPassFilter(float peakVolume) {
		return ALPHA * peakVolume + (1.0f - ALPHA) * lowPassResults;
	}
}
