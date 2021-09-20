using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignment4 : ProcessingLite.GP21
{
    // Start is called before the first frame update
    private float speed;
    public float baseSpeed;
    public float acceleration;
    public float deacceleration;
    public float maxSpeed = 15;
    Vector2 characterPos = new Vector2(1, 1);
    Vector2 character2Pos = new Vector2(1, 1);
    Vector2 continousDirection = new Vector2(0, 0);
    void Start()
    {
        //character2Pos = new Vector2(Width / 2 , Height / 2);
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Background(0);
        moveCharacter();
       // moveOtherCharacter();
    }
    public void moveOtherCharacter()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input.magnitude!= 0)
        {
            characterPos.x += Input.GetAxis("Horizontal") * baseSpeed * Time.deltaTime;
            characterPos.y += Input.GetAxis("Vertical") * baseSpeed * Time.deltaTime;
        }
        Circle(characterPos.x, characterPos.y, 0.5f);
    }
    public void moveCharacter()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();
        if (input.magnitude != 0 && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            character2Pos.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            character2Pos.y += Input.GetAxis("Vertical") * speed * Time.deltaTime;
            speed += acceleration * Time.deltaTime;

            if (character2Pos.magnitude > maxSpeed)
            {
                speed =  maxSpeed;
            }
            continousDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            continousDirection.Normalize();
        }
        else if(character2Pos.magnitude == 0)
        {
           speed = baseSpeed;
        }
        else
        {
            speed -= deacceleration * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 20);
            character2Pos.x += continousDirection.x * speed * Time.deltaTime;
            character2Pos.y += continousDirection.y * speed * Time.deltaTime;
        }
        Circle(character2Pos.x, character2Pos.y, 0.5f);
    }
}
