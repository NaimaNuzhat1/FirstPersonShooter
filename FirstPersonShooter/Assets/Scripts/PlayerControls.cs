
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    //public InputAction playerInput;
     private PlayerMovement playerInput;
    //public Animator animation;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 6.0f;
    public GameObject player;
    [SerializeField] GameObject healthbar;
    Image healthB;
    public Slider slider;
    private float gravityValue = -9.81f;
    [SerializeField] GameObject hitImage;




    #region Singleton
    public static PlayerControls instance;

    private void Awake()
    {
        instance = this;
        playerInput = new PlayerMovement();
        controller = gameObject.GetComponent<CharacterController>();
        healthB = healthbar.GetComponent<Image>();
        slider = healthbar.GetComponentInChildren<Slider>();
        //animation = gameObject.GetComponent<Animator>();
    }
    #endregion

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            //GetComponent<Animation>().SetBool("isWalking", false);
            playerVelocity.y = 0f;
        }

        Vector2 movementInput=playerInput.Touch.Movement.ReadValue<Vector2>();
        Vector3 move =transform.right *movementInput.x+transform.forward*movementInput.y;
        if(!move.Equals(Vector3.zero))
        {
            //GetComponent<Animation>().SetBool("isWalking", true);
        }
        controller.Move(move * Time.deltaTime * playerSpeed);

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name=="EnemyBullet(Clone)")
        {
            slider.value--;
            StartCoroutine(hit());
            if (slider.value <= 0)
            {
                StartCoroutine(die());
            }

        } 
   
    }

    IEnumerator hit()
    {
        hitImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitImage.SetActive(false);
    }

    IEnumerator die()
    {
        //GetComponent<Animation>().SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("isDead", 0);

    }


}
