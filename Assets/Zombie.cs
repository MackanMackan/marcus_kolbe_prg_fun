using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : ProcessingLite.GP21
{
    float size = 0.5f;
    float speed = 2f;
    float maxSpeed = 0.15f;

    Vector2 velocity;
    Vector2 acceleration;
    Vector2 zombiePos;

    Shoot shot;
    Player player;
    public Zombie(float width, float height)
    {
        SpawnZombie(width, height);
    }
    void Start()
    {
    }

    void Update()
    {
    }
    public void ChasePlayer(Player player)
    {
        Vector2 dist = player.GetPlayerPosition() - zombiePos;
        dist.Normalize();
        dist *= speed * Time.deltaTime;

        zombiePos += dist;
    }

    public bool CheckPlayerCought(Player player)
    {
        float maxDistance = size / 2 + player.GetPlayerSize() / 2;

        //first a quick check to see if we are too far away in x or y direction
        //if we are far away we don't collide so just return false and be done.
        if (Mathf.Abs(zombiePos.x - player.GetPlayerPosition().x) > maxDistance || Mathf.Abs(zombiePos.y - player.GetPlayerPosition().y) > maxDistance)
        {
            return false;
        }
        //we then run the slower distance calculation
        //Distance uses Pythagoras to get exact distance, if we still are to far away we are not colliding.
        else if (Vector2.Distance(new Vector2(zombiePos.x, zombiePos.y), new Vector2(player.GetPlayerPosition().x, player.GetPlayerPosition().y)) > maxDistance)
        {
            return false;
        }
        //We now know the points are closer then the distance so we are colliding!
        else
        {
            return true;
        }
    }
    public bool ZombieShot(Shoot shot)
    {
        float maxDistance = size / 2 + shot.GetShotSize() / 2;

        //first a quick check to see if we are too far away in x or y direction
        //if we are far away we don't collide so just return false and be done.
        if (Mathf.Abs(zombiePos.x - shot.GetShotPosition().x) > maxDistance || Mathf.Abs(zombiePos.y - shot.GetShotPosition().y) > maxDistance)
        {
            return false;
        }
        //we then run the slower distance calculation
        //Distance uses Pythagoras to get exact distance, if we still are to far away we are not colliding.
        else if (Vector2.Distance(new Vector2(zombiePos.x, zombiePos.y), new Vector2(shot.GetShotPosition().x, shot.GetShotPosition().y)) > maxDistance)
        {
            return false;
        }
        //We now know the points are closer then the distance so we are colliding!
        else
        {
            velocity.x = zombiePos.x * speed * Time.deltaTime;
            velocity.y = zombiePos.y * speed * Time.deltaTime;
            return true;
        }
    }
    public void DrawZombie()
    {
        Stroke(0,240,20);
        Fill(0,0,0);
        Circle(zombiePos.x,zombiePos.y,size);
    }
    public void SpawnZombie(float width, float height)
    {
        switch(Random.Range(0, 4))
        {
            case 0:
                zombiePos = new Vector2(Random.Range(-10, -1), Random.Range(-10, height + 10));
                break;
            case 1:
                zombiePos = new Vector2(Random.Range(-10, width + 10), Random.Range(-10, - 1));
                break;
            case 2:
                zombiePos = new Vector2(Random.Range(width + 10, width + 1), Random.Range(-10, height + 10));
                break;
            case 3:
                zombiePos = new Vector2(Random.Range(-10, width + 10), Random.Range(height + 1,height + 10));
                break;
        }
    }
}
