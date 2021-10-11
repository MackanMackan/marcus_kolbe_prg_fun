using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWriter : ProcessingLite.GP21
{

    public void WriteAAt(float x, float y)
    {
        Line(x, y, x + 1, y + 4);
        Line(x+1, y +4, x + 2, y);
        Line(x + 0.4f, y + 1.5f, x + 1.6f, y + 1.5f);
    }
    public void WriteMAt(float x, float y)
    {
        Line(x,y,x + 1,y+4);
        Line(x + 1, y + 4, x + 1.5f, y +2);
        Line(x + 1.5f, y +2,x+2,y+4);
        Line(x+2,y+4,x+3,y);
    }
    public void WriteGAt(float x, float y)
    {
        Line(x,y+4,x+2,y+4);
        Line(x,y,x,y+4);
        Line(x,y,x+3,y);
        Line(x+3,y,x+3,y+2);
        Line(x+3,y+2,x +1.5f,y+2);
    }public void WriteEAt(float x, float y)
    {
        Line(x,y,x,y+4);
        Line(x,y+4,x+2,y+4);
        Line(x,y+2,x+1,y+2);
        Line(x,y,x+2,y);
    }public void WriteOAt(float x, float y)
    {
        Line(x,y,x,y+4);
        Line(x,y+4,x+3,y+4);
        Line(x+3,y+4,x+3,y);
        Line(x,y,x+3,y);
    }public void WriteVAt(float x, float y)
    {
        Line(x,y+4,x+1,y);
        Line(x+1,y,x+2,y+4);
    }public void WriteRAt(float x, float y)
    {
        Line(x,y+4,x,y);
        Line(x,y+4,x+2,y+3);
        Line(x+2,y+3,x,y+2);
        Line(x,y+2,x+2,y);
    }
    public void WriteGameOver(float x, float y)
    {
        Stroke(0, 240, 50);
        WriteGAt(x, y);
        WriteAAt(x + 4, y);
        WriteMAt(x+7,y);
        WriteEAt(x+11,y);
        WriteOAt(x, y - 4.3f);
        WriteVAt(x+4, y- 4.3f);
        WriteEAt(x + 7, y - 4.3f);
        WriteRAt(x + 11, y - 4.3f);
    }
}
