using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollision;
    public Rigidbody rb;
    public float impulseForce = 5f;
    private Vector3 startPos;

    public int perfectPass = 0;
    public bool isSuperSpeedActive;

    public AudioClip swooshSound;
    public AudioClip hitSound;
    private AudioSource playerAudio;


    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
            playerAudio.PlayOneShot(swooshSound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;

        if (isSuperSpeedActive)
        {
            if (!collision.transform.GetComponent<Goal>())
            {
                Destroy(collision.transform.parent.gameObject, 0.3f);
                Debug.Log("Destroying platform");
            }
        }
        else
        {
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
            {
                deathPart.HitDeathPart();
            }
        }

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
        rb.AddTorque(Vector3.right, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);
        
        perfectPass = 0;
        isSuperSpeedActive = false;

        playerAudio.PlayOneShot(hitSound);
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
