using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    // Win condition
    private bool[] levelsEnded = new bool[3];
    private bool haveIWin = false;
    public RawImage youWinImage;

    public RawImage pauseImage;
    private bool isPauseActive = false;
    
    private float timer = 0f;

    private Vector3 beginPosition = new Vector3(0,0,0);
    private Vector3 startPosition;
    public int life = 3;
    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private float jumpForce = 10f;
    private Rigidbody rigidbody;
    private int jumpStack = 2;
    float timeLeft = 5.0f;

    public Camera camera;

    // Manage UI
    public RawImage life1;
    public RawImage life2;
    public RawImage life3;

    public Text jumpAvailable;

    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        LaunchGame();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void LaunchGame()
    {
        timer = 0f;
        youWinImage.gameObject.SetActive(false);
        pauseImage.gameObject.SetActive(false);
        levelsEnded[0] = false;
        levelsEnded[1] = false;
        levelsEnded[2] = false;
        startPosition = beginPosition;
    }

    // Update is called once per frame
    void Update()
    {
        WinCondition();
        LooseByFalling();
        CheckLife();
        UpdateUI();
        Timer();

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            if (jumpStack < 2)
            {
                timeLeft = 3;
                jumpStack += 1;
            }
        } else
        {
            timeLeft -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (haveIWin == true)
            {
                LaunchGame();
                return;
            } else
            {
                isPauseActive = !isPauseActive;
                pauseImage.gameObject.SetActive(isPauseActive);
            }
        }

        if (haveIWin == true)
        {
            return;
        }

        // Go forward
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed * 5);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
        }
        // Turn left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Rotate(transform.up * -10f * Time.fixedDeltaTime);
            }
            else
            {
                transform.Rotate(transform.up * -50f * Time.fixedDeltaTime);
            }
        }

        // Go backward
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        // Turn right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Rotate(transform.up * 10f * Time.fixedDeltaTime);
            } else
            {
                transform.Rotate(transform.up * 50f * Time.fixedDeltaTime);
            }
            
        }

        // Move the camera up and down
        if (Input.GetKey(KeyCode.W))
        {
            camera.transform.Rotate(Vector3.right * 30f * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            camera.transform.Rotate(Vector3.left * 30f * Time.fixedDeltaTime);
        }

        // Go backward
        if (Input.GetKeyDown(KeyCode.Space) && (timeLeft <= 0 || jumpStack >= 1))
        {
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
            timeLeft = 3;
            jumpStack -= 1;
        }
    }

    private void CheckLife()
    {
        if (life == 0)
        {
            transform.position = startPosition;
            life += 3;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyCylinder"))
        {
            life -= 1;
            rigidbody.AddForce(Vector3.left * 10, ForceMode.VelocityChange);
        }
        if (collision.gameObject.CompareTag("EnemySphere"))
        {
            life -= 1;
            rigidbody.AddForce(Vector3.back * 10, ForceMode.VelocityChange);
        }
        if (collision.gameObject.CompareTag("Level1End") || collision.gameObject.CompareTag("Level2End") || collision.gameObject.CompareTag("Level3End"))
        {
            startPosition = collision.transform.position + new Vector3(0, 5, 0);
            if (collision.gameObject.CompareTag("Level1End"))
            {
                levelsEnded[0] = true;
            }
            if (collision.gameObject.CompareTag("Level2End"))
            {
                levelsEnded[1] = true;
            }
            if (collision.gameObject.CompareTag("Level3End"))
            {
                levelsEnded[2] = true;
            }
        }

        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                //collision was from below
                jumpStack = 2;
            }
        }
        
    }

    private void LooseByFalling()
    {
        if (transform.position.y <= 0)
        {
            life = 0;
        }
    }

    private void UpdateUI()
    {
        // Manage life display
        if (life == 3)
        {
            life1.gameObject.SetActive(true);
            life2.gameObject.SetActive(true);
            life3.gameObject.SetActive(true);
        }
        if (life == 2)
        {
            life1.gameObject.SetActive(false);
            life2.gameObject.SetActive(true);
            life3.gameObject.SetActive(true);
        }
        if (life == 1)
        {
            life1.gameObject.SetActive(false);
            life2.gameObject.SetActive(false);
            life3.gameObject.SetActive(true);
        }

        // Manage Jump display
        jumpAvailable.text = jumpStack.ToString();
    }

    private void WinCondition()
    {
        if (levelsEnded[0] == true && levelsEnded[1] == true && levelsEnded[2] == true)
        {
            haveIWin = true;
        }
        if (haveIWin == true)
        {
            youWinImage.gameObject.SetActive(true);
        }
    }

    private void Timer()
    {
        if (!isPauseActive)
        {
            timer += Time.deltaTime;
        }

        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerText.text = niceTime;
    }
}
