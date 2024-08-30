using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Player sounds
    [SerializeField] private AudioClip _playerMoveSound;
    [SerializeField] private AudioClip _playerLandSound;
    [SerializeField] private AudioClip __playerDeath;

    public float playerMoveSpeed = 0.05f;
    public bool _flying;

    private void Start() {
        _flying = false;
    }

    private void Update() {
        if(!_flying) {
            if (Input.GetKeyDown(KeyCode.W)) FindLandingPoint(Vector2.up);
            if (Input.GetKeyDown(KeyCode.S)) FindLandingPoint(Vector2.down);
            if (Input.GetKeyDown(KeyCode.D)) FindLandingPoint(Vector2.right);
            if (Input.GetKeyDown(KeyCode.A)) FindLandingPoint(Vector2.left);
        }
    }

    void FindLandingPoint(Vector2 direction) {
        GameManager.Instance.SFXPlayer.PlayOneShot(_playerMoveSound, Random.Range(0.9f, 1.1f));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        if(hit.collider != null) {
            _flying=true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, DirectionToHeading(direction)));
            Vector2 endPos = hit.point - (new Vector2(0.5f, 0.5f) * direction);
            StartCoroutine(LerpMe(transform.position, endPos, playerMoveSpeed));
        }
    }

    IEnumerator LerpMe(Vector2 start, Vector2 end, float duration) {
        float time = 0;
        while (time < duration) {
            transform.position = Vector2.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.SFXPlayer.PlayOneShot(_playerLandSound);
        _flying = false;
        transform.position = end;
    }

    float DirectionToHeading(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = (angle + 90) % 360; // Adjusting to make up = 180, down = 0
        if (angle < 0) angle += 360; // Ensure the angle is positive
        return angle;
    }
}