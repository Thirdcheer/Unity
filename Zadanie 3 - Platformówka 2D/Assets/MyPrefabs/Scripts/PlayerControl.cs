using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform fireSpawn;
    public AudioClip footsteps;
    public AudioClip jumpSound;
    public AudioClip shootSound;
    private AudioSource audioSource;
    public Slider slider;

	[HideInInspector]
	public bool facingRight = true;			
	[HideInInspector]
	public bool jump = false;				


	public float moveForce = 365f;			
	public float maxSpeed = 5f;				
	public float jumpForce = 1000f;		
    

	private Transform groundCheck;			
	private bool grounded = false;			
	private Animator anim;
    GameStats gs;

    public void OnSliderValueChanged() {
        audioSource.volume = slider.value;
    }

    private void OnLevelWasLoaded(int level) {
        gameObject.transform.position = new Vector2(0, -0.69f);
    }

    public void playTupTup() {
        audioSource.PlayOneShot(footsteps);
    }
    public void playJump() {
        audioSource.PlayOneShot(jumpSound);
    }
    public void playShoot() {
        audioSource.PlayOneShot(shootSound);
    }
    
    private void Start() {
        gs = GameObject.FindObjectOfType<GameStats>();
        audioSource = gameObject.GetComponent<AudioSource>();
        slider.value = audioSource.volume;
    }

    void Awake()
	{
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }


	void Update()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        Debug.Log("Grounded: " + grounded);
        if (Input.GetButtonDown("Jump") && grounded) {
            jump = true;
            Debug.Log("Jump!");
        }


        if (Input.GetKeyDown(KeyCode.F)) {

            Invoke("Fire", 0.25f);
            anim.SetTrigger("Shoot");
        }
    }


	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");
        
		anim.SetFloat("Speed", Mathf.Abs(h));
        
		if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h *2 * moveForce);
        
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        
		if(h > 0 && !facingRight)
			Flip();
		else if(h < 0 && facingRight)
			Flip();
        
		if(jump)
		{
			anim.SetTrigger("Jump");
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            
			jump = false;
		}

	}
	
    void Fire() {
        var fire = Instantiate(firePrefab, fireSpawn.position, fireSpawn.localRotation);
        if(facingRight)
            fire.GetComponent<Rigidbody2D>().velocity = new Vector2(2,0);
        else
            fire.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
        Destroy(fire, 0.5f);
    }
	
	void Flip ()
	{
		facingRight = !facingRight;
        
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            int dir;
            if (facingRight)
                dir = -1;
            else
                dir = 1;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(dir * 3, 0f));
            Debug.Log("Health reduced");
            gs.reduceHealth();
        }
    }
}
