using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : ProcessingLite.GP21
{
    Vector2 shotPos;
    float size = 0.1f;
    float bulletSpeed = 20f;
    Player player;
    Vector2 dist;
    public Shoot(Vector2 playerPos)
    {
        //Where the shots will begin from
        shotPos = playerPos;
        //Gets direction for the shot
        dist = new Vector2(MouseX, MouseY) - playerPos;
    }
    public void DrawShot()
    {
        Stroke(240,240,30);
        Fill(240,240,30);

        dist.Normalize();
        dist *= bulletSpeed * Time.deltaTime;

        shotPos += dist;

        Circle(shotPos.x, shotPos.y, size);
    }
    public Vector2 GetShotPosition()
    {
        return shotPos;
    }
    public float GetShotSize()
    {
        return size;
    }
}
