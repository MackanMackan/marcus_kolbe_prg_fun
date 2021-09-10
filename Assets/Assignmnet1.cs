using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignmnet1 : ProcessingLite.GP21
{
    // Start is called before the first frame update

    public Camera camera;

    bool mouseButtonPressedOnce = false;
    bool inPaintMode = false;
    bool inEraseMode = false;

    Vector3 mousePos;
    Vector3 worldPosFirstClick;
    Vector3 worldPosSecondClick;

    float count;

    void Start()
    {
        count = 0;
        worldPosFirstClick = new Vector3(0, 0, 0);
        worldPosSecondClick = new Vector3(0, 0, 0);

        mousePos = new Vector3(0,0,0);

        Line(4, 7, 4, 3);
        Line(4, 7, 5, 5);
        Line(6, 7, 5, 5);
        Line(6, 7, 6, 3);

        Line(8, 7, 7, 3);
        Line(8, 7, 9, 3);
        Line(7.5f , 5, 8.5f, 5);

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
    }
    private void paintLines()
    {
        Fill(255, 255, 255);
        Stroke(255, 255, 255);
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
        Fill(255, 255, 255);
        Stroke(255, 255, 255);
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
        // paints a circle filled and outlines of the cameras background color at mouse position, which makes a eraser effect
        Fill(55, 98, 164);
        Stroke(55, 98, 164);

        if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosFirstClick = Camera.main.ScreenToWorldPoint(mousePos);
            Circle(worldPosFirstClick.x,worldPosFirstClick.y,1);

        }
    }
}
