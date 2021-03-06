using UnityEngine;
using System.Collections;

public class FlickerSprite : MonoBehaviour {
    public Color c1;
    public Color c2;
    //public Material mat;
    public float cycleHz; // Hz, the mesurement of cycles.
    private SpriteRenderer _spriteRenderer;
    private int updateCounter = 0;
    private bool swap = false;
    private TouchScreenKeyboard keyboard;

    float dtime = 0; // delta time

    void Awake() {
    	_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //Application.targetFrameRate = 60;
    }
    // Update is called once per frame

    void Start() {
    	_spriteRenderer.color = c1;
    }

    void Update() {
    	//MakeFlicker();
        //Debug.Log("Seconds: " + Time.time.ToString() + " Flashes: " + updateCounter.ToString() + "Hz expected: " + cycleHz.ToString() + " Hz actual: " + (updateCounter / Time.time).ToString());
    	//Debug.Log((1/Time.deltaTime).ToString());
    }

    public void MakeFlicker() {
        // update frequency time-step
        dtime += Time.deltaTime;

        // Sample the waveform at a specific time.
        float wave = Mathf.Sin( (dtime * 2.0f * Mathf.PI) * cycleHz);

        //print (dtime);
        //print (wave);

        // Cycle between sprites based on the waveform.
        if (wave > 0.0f) {
            _spriteRenderer.color = c1;
            // if (swap) {
            //     updateCounter++;
            //     swap = false;
            // }
            //print ("White");
        } else {
            _spriteRenderer.color = c2;
            // swap = true;
            //print ("Black");
        }

        // prevents dtime from climbing to infinity,
        // by stepping it back in the waveform to a point
        // of equal value.
        if (wave == 0.0f) {
            dtime = 0.0f;
        }
    }

    public void SetHz(float newHz) {
        if (newHz >= 0) {
            cycleHz = newHz;
        }
    }

    public void SetHzString(string newHzString) {
        if (newHzString.Length > 0) {
            SetHz(float.Parse(newHzString));
        }
    }

}
