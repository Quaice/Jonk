using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public SFXController SFXPlayer {  get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(Instance);
        }
    }

    public void Start() {
        SFXPlayer = gameObject.AddComponent<SFXController>();
    }
}