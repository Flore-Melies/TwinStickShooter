using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject deathCanvas, winCanvas;

    private Vector2 direction;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var position = (Vector2) transform.position;
        direction = ((Vector2)player.transform.position - position).normalized;
        var aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
        position += direction * (speed * Time.deltaTime);
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(deathCanvas);
        }
        else if (other.gameObject.CompareTag("Projectile"))
        {
            Instantiate(winCanvas);
            Destroy(player);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
