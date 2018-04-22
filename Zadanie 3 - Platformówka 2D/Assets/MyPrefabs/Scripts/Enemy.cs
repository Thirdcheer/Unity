using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float moveSpeed = 2f;		

    private Transform frontCheck;
    private Transform groundCheck;
    bool grounded;

	void Awake()
	{
        groundCheck = transform.Find("groundCheck");
        frontCheck = transform.Find("frontCheck").transform;
    }


    void Update() {
        grounded = Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Ground"));
        if (!grounded)
        {
            Flip();
        }
    }

    void FixedUpdate ()
	{
		Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);

        foreach (Collider2D c in frontHits)
		{
            Debug.Log(grounded);
			if(c.tag == "Coin" || !grounded)
			{
				Flip ();
				break;
			}
		}
        
		GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);	
	}
	
	
	public void Flip()
	{
        Vector3 enemyPos = transform.position;
		Vector3 enemyScale = transform.lossyScale;
		enemyScale.x *= -1;
        //transform.position = enemyPos;
		transform.localScale = enemyScale;
	}

    public void OnCollisionEnter2D(Collision2D collision) {
        
    }
}
