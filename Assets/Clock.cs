using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : ProcessingLite.GP21
{
    // Start is called before the first frame update
    float clockCenterX = 10;
    float clockCenterY = 5;
    float clockRadius = 9;
    float degrees = 0;

    float spaceBetweenDigits = 0.3f;

    int seconds, minutes, hours;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Background(0);
        ClockEdge();
        HourArm();
        MinuteArm();
        SecondArm();
        PaintDigitalClock(1, 6);
    }
    private void ClockEdge()
    {
        degrees = 0;
        Stroke(255, 255, 255);
        Fill(0, 0, 0);

        Circle(clockCenterX, clockCenterY, clockRadius);
        float radians = degrees * Mathf.Deg2Rad;
        float x = 0;
        float y = 0;
        float lineLength = 0.2f;

        for (int i = 0; i < 60; i++)
        {
            degrees += 6;
            radians = degrees * Mathf.Deg2Rad;
            x = Mathf.Cos(radians);
            y = Mathf.Sin(radians);

            if (degrees % 30 == 0)
            {
                StrokeWeight(2);
                lineLength = 0.4f;
            }
            else
            {
                StrokeWeight(1);
                lineLength = 0.2f;
            }
            Line((x * (clockRadius / 2 - lineLength) + clockCenterX), (y * (clockRadius / 2 - lineLength) + clockCenterY),
                (x * (clockRadius / 2) + clockCenterX), (y * (clockRadius / 2) + clockCenterY));
        }
    }
    private void MinuteArm()
    {


        minutes = System.DateTime.Now.Minute * -1;

        //split 360 degrees on 60 min is 6, so we have to multiplay that
        degrees = minutes * 6;
        float radians;
        float x = 0;
        float y = 0;
        float lineLength = 0.5f;


        radians = degrees * Mathf.Deg2Rad - Mathf.PI / 2;
        x = Mathf.Cos(radians);
        y = Mathf.Sin(radians);

        x = x * -1;
        y = y * -1;
        Stroke(12, 123, 90);
        Line((x * (clockRadius / 2 - lineLength) + clockCenterX),
            (y * (clockRadius / 2 - lineLength) + clockCenterY), clockCenterX, clockCenterY);
    }
    private void HourArm()
    {


        hours = (System.DateTime.Now.Hour - 1) * -1;
        //split 360 degrees on 12 hours is 30, so we have to multiplay that
        degrees = hours * 30;
        float radians;
        float x = 0;
        float y = 0;
        float lineLength = 2.5f;


        radians = degrees * Mathf.Deg2Rad - Mathf.PI / 2;
        x = Mathf.Cos(radians);
        y = Mathf.Sin(radians);

        x = x * -1;
        y = y * -1;
        Stroke(225, 123, 90);
        Line((x * (clockRadius / 2 - lineLength) + clockCenterX),
            (y * (clockRadius / 2 - lineLength) + clockCenterY), clockCenterX, clockCenterY);
    }
    private void SecondArm()
    {
        seconds = System.DateTime.Now.Second * -1;
        //split 360 degrees on 60 seconds is 6, so we have to multiplay that
        degrees = seconds * 6;
        float radians;
        float x = 0;
        float y = 0;
        float lineLength = 0.2f;


        radians = degrees * Mathf.Deg2Rad - Mathf.PI / 2;
        x = Mathf.Cos(radians);
        y = Mathf.Sin(radians);

        x = x * -1;
        y = y * -1;
        Stroke(128, 123, 240);
        Line((x * (clockRadius / 2 - lineLength) + clockCenterX),
            (y * (clockRadius / 2 - lineLength) + clockCenterY), clockCenterX, clockCenterY);
    }
    private void PaintDigitalClock(float x, float y)
    {
        Stroke(40, 255, 90);
        Fill(40, 255, 90);
        StrokeWeight(0.5f);

        hours = System.DateTime.Now.Hour;
        minutes = System.DateTime.Now.Minute;
        seconds = System.DateTime.Now.Second;

        DigitalHour(x, y);
        DigitalMinute(x + 1, y);
        DigitalSecond(x + 2, y);
    }
    private void DigitalHour(float x, float y)
    {
        hours--;
        int hoursFirstDigit = int.Parse(hours.ToString().Substring(0, 1));
        if (hours.ToString().Length > 1)
        {
            int hoursSecondDigit = int.Parse(hours.ToString().Substring(1));
            NumberToBeDisplayed(hoursFirstDigit, x, y);
            NumberToBeDisplayed(hoursSecondDigit, x + spaceBetweenDigits, y);
        }
        else
        {
            NumberToBeDisplayed(hoursFirstDigit, x + spaceBetweenDigits, y);
            NumberToBeDisplayed(0, x, y);
        }
        TimeSplitters(x + 0.2f, y);
    }
    private void DigitalMinute(float x, float y)
    {
        int minuteFirstDigit = int.Parse(minutes.ToString().Substring(0, 1));
        if (minutes.ToString().Length > 1)
        {
            int minuteSecondDigit = int.Parse(minutes.ToString().Substring(1));
            NumberToBeDisplayed(minuteFirstDigit, x, y);
            NumberToBeDisplayed(minuteSecondDigit, x + spaceBetweenDigits, y);
        }
        else
        {
            NumberToBeDisplayed(minuteFirstDigit, x + spaceBetweenDigits, y);
            NumberToBeDisplayed(0, x, y);
        }
        TimeSplitters(x + 0.2f, y);
    }
    private void DigitalSecond(float x, float y)
    {
        int secondFirstDigit = int.Parse(seconds.ToString().Substring(0, 1));
        if (seconds.ToString().Length > 1)
        {
            int secondSecondDigit = int.Parse(seconds.ToString().Substring(1));
            NumberToBeDisplayed(secondFirstDigit, x, y);
            NumberToBeDisplayed(secondSecondDigit, x + spaceBetweenDigits, y);
        }
        else
        {
            NumberToBeDisplayed(secondFirstDigit, x + spaceBetweenDigits, y);
            NumberToBeDisplayed(0, x, y);
        }


    }
    private void TimeSplitters(float x, float y)
    {
        Circle(x + 0.6f, y + 0.1f, 0.06f);
        Circle(x + 0.6f, y + 0.4f, 0.06f);
    }

    private void Digit0(float x, float y)
    {
        BottomLine(x, y);
        LeftBottomLine(x, y);
        RightBottomLine(x, y);
        LeftTopLine(x, y);
        RightTopLine(x, y);
        TopLine(x, y);
    }
    private void Digit1(float x, float y)
    {
        RightBottomLine(x, y);
        RightTopLine(x, y);
    }
    private void Digit2(float x, float y)
    {
        BottomLine(x, y);
        LeftBottomLine(x, y);
        MiddleLine(x, y);
        RightTopLine(x, y);
        TopLine(x, y);
    }
    private void Digit3(float x, float y)
    {
        BottomLine(x, y);
        RightBottomLine(x, y);
        MiddleLine(x, y);
        RightTopLine(x, y);
        TopLine(x, y);
    }
    private void Digit4(float x, float y)
    {
        RightBottomLine(x, y);
        MiddleLine(x, y);
        LeftTopLine(x, y);
        RightTopLine(x, y);

    }
    private void Digit5(float x, float y)
    {
        BottomLine(x, y);
        RightBottomLine(x, y);
        MiddleLine(x, y);
        LeftTopLine(x, y);
        TopLine(x, y);

    }
    private void Digit6(float x, float y)
    {
        BottomLine(x, y);
        LeftBottomLine(x, y);
        RightBottomLine(x, y);
        MiddleLine(x, y);
        LeftTopLine(x, y);
        TopLine(x, y);

    }
    private void Digit7(float x, float y)
    {
        RightBottomLine(x, y);
        RightTopLine(x, y);
        TopLine(x, y);

    }
    private void Digit8(float x, float y)
    {
        BottomLine(x, y);
        LeftBottomLine(x, y);
        RightBottomLine(x, y);
        MiddleLine(x, y);
        LeftTopLine(x, y);
        RightTopLine(x, y);
        TopLine(x, y);

    }
    private void Digit9(float x, float y)
    {
        BottomLine(x, y);
        RightBottomLine(x, y);
        MiddleLine(x, y);
        LeftTopLine(x, y);
        RightTopLine(x, y);
        TopLine(x, y);

    }
    private void BottomLine(float x, float y)
    {
        Line(x, y, x + 0.2f, y);
    }
    private void RightBottomLine(float x, float y)
    {
        Line(x + 0.2f, y, x + 0.2f, y + 0.3f);
    }
    private void LeftBottomLine(float x, float y)
    {
        Line(x, y, x, y + 0.3f);
    }
    private void TopLine(float x, float y)
    {
        Line(x, y + 0.6f, x + 0.2f, y + 0.6f);
    }
    private void RightTopLine(float x, float y)
    {
        Line(x + 0.2f, y + 0.3f, x + 0.2f, y + 0.6f);
    }
    private void LeftTopLine(float x, float y)
    {
        Line(x, y + 0.3f, x, y + 0.6f);
    }
    private void MiddleLine(float x, float y)
    {
        Line(x, y + 0.3f, x + 0.2f, y + 0.3f);
    }

    void NumberToBeDisplayed(int time, float x, float y)
    {
        switch (time)
        {
            case 0:
                Digit0(x, y);
                break;
            case 1:
                Digit1(x, y);
                break;
            case 2:
                Digit2(x, y);
                break;
            case 3:
                Digit3(x, y);
                break;
            case 4:
                Digit4(x, y);
                break;
            case 5:
                Digit5(x, y);
                break;
            case 6:
                Digit6(x, y);
                break;
            case 7:
                Digit7(x, y);
                break;
            case 8:
                Digit8(x, y);
                break;
            case 9:
                Digit9(x, y);
                break;
        }
    }
}
