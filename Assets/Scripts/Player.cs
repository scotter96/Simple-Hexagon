using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isMobile = false;
    // [HideInInspector]
    public bool isInvincible = false;

    public float moveSpeed = 350f;

    public GameOver gameOver;
    public PowerManager powerManager;

    float h = 0f;

    bool inputDetected;
    // todo: Mouse analog input
    // bool isFacingMouse;
    string inputSource;

    // todo: Mobile inputs
    // public GameObject joystickPrefab;

    // todo: Mobile inputs
    // GameObject canvas, joystickObj;

    // todo: Mobile inputs
    // void Start()
    // {
    //     canvas = GameObject.Find("Canvas");
    // }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!isMobile)
        {
            if (!inputDetected)
            {
                if (Input.GetButton("Horizontal")) {
                    inputSource = "keyboard";
                    inputDetected = true;
                }
                else if (Input.GetAxisRaw("Mouse X") != 0) {
                    inputSource = "mouse";
                    inputDetected = true;
                }
                // todo: Mouse analog input
                // isFacingMouse = false;
            }
            else {
                if (!Input.anyKey && Input.GetAxisRaw("Mouse X") == 0)
                {
                    h = 0;
                    inputSource = "";
                    inputDetected = false;
                }

                if (inputSource == "keyboard")
                    h = Input.GetAxisRaw("Horizontal");
                else
                {
                    h = Input.GetAxisRaw("Mouse X");
                }
            }
        }
        // todo: Mobile inputs
        // else
        // {
        //     float firstTouchX = 0;
        //     float currentTouchX = 0;
        //     if (Input.touchCount > 0)
        //     {
        //         Touch touch = Input.GetTouch(0);
        //         Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        //         if (touch.phase == TouchPhase.Began)
        //         {
        //             firstTouchX = touchPos.magnitude;
        //             joystickObj = Instantiate (joystickPrefab, touchPos, Quaternion.identity);
        //             joystickObj.transform.SetParent (canvas.transform);
        //             Joystick joystick = joystickObj.GetComponent<Joystick>();
        //         }
        //         else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        //             currentTouchX = touchPos.magnitude;
        //         else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
        //             Destroy (joystickObj);
        //     }
        // }

        //// if (Input.GetKeyDown(KeyCode.Tab))
        ////     isMobile = !isMobile;
    }

    // void FaceMouse()
    // {
    //     Vector3 mousePos = Input.mousePosition;
    //     mousePos = Camera.main.ScreenToWorldPoint(mousePos);

    //     Vector2 direction = new Vector2 (
    //         mousePos.x - transform.position.x,
    //         mousePos.y - transform.position.y
    //     );

    //     transform.up = direction;
    // }

    void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, h * Time.fixedDeltaTime * -moveSpeed);
        // if (isFacingMouse)
        //     FaceMouse();
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Powerups")
        {
            powerManager.StartPowerup(col.name);
            Destroy (col.gameObject);
        }
        else
        {
            if (col.transform.parent.tag == "Hexagon" && !isInvincible)
                gameOver.Trigger();
        }
    }
}