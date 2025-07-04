using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] Transform bullet;
    [SerializeField] Transform muzzleFlash;


    public void SpawnBulllet(Vector3 pos, Vector3 dir)
    {

        Quaternion rot = Quaternion.AngleAxis(Util.GetAngleFromDirectionalVector(dir), new Vector3(0, 0, 1));

        Transform tr = Instantiate(bullet, pos, rot);

        tr.GetComponent<Bullet>().Setup(dir);

        muzzleFlash.GetComponent<MuzzleFlash>().Setup();

    }
}
