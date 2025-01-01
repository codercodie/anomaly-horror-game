using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private static Player instance;
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private void Awake()
    {
        // Ensure there is only one instance of the Player object
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        movement.Normalize();
        rb.velocity = movement * moveSpeed;

        //animator.SetFloat("Vertical", verticalInput);
        //animator.SetFloat("Horizontal", horizontalInput);
        //animator.SetFloat("Speed", movement.sqrMagnitude);

    }

}
