using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : ProcessingLite.GP21
{
    float spaceBetweenDigits = 0.3f;
    float timer = 0;
    int seconds;
    int minutes;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimer();
    }
    void UpdateTimer()
    {
        if(timer >= 1)
        {
            seconds++;
            timer = 0;
        }
        if(seconds == 60)
        {
            minutes++;
            seconds = 0;
        }
    }
    public void PaintDigitalClock(float x, float y)
    {
        Stroke(40, 255, 90);
        Fill(40, 255, 90);
        StrokeWeight(0.5f);
        DigitalMinute(x, y);
        DigitalSecond(x + 1, y);
        Fill(0, 0, 0);
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
    public void StopTimer()
    {
        timer = 0;
    }
    public int GetSeconds()
    {
        return seconds;
    } public int GetMinutes()
    {
        return minutes;
    }
}
