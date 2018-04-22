using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			
	[HideInInspector]
	public bool jump = false;				


	public float moveForce = 365f;			
	public float maxSpeed = 5f;				
	public AudioClip[] jumpClips;			
	public float jumpForce = 1000f;			

	private Transform groundCheck;			
	private bool grounded = false;			
	private Animator anim;
    GameStats gs;

    private void OnLevelWasLoaded(int level) {
        gameObject.transform.position = new Vector2(0, 0);
    }

    private void Start() {
        gs = GameObject.FindObjectOfType<GameStats>();
    }
    void Awake()
	{
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
	}


	void Update()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
        
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}


	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");
        
		anim.SetFloat("Speed", Mathf.Abs(h));
        
		if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
        
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        
		if(h > 0 && !facingRight)
			Flip();
		else if(h < 0 && facingRight)
			Flip();
        
		if(jump)
		{
			anim.SetTrigger("Jump");
            
			int i = Random.Range(0, jumpClips.Length);
			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
            
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            
			jump = false;
		}
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
            GetComponent<Rigidbody2D>().AddForce(new Vector2(dir * 3000, 0f));
            Debug.Log("Health reduced");
            gs.reduceHealth();
        }
    }
}
