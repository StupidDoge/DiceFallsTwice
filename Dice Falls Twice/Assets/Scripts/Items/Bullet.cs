using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    [SerializeField] private bool _playerBullet;

    private enum Tags { Wall, Player, EnemyCollider }

    public int Damage 
    {
        set { _damage = value; }
    }


    void Update()
    {
        transform.Translate(new Vector3(0, 0, _speed * Time.deltaTime));
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<TopDownController>(out TopDownController player)) 
        {
            player.HealthSystem.TakeDamage(_damage);
            if (player.HealthSystem.GetHealth() <= 0)
            {
                player.gameObject.GetComponent<PlayerAttack>().enabled = false;
                player.gameObject.GetComponent<InputHandler>().enabled = false;
                player.gameObject.GetComponent<TopDownController>().enabled = false;
                player.gameObject.GetComponent<PlayerWeaponManager>().enabled = false;
                player.gameObject.SetActive(false);
            }
        }

        if (collision.tag == Tags.Wall.ToString())
            Debug.Log("Wall hit");

        if (collision.tag == Tags.Player.ToString())
            Debug.Log("Player hit");

        /*if (collision.tag == Tags.EnemyCollider.ToString())
        {
            Debug.Log("Enemy hit");
        }*/

        if (collision.TryGetComponent<EnemyAI>(out EnemyAI enemy) && _playerBullet)
        {
            enemy.HealthSystem.TakeDamage(_damage);
            Debug.Log(enemy.HealthSystem.GetHealth());
            enemy.CheckHealth();
        }

        Destroy(gameObject);
    }
}
