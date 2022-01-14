using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigonomitry : ProcessingLite.GP21
{
    // Start is called before the first frame update
    float y;
    float x;
    float direction = 0;
    float offset = 0.18f;
    float speed = 20f;
    float dx;
    [SerializeField]
    int amountOfBalls = 10;
    [SerializeField]
    float ballSpacing = 0.1f;
    [SerializeField]
    float ballSize = 0.1f;
    float ballDirection = 0;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        dx = (Mathf.Pow(Mathf.PI, 2) / speed) * offset;
        NoStroke();
    }

    // Update is called once per frame
    void Update()
    {
        Background(0);
        PaintSineWave();
        PaintCircle();

    }
    void PaintSineWave()
    {
        direction += 0.1f;
        float xX = direction;
        for (int i = 0; i < 100; i++)
        {
            x = Mathf.Cos(xX);
            y = Mathf.Sin(xX);
            Fill(255, 0, 0);
            Ellipse(i * offset, Height / 2 + y, 0.1f, 0.1f);
            Fill(128, 128, 128);
            Ellipse(i * offset, Height / 2 + x, 0.1f, 0.1f);
            xX += dx;
        }
    }
    void PaintCircle()
    {
        ballDirection += 0.1f;
        float xX = ballDirection;
        for (int i = 0; i < amountOfBalls; i++)
        {
            x = Mathf.Cos(xX);
            y = Mathf.Sin(xX);
            Fill(255, 60, 80);
            Ellipse(x+i*ballSpacing, y + i * ballSpacing, ballSize, ballSize);
            xX += dx;
        }
    }
}
