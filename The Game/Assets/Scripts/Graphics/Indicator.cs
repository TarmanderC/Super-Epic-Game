using UnityEngine;

public class Indicator : MonoBehaviour

{
    SpriteRenderer spriteRenderer;
    Enemy enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GameObject.Find("TestEnemy1").GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        spriteRenderer.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.nearbyPlayer) {
            spriteRenderer.color = Color.white;
        } else {
            spriteRenderer.color = Color.clear;
        }
    }
}
