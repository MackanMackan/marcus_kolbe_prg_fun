using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
public class Assignment2 : ProcessingLite.GP21
{
    [SerializeField]
    [Range(0,10)]
    private float y;
    [SerializeField]
    [Range(-9, 1)]
    private float x;

    private int lines = 10;
    Random rnd = new Random();
    // Start is called before the first frame update
    void Start()
    {
        Background(0);
        parabolicCurve(y,x);
    }

    // Update is called once per frame
    void Update()
    {
        Background(0);
        parabolicCurve(x, y);
    }
    public void parabolicCurve(float x, float y)
    {

        for (int i = 0; i <= lines; i++)
        {
            float y1 = Height - i * Height / lines;
            float x2 = i * Width / lines;
            Stroke(255,255,255);

            if( i % 3 == 0 && i != 0)
            {
                Stroke(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            }
            Line(0, y1, x2, 0);
        }
        
    }
}
