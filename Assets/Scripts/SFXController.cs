using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour {

    private AudioSource SFX;

    private void Start() {
        SFX = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip) {
        SFX.PlayOneShot(clip, 1f);
    }

    public void PlayOneShot(AudioClip clip, float pitch) {
        StartCoroutine(PlayPitchedSound(clip, pitch));
    }

    IEnumerator PlayPitchedSound(AudioClip clip, float pitch) {
        SFX.pitch = pitch;
        SFX.PlayOneShot(clip, 1f);
        yield return new WaitForSeconds(clip.length);
        SFX.pitch = 1f;
    }
}