using System;
using UnityEngine;
using static Structs;

public class PlayerController : MonoBehaviour
{
    // Game Object 
    public GameObject canvas;

    public float health = 3;
    public float defense = 1;

    public PlayerMovement playerMovement;
    public PlayerShoot playerShoot;

    private void Start()
    {
        // Set the canvas to be inactive at the start of the game 
        canvas.gameObject.SetActive(false);
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;

        ((Action)(go switch
        {
            _ when go.TryGetComponent<NPCController>(out var enemy) => () => DamagePlayer(enemy),
            _ when go.TryGetComponent<PickUpData>(out var pickUp) => () =>
            {
                ((Action)(collision.transform.GetComponent<PickUpData>().pickUpType switch
                {
                    StatType statPickUp => () => ApplyStatPickUp(statPickUp),
                    EffectType effectPickUp => () => ApplyEffectPickUp(effectPickUp),
                    BulletType bulletType => () => ApplyBulletPickUp(bulletType),
                    TileType tileType => () => ApplyTilePickUp(tileType),
                    WeatherType weatherType => () => ApplyWeatherPickUp(weatherType),
                    _ => () => { }
                }))();

                Destroy(collision.gameObject);
            }
            ,
            _ => () => { }
        }))();
    }

    private void DamagePlayer(NPCController enemy)
    {
        health -= enemy.attack - defense;
        CheckPlayerDeath();
    }

    private void CheckPlayerDeath()
    {
        if (health <= 0)
        {
            canvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void ApplyStatPickUp(StatType statPickUp) => ((Action)(statPickUp switch
    {
        Health => () => health = statPickUp.CalculateStatChange(health),
        Attack => () => playerShoot.attack = statPickUp.CalculateStatChange(playerShoot.attack),
        Defense => () => defense = statPickUp.CalculateStatChange(defense),
        Speed => () => playerMovement.SpeedChange(statPickUp.CalculateStatChange(playerMovement.speed)),
        FireRate => () => playerShoot.Timer = statPickUp.CalculateStatChange(playerShoot.Timer),
        _ => () => { }
    }))();

    private void ApplyEffectPickUp(EffectType effectPickUp)
    {
    }

    private void ApplyBulletPickUp(BulletType bulletPickUp)
    {
    }

    private void ApplyTilePickUp(TileType tilePickUp)
    {
    }

    private void ApplyWeatherPickUp(WeatherType weatherPickUp)
    {
    }
}
