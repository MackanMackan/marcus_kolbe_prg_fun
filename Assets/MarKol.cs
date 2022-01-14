using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarKol : IRandomWalker
{
    //Add your own variables here.
    //Do not use processing variables like width or height
    Vector2 myPosition;
    Vector2 startingPosition;

    int borderOffset = 1;
    bool roundComplete = false;
    Direction direction;
    int playAreaWidth;
    int playAreaHeight;
    float scaleFactor = 0.05f;
    int circleOffsetY = 0;
    int circleOffsetX = 0;
    int newPlayAreaHeight;
    int newPlayAreaWidth;
    Vector2 oldBorder;

    


    List<IRandomWalker> enemies = new List<IRandomWalker>();
    ProcessingLite.GP21 pl = new ProcessingLite.GP21();

    public string GetName()
    {
        return "Kolbe"; //When asked, tell them our walkers name
    }

    public Vector2 GetStartPosition(int playAreaWidth, int playAreaHeight)
    {
        this.playAreaWidth = playAreaWidth;
        this.playAreaHeight = playAreaHeight;

        //Select a starting position or use a random one.

        int x = playAreaWidth -1;
        int y = playAreaHeight-1;

        myPosition = new Vector2(x, y);
        direction = Direction.Left;

       

        startingPosition = myPosition;
        //a PVector holds floats but make sure its whole numbers that are returned!
        return myPosition;
    }

    public Vector2 Movement()
    {
        //add your own walk behavior for your walker here.
        //Make sure to only use the outputs listed below.
        pl.Stroke(59,112,192);
        pl.Fill(59, 112, 192);
        //pl.Square((Random.Range(60, playAreaWidth - 60)) * scaleFactor, Random.Range(60, playAreaHeight - 60) * scaleFactor, 5f);
        pl.Stroke(255,255,255);

        for (int i = 0; i > enemies.Count; i++)
        {
            if (enemies[i].Movement().x == myPosition.x + 1 && direction == Direction.Right)
            {
                direction = Direction.Up;
            }
            if (enemies[i].Movement().x == myPosition.x - 1 && direction == Direction.Left)
            {
                direction = Direction.Down;
            }
            if (enemies[i].Movement().y == myPosition.y + 1 && direction == Direction.Up)
            {
                direction = Direction.Left;
            }
            if (enemies[i].Movement().y == myPosition.y - 1 && direction == Direction.Down)
            {
                direction = Direction.Right;
            }
        }

        if (myPosition == startingPosition && roundComplete)
        {
            borderOffset++;
            startingPosition = new Vector2(startingPosition.x - 1, startingPosition.y - 1);
            roundComplete = false;
        }

        if (myPosition.x < borderOffset)
        {
            direction = Direction.Down;
            myPosition.x += 1;
            circleOffsetX = 100;
            return new Vector2(1, 0);
        }
        if (myPosition.x > playAreaWidth - borderOffset)
        {
            direction = Direction.Up;
            myPosition.x -= 1;
            circleOffsetX = -100;
            return new Vector2(-1, 0);
        }
        if (myPosition.y > playAreaHeight - borderOffset)
        {
            direction = Direction.Left;
            myPosition.y -= 1;
            circleOffsetY = -100;
            return new Vector2(0, -1);
        }
        if (myPosition.y < borderOffset)
        {
            direction = Direction.Right;
            myPosition.y += 1;
            circleOffsetY = 100;
            return new Vector2(0, 1);
        }

        switch (direction)
        {
            case Direction.Left:
                myPosition += new Vector2(-1, 0);
                return new Vector2(-1, 0);
            case Direction.Right:
                myPosition += new Vector2(1, 0);
                return new Vector2(1, 0);
            case Direction.Up:
                myPosition += new Vector2(0, 1);
                roundComplete = true;
                return new Vector2(0, 1);
            default:
                myPosition += new Vector2(0, -1);
                return new Vector2(0, -1);
        }


        
    }
    enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }
}

//All valid outputs:
// Vector2(-1, 0);
// Vector2(1, 0);
// Vector2(0, 1);
// Vector2(0, -1);

//Any other outputs will kill the walker!
