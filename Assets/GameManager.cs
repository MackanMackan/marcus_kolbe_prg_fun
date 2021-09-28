using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ProcessingLite.GP21
{
    List<Zombie> zombies;
    Timer timer;
    void Start()
    {
        timer = GetComponent<Timer>();
        zombies = new List<Zombie>();

        StartCoroutine(ZombieSpawner(1));

    }
    // Update is called once per frame
    void Update()
    {
        if (!CheckDeadCondition())
        {
            Background(0);
            DrawZombies();
            DrawPlayer();
        }
        else
        {
            GameOverScreen();
            timer.StopTimer();
        }
        DrawTimer();
    }
    void DrawZombies()
    {
        foreach (Zombie zombie in zombies)
        {
            zombie.ChasePlayer(GetComponent<Player>());
            zombie.DrawZombie();
        }
    }
    void DrawPlayer()
    {
        Stroke(255,255,255);
        GetComponent<Player>().MoveCharacter();
    }
    bool CheckDeadCondition()
    {
        foreach (Zombie zombie in zombies)
        {
            if (zombie.CheckPlayerCought(GetComponent<Player>()))
            {
                return true;
            }
        }
        return false;
    }
    void GameOverScreen()
    {
        Background(0);
        GetComponent<ScreenWriter>().WriteGameOver(3,Height/2);
    }
    void DrawTimer()
    {
        timer.PaintDigitalClock(0.5f,Height - 1);
    }
    IEnumerator ZombieSpawner(float secondsToWait)
    {
        while (true)
        {
            int seconds = timer.GetSeconds();
            int minutes = timer.GetMinutes();
            int amountOfZombies = seconds/3 * (minutes + 1);

            for (int i = 0; i < amountOfZombies; i++)
            {
                zombies.Add(new Zombie(Width, Height));
            }
            yield return new WaitForSeconds(secondsToWait);
        }
    }
}