using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : ProcessingLite.GP21
{
    Vector2 shotPos;
    float size = 0.2f;
    float bulletSpeed = 0.5f;
    Player player;
    Vector2 dist;
    public Shoot(Vector2 playerPos)
    {
        shotPos = playerPos + new Vector2(MouseX,MouseY).normalized;
        dist = playerPos + new Vector2(MouseX, MouseY);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DrawShot();
    }
    void DrawShot()
    {
        Stroke(240,240,30);
        Fill(240,240,30);

        dist.Normalize();
        dist *= bulletSpeed * Time.deltaTime;

        shotPos += dist;

        Circle(shotPos.x, shotPos.y, size);
    }
    public void ShootGun()
    {
        
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
