using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private int speed;

    public MobInGame MobInGame { get; set; }

    public TowerInGame TowerInGame { get; set; }

    private void FixedUpdate()
    {
        if (this.TowerInGame != null && this.MobInGame != null && this.MobInGame.gameObject != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.MobInGame.gameObject.transform.position, this.speed * Time.deltaTime);
            if (this.transform.position.x == this.MobInGame.gameObject.transform.position.x && this.transform.position.y == this.MobInGame.gameObject.transform.position.y)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        this.speed = 1000;
    }
}