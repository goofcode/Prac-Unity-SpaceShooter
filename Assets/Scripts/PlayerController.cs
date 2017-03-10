using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveBoundary
{
    public float xmin, xmax, zmin, zmax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public MoveBoundary boundary;
    public float tilt;

    public GameObject shot;
    public Transform shotSpawn;
    public float firerate;

    private float nextfire = 0.0f;

    private Rigidbody rb;
	private AudioSource audio;


    void Start( )
    {
        rb = GetComponent<Rigidbody>( );
		audio = GetComponent<AudioSource> ();
    }

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play ();
        }
    }

    void FixedUpdate( )
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = movement*speed;
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xmin, boundary.xmax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zmin, boundary.zmax)
        );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * (-tilt));
    }
}