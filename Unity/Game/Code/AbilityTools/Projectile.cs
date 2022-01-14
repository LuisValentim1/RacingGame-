using UnityEngine;

public class Projectile : MonoBehaviour 
{
    private float timer;
    private Vector2 direction;
    private float speed;
    private bool ended;


    private void Update() {
        transform.position += transform.up * speed * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer <= 0) {
            ended = true;
        }
    }

    public void setProjectile(float timer, float speed) {
        this.timer = timer;
        this.speed = speed;
        ended = false;
    }

    public void setProjectile(float timer, Vector2 direction, float speed) {
        this.timer = timer;
        this.direction = direction;
        this.speed = speed;
        ended = false;
    }

    public void setGraphics(RuntimeAnimatorController runtimeAnimatorController) {
        GetComponentInChildren<Animator>().runtimeAnimatorController = runtimeAnimatorController;
    }

    public bool getEnded() { return ended; }
}