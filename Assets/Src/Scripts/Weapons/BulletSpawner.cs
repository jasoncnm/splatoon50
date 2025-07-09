using UnityEngine;
using UnityEngine.UIElements;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] Transform bullet;
    [SerializeField] Transform muzzleFlash;


    public void SpawnBulllet(Vector3 pos, Vector3 dir)
    {
        GunProperties gp = GetComponent<GunProperties>();

        float damage = gp.damage;
        float fallOffDistance = gp.fallOffDistance;
        float pierce = gp.pierce;
        float spread = gp.spread;

        float angle = Util.GetAngleFromDirectionalVector(dir);

        float offset = Random.Range(-spread, spread);

        angle += offset;

        Quaternion rot = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

        Transform tr = Instantiate(bullet, pos, rot);

        dir = tr.right;

        tr.GetComponent<Bullet>().Setup(dir, damage, pierce, fallOffDistance);

        muzzleFlash.GetComponent<MuzzleFlash>().Setup();

    }

}
