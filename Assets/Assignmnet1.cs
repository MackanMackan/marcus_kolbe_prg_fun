using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Assignmnet1 : ProcessingLite.GP21
{
    // Start is called before the first frame update

    public GameObject sprite;
    SpriteRenderer background;
    public GameObject audioObject;

    bool mouseButtonPressedOnce = false;
    bool inPaintMode = false;
    bool inEraseMode = false;
    bool inSnakeMode = false;

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

    float x1 = 7f;
    float y1 = 5f;
    float x2 = 7.1f;
    float y2 = 5f;

    float snakeBorderPosX = 4;
    float snakeBorderPosY = -2;
    float borderLineXMax = 11.5f;
    float borderLineXMin = 2;
    float borderLineYMax = 11.5f;
    float borderLineYMin = 2.5f;

    [SerializeField]
    private float snakeSpeed;
    [SerializeField]
    private float snakeSpeedTimer = 0.2f;
    [SerializeField]
    private int snakeSize = 10;
    [SerializeField]
    private float snakeLength = 4f;
    int snakeColorR = 23;
    int snakeColorB = 128;
    int snakeColorG = 190;

    bool alive = true;
    bool movingUp = false;
    bool movingDown = false;
    bool movingRight = true;
    bool movingLeft = false;

    bool snakeFoodCreated = false;
    float latestSnakeFoodPosX;
    float latestSnakeFoodPosY;
    int foodEaten = 0;

    List<LineData> lines = new List<LineData>();
    void Start()
    {
        count = 0;
        worldPosFirstClick = new Vector3(0, 0, 0);
        worldPosSecondClick = new Vector3(0, 0, 0);
        mousePos = new Vector3(0,0,0);

        background = sprite.GetComponent<SpriteRenderer>();

        movingRight = true;

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
        if (Input.GetKeyDown(KeyCode.S))
        {
            inSnakeMode = !inSnakeMode;
            alive = true;
            Debug.Log("Snake Mode = " + inSnakeMode);
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

        if (inSnakeMode)
        {
            inPaintMode = false;
            inEraseMode = false;
            audioObject.GetComponent<EffectsHandler>().playMainSong();
            playSnake(snakeBorderPosX,snakeBorderPosY);
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
        //This is free painting, paints a small line, aka dot, att mouse position for as long as you hold down mouse click
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

        /*int r = (int)Math.Round(background.color.r);
        int g = (int)Math.Round(background.color.g);
        int b = (int)Math.Round(background.color.b);*/
        int r = (int)Math.Round(Camera.main.backgroundColor.r*255);
        int g = (int)Math.Round(Camera.main.backgroundColor.g*255);
        int b = (int)Math.Round(Camera.main.backgroundColor.b*255);
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
            Circle(0.5f, 6, 0.8f);

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
    public void playSnake(float x, float y)
    {
        //painting the battlefield
        Stroke(82,51,10);

        Line(borderLineXMin + x,borderLineYMax + y, borderLineXMin + x,borderLineYMin + y);
        Line(borderLineXMin + x, borderLineYMin + y,borderLineXMax + x, borderLineYMin + y);
        Line(borderLineXMax + x, borderLineYMax + y, borderLineXMax + x, borderLineYMin + y);
        Line(borderLineXMin + x, borderLineYMax + y, borderLineXMax + x, borderLineYMax + y);
        if (alive)
        {
            snakeMovement();
            if (!snakeFoodCreated)
            {
                paintSnakeFood();
            }
            eatSnakeFood();
            checkDeath();

        }else if (!alive)
        {
            Fill(59, 112, 192);

            //Circle(10,10,50);
            audioObject.GetComponent<EffectsHandler>().playGameOverSong();
            inSnakeMode = false;
        }
        count += Time.deltaTime;
    }
    public void snakeMovement()
    {
        Stroke(snakeColorR,snakeColorG,snakeColorB);

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (movingUp || movingDown && !movingRight)
            {
                movingUp = false;
                movingDown = false;
                movingLeft = false;
                movingRight = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (movingRight || movingLeft && !movingUp)
            {
                movingUp = true;
                movingDown = false;
                movingLeft = false;
                movingRight = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (movingUp || movingDown && !movingLeft)
            {
                movingUp = false;
                movingDown = false;
                movingLeft = true;
                movingRight = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (movingRight || movingLeft && !movingDown)
            {
                movingUp = false;
                movingDown = true;
                movingLeft = false;
                movingRight = false;
            }
        }

        if (count > snakeSpeedTimer)
        {
            if (movingRight)
            {
                x1 += snakeLength * snakeSpeed;
                x2 += snakeLength * snakeSpeed;
                Line(x1, y1, x2, y2);
                lines.Add(new LineData(x1, y1, x2, y2));
                count = 0;
            }
            else if (movingUp)
            {
                y1 += snakeLength * snakeSpeed;
                y2 += snakeLength * snakeSpeed;
                Line(x1, y1, x2, y2);
                lines.Add(new LineData(x1, y1, x2, y2));
                count = 0;
            }
            else if (movingLeft)
            {
                x1 -= snakeLength * snakeSpeed;
                x2 -= snakeLength * snakeSpeed;
                Line(x1, y1, x2, y2);
                lines.Add(new LineData(x1, y1, x2, y2));
                count = 0;
            }
            else if (movingDown)
            {
                y1 -= snakeLength * snakeSpeed;
                y2 -= snakeLength * snakeSpeed;
                Line(x1, y1, x2, y2);
                lines.Add(new LineData(x1, y1, x2, y2));
                count = 0;
            }
        }
        else
        {
            count += Time.deltaTime;
        }

        if (lines.Count == snakeSize+1)
        {
            //Get color of the background to use as eraser
            Stroke((int)Math.Round(Camera.main.backgroundColor.r * 255), (int)Math.Round(Camera.main.backgroundColor.g * 255)
                , (int)Math.Round(Camera.main.backgroundColor.b * 255));
            Line(lines[0].x1, lines[0].y1, lines[0].x2, lines[0].y2);
            lines.RemoveAt(0);
        }
    }
    public void paintSnakeFood()
    {
        Stroke(0,0,0);
        Fill(0,0,0);
        // Gets random double and makes it so that it is in between the borderlines for the game, also add a margin so it doesnt spawn in to tight corners
        float x = (float)(new System.Random().NextDouble() * (borderLineXMax - borderLineXMin) + borderLineXMin) + snakeBorderPosX;
        float y = (float)(new System.Random().NextDouble() * (borderLineYMax - borderLineYMin) + borderLineYMin) + snakeBorderPosY;

        if(x > borderLineXMax - 0.5 + snakeBorderPosX)
        {
            x = borderLineXMax - 0.5f + snakeBorderPosX;
        }else if (x < borderLineXMin + 0.5 + snakeBorderPosX)
        {
            x = borderLineXMin + 0.5f + snakeBorderPosX;
        }

        if (y > borderLineYMax - 0.5 + snakeBorderPosY)
        {
            y = borderLineYMax - 0.5f + snakeBorderPosY;
        }
        else if (y < borderLineYMin + 0.5 + snakeBorderPosY)
        {
            y = borderLineYMin + 0.5f + snakeBorderPosY;
        }
        Circle(x,y,0.5f);
        snakeFoodCreated = true;

        latestSnakeFoodPosX = x;
        latestSnakeFoodPosY = y;
    }
    public void eatSnakeFood()
    {
        if(x1 > (latestSnakeFoodPosX - 0.5) && x1 < (latestSnakeFoodPosX + 0.5)
            && y1 > (latestSnakeFoodPosY - 0.5) && y1 < (latestSnakeFoodPosY + 0.5))
        {
            int r = (int)Math.Round(Camera.main.backgroundColor.r * 255);
            int g = (int)Math.Round(Camera.main.backgroundColor.g * 255);
            int b = (int)Math.Round(Camera.main.backgroundColor.b * 255);
            // paints a circle filled and outlines of the cameras background color, which makes it look like its eaten

            Fill(r, g, b);
            Stroke(r, g, b);

            Circle(latestSnakeFoodPosX, latestSnakeFoodPosY, 0.6f);
            snakeSize += 10;
            snakeFoodCreated = false;
            foodEaten++;
            if(snakeSpeedTimer <= 0.05)
            {
                snakeSpeedTimer = 0.05f;
            }
            else
            {
                snakeSpeedTimer -= 0.005f;
            }
            Debug.Log("EATEN! : "+foodEaten);
            audioObject.GetComponent<EffectsHandler>().playEatSFX();
            Random rnd = new Random();

            snakeColorR = rnd.Next(1, 255) + 1;
            snakeColorG = rnd.Next(1, 255) + 1;
            snakeColorB = rnd.Next(1, 255) + 1;
            
        }
    }
    public void checkDeath()
    {
        int i = 0;
        float bigX = 0;
        float smallX = 0;
        float bigY = 0;
        float smallY = 0;
        if (x1 > borderLineXMax + snakeBorderPosX || x1 < borderLineXMin + snakeBorderPosX
            || y1 > borderLineYMax + snakeBorderPosY || y1 < borderLineYMin + snakeBorderPosY)
        {
            alive = false;
        }
        foreach (LineData line in lines)
        {
            i++;

            //Sorts the x and y´s for easy check if the snakes is within them, aka hits itself
            if(line.x1 > line.x2)
            {
                bigX = line.x1;
                smallX = line.x2;
            }
            else
            {
                bigX = line.x2;
                smallX = line.x1;
            }

            if(line.y1 > line.y2)
            {
                bigY = line.y1;
                smallY = line.y2;
            }
            else
            {
                bigY = line.y2;
                smallY = line.y1;
            }
            // break if on the last third element, otherwise you will always hit yourself in the beginning
            if(i >= lines.Count-3)
            {
                break;
            }
            if(x1 <= bigX && x1 >= smallX && y1 <= bigY && y1 >= smallY)
            {
                Debug.Log("Me: "+ x1 +", "+y1);
                Debug.Log("Line: " + line.x1 + ", " + line.y1 + ", "+ line.x2 + ", "+ line.y2);
                alive = false;
                break;
            }
        }
    }
}
public class LineData
{
    public float x1, y1, x2, y2;
    public LineData(float x1, float y1, float x2, float y2)
    {
        this.x1 = x1;
        this.y1 = y1;
        this.x2 = x2;
        this.y2 = y2;
    }
}