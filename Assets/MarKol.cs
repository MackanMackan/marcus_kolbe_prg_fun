using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarKol : IRandomWalker
{
    //Add your own variables here.
    //Do not use processing variables like width or height
    Vector2 positon;
    Direction dir;
    int playAreaWidth;
    int playAreaHeight;
    int newPlayAreaHeight;
    int newPlayAreaWidth;

    public string GetName()
    {
        return "Kolbe"; //When asked, tell them our walkers name
    }

    public Vector2 GetStartPosition(int playAreaWidth, int playAreaHeight)
    {
        this.playAreaWidth = Mathf.RoundToInt(playAreaWidth);
        this.playAreaHeight = Mathf.RoundToInt(playAreaHeight);
        //Select a starting position or use a random one.

        float x = Random.Range(0, playAreaWidth);
        float y = Random.Range(0, playAreaHeight);
        positon = new Vector2(x, y);
        dir = Direction.Down;
        //a PVector holds floats but make sure its whole numbers that are returned!
        return positon;
    }

    public Vector2 Movement()
    {
        //add your own walk behavior for your walker here.
        //Make sure to only use the outputs listed below.
        if (positon.x < 0)
        {
            dir = Direction.Right;
        }
        if (positon.x > playAreaWidth)
        {
            dir = Direction.Left;
        }
        if (positon.y > playAreaHeight)
        {
            dir = Direction.Down;
        }
        if (positon.y < 0)
        {
            dir = Direction.Up;
        }

        switch (dir)
        {
            case Direction.Left:
                positon += new Vector2(-1, 0);
                return new Vector2(-1, 0);
            case Direction.Right:
                positon += new Vector2(1, 0);
                return new Vector2(1, 0);
            case Direction.Up:
                positon += new Vector2(0, 1);
                return new Vector2(0, 1);
            default:
                positon += new Vector2(0, -1);
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
