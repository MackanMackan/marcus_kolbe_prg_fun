using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : ProcessingLite.GP21
{
    //Our class variables
    Vector2 position; //Ball position
    Vector2 velocity; //Ball direction
    float size = 0.5f;
    Vector3 colorRGB = new Vector3(255, 0, 0);
    //Ball Constructor, called when we type new Ball(x, y);
    private void Update()
    {
    }
    public Ball(float x, float y)
    {
        //Set our position when we create the code.
        position = new Vector2(x, y);

        //Create the velocity vector and give it a random direction.
        velocity = new Vector2();
        velocity.x = Random.Range(0, 11) - 5;
        velocity.y = Random.Range(0, 11) - 5;
    }

    //Draw our ball
    public void Draw()
    {
        Stroke(255, 0, 0);
        Circle(position.x, position.y, size);
    }

    //Update our ball
    public void UpdatePos()
    {
        position += velocity * Time.deltaTime;
    }
    //Check collision, 2 circles
    //x1, y1 is the position of the first circle
    //size1 is the radius of the first circle
    //then the same data for circle number two

    //function will return true (collision) or false (no collision)
    //bool is the return type

    bool CircleCollision(Ball ball1, Ball ball2)
    {
        float maxDistance = ball1.size + ball2.size;

        //first a quick check to see if we are too far away in x or y direction
        //if we are far away we don't collide so just return false and be done.
        if (Mathf.Abs(ball1.position.x - ball2.position.x) > maxDistance || Mathf.Abs(ball1.position.y - ball2.position.y) > maxDistance)
        {
            return false;
        }
        //we then run the slower distance calculation
        //Distance uses Pythagoras to get exact distance, if we still are to far away we are not colliding.
        else if (Vector2.Distance(new Vector2(ball1.position.x, ball1.position.y), new Vector2(ball2.position.x, ball2.position.y)) > maxDistance)
        {
            return false;
        }
        //We now know the points are closer then the distance so we are colliding!
        else
        {
            return true;
        }
    }
    public bool CircleCollision(Ball ball1, Player ball2)
    {
        float maxDistance = ball1.size + ball2.GetPlayerSize();

        //first a quick check to see if we are too far away in x or y direction
        //if we are far away we don't collide so just return false and be done.
        if (Mathf.Abs(ball1.position.x - ball2.GetPlayerPosition().x) > maxDistance || Mathf.Abs(ball1.position.y - ball2.GetPlayerPosition().y) > maxDistance)
        {
            return false;
        }
        //we then run the slower distance calculation
        //Distance uses Pythagoras to get exact distance, if we still are to far away we are not colliding.
        else if (Vector2.Distance(new Vector2(ball1.position.x, ball1.position.y), new Vector2(ball2.GetPlayerPosition().x, ball2.GetPlayerPosition().y)) > maxDistance)
        {
            return false;
        }
        //We now know the points are closer then the distance so we are colliding!
        else
        {
            return true;
        }
    }
    public void BounceOnScreen()
    {
        if (position.x  <= 0 + size / 2 || position.x >= Width - size / 2)
        {
            velocity.x *= -1;
        }
        if (position.y  <= 0 + size / 2 || position.y >= Height - size / 2)
        {
            velocity.y *= -1;
        }
    }
}
