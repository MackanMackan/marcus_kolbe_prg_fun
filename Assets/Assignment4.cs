using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignment4 : ProcessingLite.GP21
{
    // Start is called before the first frame update
    private float speed;
    public float baseSpeed;
    public float maxSpeed = 15;
    Vector2 characterPos = new Vector2(1, 1);
    Vector2 character2Pos = new Vector2(1, 1);
    Vector2 velocity;
    Vector2 acceleration;
    Vector2 deacceleration;
    float deaccX;
    float deaccY;
    public float deaccValue;
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
        if (input.magnitude != 0)
        {
            characterPos.x += Input.GetAxis("Horizontal") * baseSpeed * Time.deltaTime;
            characterPos.y += Input.GetAxis("Vertical") * baseSpeed * Time.deltaTime;
        }
        Circle(characterPos.x, characterPos.y, 0.5f);
    }
    public void moveCharacter()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();
        float x = input.x * speed * Time.deltaTime;
        float y = input.y * speed * Time.deltaTime;

        acceleration = new Vector2(x, y);
        velocity += acceleration * Time.deltaTime;


        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        if (acceleration == Vector2.zero)
        {
            deacceleration = new Vector2(1 + deaccValue * 0.001f, 1 + deaccValue * 0.001f);
            velocity /= deacceleration;
            if (velocity.magnitude < 0.0001f)
            {
                velocity *= 0;
            }
        }

        character2Pos.x = (character2Pos.x + Width) % Width;
        character2Pos.y = (character2Pos.y + Height) % Height;

        character2Pos += velocity;
        Circle(character2Pos.x, character2Pos.y, 0.5f);
    }
}
