using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Player player;
    Rigidbody rb;
    Vector3 initialPosition;
    bool _isFirePressed = false;
    // forward - right - back - left
    bool[] _isMovePressed = {false, false, false, false};
    bool[] _isTurnPressed = {false, false};
    bool _isRPressed = false;

    float xSpeed = 20f;
    float zSpeed = 20f;
    float ySpeedRate = 80f; 

    private Dictionary<PlayerActions, KeyCode> keyMap = new Dictionary<PlayerActions, KeyCode>();

    void SetKeyBoardLayout() {
        keyMap.Add(PlayerActions.Left, KeyCode.A);
        keyMap.Add(PlayerActions.Right, KeyCode.D);
        keyMap.Add(PlayerActions.Forward, KeyCode.W);
        keyMap.Add(PlayerActions.Back, KeyCode.S);
        keyMap.Add(PlayerActions.Fire, KeyCode.Space);
        keyMap.Add(PlayerActions.Respawn, KeyCode.R);
        keyMap.Add(PlayerActions.TurnLeft, KeyCode.Q);
        keyMap.Add(PlayerActions.TurnRight, KeyCode.E);
    }

    void ListenButtons() {
        _isMovePressed[0] = Input.GetKey(keyMap[PlayerActions.Forward]);
        _isMovePressed[1] = Input.GetKey(keyMap[PlayerActions.Right]);
        _isMovePressed[2] = Input.GetKey(keyMap[PlayerActions.Back]);
        _isMovePressed[3] = Input.GetKey(keyMap[PlayerActions.Left]);
        _isFirePressed = Input.GetKeyDown(keyMap[PlayerActions.Fire]);
        _isRPressed = Input.GetKey(keyMap[PlayerActions.Respawn]);
        _isTurnPressed[0] = Input.GetKey(keyMap[PlayerActions.TurnLeft]);
        _isTurnPressed[1] = Input.GetKey(keyMap[PlayerActions.TurnRight]);
    }

    void MoveHandler() {
        float stepForce = 1.0f * Time.fixedDeltaTime;
        float mainForce = 1.0f * Time.fixedDeltaTime;

        if (_isMovePressed[1] && _isMovePressed[3] || !_isMovePressed[1] && !_isMovePressed[3]) {
            stepForce = 0;
        }
        else if (_isMovePressed[1]) {
            stepForce *= xSpeed * -1;
        }
        else if (_isMovePressed[3]) {
            stepForce *= xSpeed;
        }

        if (_isMovePressed[0] && _isMovePressed[2] || !_isMovePressed[0] && !_isMovePressed[2]) {
            mainForce = 0;
        }
        else if (_isMovePressed[0]) {
            mainForce *= zSpeed;
        }
        else if (_isMovePressed[2]) {
            mainForce *= zSpeed * -1;
        }

        if (stepForce != 0) {
            rb.MovePosition(transform.position + Vector3.left * stepForce);
        }
        if (mainForce != 0) {
            rb.MovePosition(transform.position + Vector3.forward * mainForce);
        }
    }

    void TurnHandler() {
        float turnRate = 1.0f * Time.fixedDeltaTime;

        if (_isTurnPressed[0] && _isTurnPressed[1] || !_isTurnPressed[0] && !_isTurnPressed[1]) {
            turnRate = 0;
        }
        else if (_isTurnPressed[0]) {
            turnRate *= -ySpeedRate;
        }
        else if (_isTurnPressed[1]) {
            turnRate *= ySpeedRate;
        }

        if (turnRate != 0) {
            Quaternion deltaRotation = Quaternion.Euler(Vector3.up * turnRate);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

    void ExtraButtonsHandler() {
        if (_isRPressed) {
            RespawnAtCheckpoint();
        }
        if (_isFirePressed) {
            player.PizzaShotInit();
        }
    }

    public void RespawnAtCheckpoint() {
        RespawnAt(initialPosition);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        initialPosition = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        SetKeyBoardLayout();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ListenButtons();
        ExtraButtonsHandler();
        MoveHandler();
        TurnHandler();
    }

    public void RespawnAt(Vector3 newPosition) {
        transform.rotation = Quaternion.identity;
        transform.position = newPosition;        
    }
}
