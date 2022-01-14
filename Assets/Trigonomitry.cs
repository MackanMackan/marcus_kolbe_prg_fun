using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigonomitry : ProcessingLite.GP21
{
    // Start is called before the first frame update
    float y;
    float x;
    float direction = 0;
    float offset = 0.1f;
    float speed = 0.15f;

    Vector2 position = new Vector2(0,2);
    void Start()
    {
        Application.targetFrameRate = 30;
        //Camera.main.transform.position = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Background(128);

        for (int i = 0; i < 100; i++)
        {
            x = Mathf.Cos(direction);
            y = Mathf.Sin(direction);
            Circle(position.x+=offset, Height/2+y, 0.1f);
            position.x += offset;
            direction += (speed * Time.deltaTime)/4;
        }
        position.x = 0;
        position.y = 0;
        // Circle(position.x, Height/2, 0.5f);
    }

}
