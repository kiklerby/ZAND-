using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float shiftSpeed = 10f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] AudioSource characterSounds;

    bool isGrounded = true;
    float stamina = 5f;
    float currentSpeed;
    Rigidbody rb;
    Vector3 direction;
    Animator anim;
    private int health;
    public bool dead;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = movementSpeed;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            //anim.SetBool("Jump", true);
            //characterSounds.Stop();
            //AudioSource.PlayClipAtPoint(jump, transform.position);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(stamina > 0)
            {
                stamina -= Time.deltaTime;
                currentSpeed = shiftSpeed;
            }
            else
            {
            currentSpeed = movementSpeed;
            }

        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {            
                stamina += Time.deltaTime;                      
                currentSpeed = movementSpeed;
        }
        if(stamina > 5f)
        {
            stamina = 5f;
        }
        else if (stamina < 0)
        {
            stamina = 0;
        }


    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision){
        isGrounded = true;
        //anim.SetBool("Jump", false);
    }

}
