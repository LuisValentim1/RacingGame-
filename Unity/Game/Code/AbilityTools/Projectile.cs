using UnityEngine;

public class Projectile : MonoBehaviour 
{
    public Transform spawnPoint;
    private float timer;
    private float timerToDestroy = 1f;
    private Vector2 direction;
    private float speed;
    private bool ended;


    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            ended = true;
            timerToDestroy -= Time.deltaTime;
            if (timerToDestroy <= 0)
                Destroy(gameObject);
        }else{
            transform.position += transform.up * speed * Time.deltaTime;
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