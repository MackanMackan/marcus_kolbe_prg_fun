using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignment3 : ProcessingLite.GP21
{
    // Start is called before the first frame update
    Vector2 circleVector;
    Vector2 inBetween;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Background(0);
        TeleportCircle();
        DrawLineToMouse();
        MoveCircleToTowardsMouse();
    }
    private void TeleportCircle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            circleVector = new Vector2(MouseX, MouseY);
        }
        Circle(circleVector.x, circleVector.y, 3);
    }
    private void DrawLineToMouse()
    {
        if (Input.GetMouseButton(0))
        {
            Line(circleVector.x,circleVector.y,MouseX,MouseY);
            inBetween = new Vector2(MouseX, MouseY) - circleVector;
            Debug.Log(inBetween);
        }
    }
    private void MoveCircleToTowardsMouse()
    {
        if (Input.GetMouseButton(0))
        {
            circleVector = new Vector2(circleVector.x + inBetween.x / 300, circleVector.y + inBetween.y / 300);
        }
    }
}