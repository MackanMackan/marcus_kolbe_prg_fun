using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ProcessingLite.GP21
{
    List<Zombie> zombies;
    List<Shoot> shots;

    Timer timer;
    void Start()
    {
        timer = GetComponent<Timer>();
        zombies = new List<Zombie>();
        shots = new List<Shoot>();

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
            ShootGun();
            DrawShots();
            checkZombieShot();
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
        Stroke(255, 255, 255);
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
        GetComponent<ScreenWriter>().WriteGameOver(3, Height / 2);
    }
    void DrawTimer()
    {
        timer.PaintDigitalClock(0.5f, Height - 1);
    }
    IEnumerator ZombieSpawner(float secondsToWait)
    {
        while (true)
        {
            int seconds = timer.GetSeconds();
            int minutes = timer.GetMinutes();
            int amountOfZombies = seconds / 3 * (minutes + 1);

            for (int i = 0; i < amountOfZombies; i++)
            {
                zombies.Add(new Zombie(Width, Height));
            }
            yield return new WaitForSeconds(secondsToWait);
        }
    }
    public void ShootGun()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shots.Add(new Shoot(GetComponent<Player>().GetPlayerPosition()));
        }
    }
    public void DrawShots()
    {
        foreach (Shoot shots in shots)
        {

            shots.DrawShot();
        }
    }
    void checkZombieShot()
    {
        int shotIncrement = 0;
        int zombieIncrement = 0;
        bool zombieGotShot = false;
        //For each shot, check if it hits any zombie on board. if true, then remove both the shot and the zombie
        foreach (Shoot shot in shots)
        {
            // if shot is out of screen, remove it for prestanda purposes
            if (shot.GetShotPosition().x % Width <= 0 || shot.GetShotPosition().y % Height <= 0 
                || shot.GetShotPosition().x >= Width || shot.GetShotPosition().y >= Height)
            {
                shots.RemoveAt(shotIncrement);
                break;
            }

            foreach (Zombie zombie in zombies)
            {
                if (zombie.ZombieShot(shot))
                {
                    shots.RemoveAt(shotIncrement);
                    zombies.RemoveAt(zombieIncrement);
                    zombieGotShot = true;
                    break;
                }

                zombieIncrement++;
            }
            if (zombieGotShot)
            {
                zombieGotShot = false;
                break;
            }
            zombieIncrement = 0;
            shotIncrement++;
        }
    }
}