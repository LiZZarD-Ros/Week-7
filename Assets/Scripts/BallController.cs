using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollision;
    public Rigidbody rb;
    public float impulseForce = 5f;
    private Vector3 startPos;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;

        DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
        if (deathPart)
        {
            deathPart.HitDeathPart();
        }

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);
                
    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }

    public void ResetBall()
    {
        transform.position = startPos;
    }
}
