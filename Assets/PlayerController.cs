using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private float moveInput;
    private float moveInputVert;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    //Jumping
    public float jumpForce;  //Douuble JUMP
    int extraJump;
    public int extraJumpValue;

    // bool isJumping=false; // Hold JUMP
    // public float jumpTime;
    // float jumpTimeCounter;

    //Climbing
    public LayerMask WhatIsLadder;
    public float climbRayDistance;
    private bool isClimbing;

    //PRIVATE
    float originalGravity;

    private Animator animator;

    [SerializeField] Transform respawnPoint;
    [SerializeField] GameObject respawnParticle;

    public AudioClip[] footSound;
    public AudioClip jumpSound, spawnSound;
    AudioSource audioSource;

    public int lvlIndex;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
     
    }
    // Start is called before the first frame update
    void Start()
    {
        //respawnPoint = transform;
        Spawn();
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        animator =  GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0){
            Flip();
        }else if(facingRight == true && moveInput <0){
            Flip();
        }

        if(moveInput != 0){
            animator.SetBool("IsWalking",true);
            if(!audioSource.isPlaying){
                audioSource.clip =footSound[UnityEngine.Random.Range(0,footSound.Length)];
                audioSource.Play();
            }
        }else{
            animator.SetBool("IsWalking",false);
        }


        //Climbing
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up , climbRayDistance , WhatIsLadder);
        
        if (hitInfo.collider != null){
            if (Input.GetKeyDown(KeyCode.UpArrow)){
                isClimbing = true;
            }
        }else{
            isClimbing = false;
        }

        if (isClimbing ==true){   
            moveInputVert = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x , moveInputVert * speed);
            rb.gravityScale = 0; 
        }else{
            rb.gravityScale = originalGravity;
        }


    }

    void Update(){

        // DOUBLE JUMP

        isGrounded = Physics2D.OverlapCircle(groundCheck.position , checkRadius , whatIsGround);

        if (isGrounded == true){
            extraJump = extraJumpValue;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump > 0){   
            rb.velocity = Vector2.up *jumpForce;
            extraJump--;
        }else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJump == 0 && isGrounded == true){
            rb.velocity = Vector2.up *jumpForce;
            animator.SetTrigger("Jump");
            audioSource.clip = jumpSound;
            audioSource.PlayOneShot(audioSource.clip);
        }


        // HOLD JUMP

        // if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)){           
        //     isJumping = true;
        //     jumpTimeCounter = jumpTime;
        //     rb.velocity = Vector2.up * jumpForce;
        // }

        // if(isJumping == true && Input.GetKey(KeyCode.Space) ){
        //     if(jumpTimeCounter > 0){
        //         rb.velocity = Vector2.up * jumpForce;
        //         jumpTimeCounter -= Time.deltaTime;
        //     }else{
        //         isJumping= false;
        //     }
        // }
        // if(Input.GetKeyUp(KeyCode.Space)){
        //     isJumping = false;
        // }

    }


    void Flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void Spawn(){
        // rb.velocity =;
        transform.position = respawnPoint.position;
        Instantiate(respawnParticle,transform.position, Quaternion.identity);
        // audioSource.clip = spawnSound;
        // audioSource.Play();
    }  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Finish")){
            Debug.Log($"LVL COMPLETE");
            SceneManager.LoadScene(lvlIndex);          
        }

        if(other.CompareTag("Kata")){
            Debug.Log($"MARA KAATA");
            Spawn();
        }
    }  

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnDisable()
    {
   
            Spawn();
   
    }
}



