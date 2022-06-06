using UnityEngine;

// moving the player around the playing area using the W, S, A, D buttons, using physics on objects in the project

public class Move : MonoBehaviour
{
    [SerializeField] private float _speed;  // player speed field
    [SerializeField] private float _sensitivity;  // camera sensitivity field

    public GameObject Player;
    private float _mainSpeed = 1.0f;  // stable speed
    private float _maxSpeed = 20.0f;  // maximum speed when holding down the shift key
    private float _shiftAdd = 5.0f;  // acceleration when pressing the shift key
    private float _camSensitive = 0.001f;  // camera sensitivity
    private Vector3 _lastMouse = new Vector3(255, 255, 255);  // rotate the camera with the mouse
    private float _totalRun = 1.0f;  // braking


    void Update()  // rotate camera based on mouse cursor location
    {
        _lastMouse = Input.mousePosition - _lastMouse;
        _lastMouse = new Vector3(-_lastMouse.y * _camSensitive * _sensitivity, _lastMouse.x * _camSensitive * _sensitivity, 0);
        _lastMouse = new Vector3(Player.transform.eulerAngles.x + _lastMouse.x, Player.transform.eulerAngles.y + _lastMouse.y, 0);
        Player.transform.eulerAngles = _lastMouse;
        _lastMouse = Input.mousePosition;

        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _totalRun += Time.deltaTime;
            p = p * _totalRun * _shiftAdd;
            p.x = Mathf.Clamp(p.x, -_maxSpeed * _speed, _maxSpeed * _speed);
            p.y = Mathf.Clamp(p.y, -_maxSpeed * _speed, _maxSpeed * _speed);
            p.z = Mathf.Clamp(p.z, -_maxSpeed * _speed, _maxSpeed * _speed);
        }
        else
        {
            _totalRun = Mathf.Clamp(_totalRun * 0.5f, 1f, 1000f);
            p = p * _mainSpeed * _speed;
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = Player.transform.position;
        if (Input.GetKey(KeyCode.Space))
        {
            Player.transform.Translate(p);
            newPosition.x = Player.transform.position.x;
            newPosition.z = Player.transform.position.z;
            Player.transform.position = newPosition;
        }
        else
        {
            Player.transform.Translate(p);
        }
    }

    private Vector3 GetBaseInput()  // player movement when W, S, A, D buttons are pressed
    {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
