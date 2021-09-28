using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ProcessingLite.GP21
{
    public float speed;
    public float maxSpeed = 15;
    float size = 0.5f;
    Vector2 position;
    Vector2 velocity;
    Vector2 acceleration;
    Vector2 deacceleration;
    public float deaccValue;

    
    // Start is called before the first frame update
    void Start()
    {
        position = new Vector2(Width/2, Height/2);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void MoveCharacter()
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

        position.x = (position.x + Width) % Width;
        position.y = (position.y + Height) % Height;

        position += velocity;
        Fill(0,0,0);
        Circle(position.x, position.y, size);
    }
    public float GetPlayerSize()
    {
        return size;
    }
    public Vector2 GetPlayerPosition()
    {
        return position;
    }
}
