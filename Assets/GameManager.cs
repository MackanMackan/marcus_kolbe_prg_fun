using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ProcessingLite.GP21
{
    int numberOfBalls = 10;
    Ball[] balls;
    Player player;
    void Start()
    {
        balls = new Ball[numberOfBalls];
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i] = new Ball(Width / 2, Height / 2);
        }

        player = new Player();
    }
    // Update is called once per frame
    void Update()
    {
        if (!CheckDeadCondition())
        {
            Background(0);
            DrawBalls();
            DrawPlayer();
        }
        else
        {
            GameOverScreen();
        }
    }
    void DrawBalls()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].UpdatePos();
            balls[i].Draw();
            balls[i].BounceOnScreen();
        }
    }
    void DrawPlayer()
    {
        player.MoveCharacter();
    }
    bool CheckDeadCondition()
    {
        foreach (Ball ball in balls)
        {
            if (ball.CircleCollision(ball, player))
            {
                return true;
            }
        }
        return false;
    }
    void GameOverScreen()
    {
        Background(0);
    }
}
