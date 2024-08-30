using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Player;
    Vector3 velocity = Vector3.zero;
    [SerializeField] private float _followSpeed = 0.1f;


    private void Start() {
        if (Player == null) GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate() {
        MoveCameraWithPlayer();
    }

    void MoveCameraWithPlayer() {
        Vector3 playerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, _followSpeed);
    }
}