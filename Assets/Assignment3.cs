using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignment3 : ProcessingLite.GP21
{
    // Start is called before the first frame update
    Vector2 circleVector,inBetween,direction;
    public float speed = 0;
    float moveSpeed = 0;
    float circleDiameter = 3;
    float gravity = 0.02f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Background(0);
        gravityFall();
        TeleportCircle();
        DrawLineToMouse();
        MoveCircleTowardsMouse();
        BounceOnWalls();
    }
    private void TeleportCircle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            circleVector = new Vector2(MouseX, MouseY);
        }
        Circle(circleVector.x, circleVector.y, circleDiameter);
    }
    private void gravityFall()
    {
        float breakFriction = direction.x * Time.deltaTime / 2;
        if (circleVector.y >= 0 + circleDiameter / 2  + 0.1f && circleVector.y <= 10 - circleDiameter / 2 + 0.1f)
        {
            direction.y -= gravity * Time.deltaTime;
        }
        direction.x -= breakFriction;
    }
    private void DrawLineToMouse()
    {
        if (Input.GetMouseButton(0))
        {
            Line(circleVector.x, circleVector.y, MouseX, MouseY);
            inBetween = new Vector2(MouseX, MouseY) - circleVector;
            direction = new Vector2(0, 0);
            moveSpeed = 0;
        }
    }
    private void MoveCircleTowardsMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            moveSpeed = speed * Time.deltaTime;
            moveSpeed = Mathf.Clamp(moveSpeed, 0, 0.005f);
            direction = new Vector2(inBetween.x, inBetween.y) * moveSpeed;
        }
        circleVector += direction;
    }
    private void BounceOnWalls()
    {
        if (circleVector.x <= 0 + circleDiameter / 2 || circleVector.x >= Width - circleDiameter / 2)
        {
            direction.x *= -0.8f;
        }
        else if (circleVector.y <= 0 + circleDiameter / 2 || circleVector.y >= Height - circleDiameter / 2)
        {
            direction.y *= -0.8f;
        }
    }
}