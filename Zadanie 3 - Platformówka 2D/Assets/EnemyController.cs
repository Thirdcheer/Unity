using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public Transform groundCheck;
    public Transform frontCheck;
    bool grounded;
    int movingRight;
    private Rigidbody2D rb2d;
    private Animator anim;

    // Use this for initialization
    void Start () {
        movingRight = -1;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }


        // Update is called once per frame
        void Update () {

        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        Debug.Log(grounded);
        Debug.Log(frontHits.Length);
        rb2d.velocity = Vector2.right * movingRight;

        Debug.Log("Moving right" + movingRight);
        if (!grounded || frontHits.Length != 0)
        {
            Flip();
            frontHits = null;
        }
    }

    void Flip() {
        movingRight *= -1;
        transform.localScale.Set(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
