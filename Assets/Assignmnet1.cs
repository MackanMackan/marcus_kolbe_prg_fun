using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignmnet1 : ProcessingLite.GP21
{
    // Start is called before the first frame update

    public GameObject sprite;
    SpriteRenderer background;

    bool mouseButtonPressedOnce = false;
    bool inPaintMode = false;
    bool inEraseMode = false;

    Vector3 mousePos;
    Vector3 worldPosFirstClick;
    Vector3 worldPosSecondClick;

    float count;

    [Range(1,255)]
    public int fillColorR;
    [Range(1, 255)]
    public int fillColorG;
    [Range(1, 255)]
    public int fillColorB;
    
    [Range(1, 255)]
    public int strokeColorR;
    [Range(1, 255)]
    public int strokeColorG;
    [Range(1, 255)]
    public int strokeColorB;

    [Range(1, 255)]
    public int backgroundColorR;
    [Range(1, 255)]
    public int backgroundColorG;
    [Range(1, 255)]
    public int backgroundColorB;

    int previousBackgroundColorR = 0;
    int previousBackgroundColorB = 0;
    int previousBackgroundColorG = 0;

    int previousStrokeColorR = 0;
    int previousStrokeColorG = 0;
    int previousStrokeColorB = 0;

    int previousFillColorR = 0;
    int previousFillColorG = 0;
    int previousFillColorB = 0;
    void Start()
    {
        count = 0;
        worldPosFirstClick = new Vector3(0, 0, 0);
        worldPosSecondClick = new Vector3(0, 0, 0);

        mousePos = new Vector3(0,0,0);

        background = sprite.GetComponent<SpriteRenderer>();

        letterM(-3,0);
        letterA(-4,0);


    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)){
            inPaintMode = !inPaintMode;
            inEraseMode = false;
            Debug.Log("Paint Mode = "+inPaintMode);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            inEraseMode = !inEraseMode;
            inPaintMode = false;
            Debug.Log("Erase Mode = " + inEraseMode);
        }
        if (inPaintMode)
        {
            // paint your own name!
            paint();
        }else if (inEraseMode)
        {
            erase();
        }
        else
        {
            paintLines();
        }

        if (!inEraseMode)
        {
            Fill(fillColorR, fillColorG, fillColorB);
            Stroke(strokeColorR, strokeColorG, strokeColorB);
        }

        changeBackgroundColor();
        previewColorStroke();
        previewColorFill();
    }
    private void paintLines()
    {
        // takes the mouse position and saves it for the first point used for the LINE method
        if (Input.GetMouseButtonDown(0) && mouseButtonPressedOnce == false)
        {
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosFirstClick = Camera.main.ScreenToWorldPoint(mousePos);
            mouseButtonPressedOnce = true;
        }else if (Input.GetMouseButtonDown(0) && mouseButtonPressedOnce)
            {
                // takes the mouse position and saves it for the second point used for the LINE method
                mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;
                worldPosSecondClick = Camera.main.ScreenToWorldPoint(mousePos);

                Line(worldPosFirstClick.x, worldPosFirstClick.y, worldPosSecondClick.x, worldPosSecondClick.y);
                mouseButtonPressedOnce = false;
            }
    }
    private void paint()
    {
        
        if (Input.GetMouseButton(0) && count == 0)
        {
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosFirstClick = Camera.main.ScreenToWorldPoint(mousePos);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            count = 0f;
        }

        if(count > 0.05f)
        {
            // takes the mouse position and saves it for the second point used for the LINE method
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosSecondClick = Camera.main.ScreenToWorldPoint(mousePos);

            Line(worldPosFirstClick.x, worldPosFirstClick.y, worldPosSecondClick.x, worldPosSecondClick.y);
            count = 0;
        }
        else if(Input.GetMouseButton(0))
        {
            count += Time.deltaTime;
        }
    }
    private void erase()
    {
        
        // it takes the background color in real time, if the background would change in runtime

        int r = (int)Math.Round(background.color.r);
        int g = (int)Math.Round(background.color.g);
        int b = (int)Math.Round(background.color.b);

        // paints a circle filled and outlines of the cameras background color at mouse position, which makes a eraser effect
        
        Fill(r, g, b);
        Stroke(r, g, b);

        if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosFirstClick = Camera.main.ScreenToWorldPoint(mousePos);
            Circle(worldPosFirstClick.x,worldPosFirstClick.y,1);

        }
    }
    public void letterA(float x, float y)
    {
        Line(8+x, 7+y, 7+x, 3+y);
        Line(8+x, 7+y, 9+x, 3+y);
        Line(7.5f+x, 5+y, 8.5f+x, 5+y);
    }
    public void letterM(float x, float y)
    {
        Line(4+x, 7+y, 4+x, 3+y);
        Line(4+x, 7+y, 5+x, 5+y);
        Line(6+x, 7+y, 5+x, 5+y);
        Line(6+x, 7+y, 6+x, 3+y);
    }
    public void previewColorStroke()
    {
        if (previousStrokeColorR != strokeColorR || previousStrokeColorG != strokeColorG
            || previousStrokeColorB != strokeColorB)
        {
            Line(0.5f, 9, 0.5f, 7);

            previousStrokeColorR = strokeColorR;
            previousStrokeColorG = strokeColorG;
            previousStrokeColorB = strokeColorB;
        }
    }
    public void previewColorFill()
    {
        if (previousFillColorR != fillColorR || previousFillColorG != fillColorG
            || previousFillColorB != fillColorB)
        {
            Circle(0.5f, 6, 0.8f);

            previousFillColorR = fillColorR;
            previousFillColorG = fillColorG;
            previousFillColorB = fillColorB;
        }
    }
    public void changeBackgroundColor()
    {
        if (previousBackgroundColorR != backgroundColorR || previousBackgroundColorG != backgroundColorG 
            || previousBackgroundColorB != backgroundColorB)
        {
            //Changes backcolor in real time
            background.color = new Color32((byte)backgroundColorR, (byte)backgroundColorG, (byte)backgroundColorB, 255);
            

            previousBackgroundColorR = backgroundColorR;
            previousBackgroundColorG = backgroundColorG;
            previousBackgroundColorB = backgroundColorB;

            sprite.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y, -9.0f);
            sprite.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y, -9.99f);
        }

    }
}
