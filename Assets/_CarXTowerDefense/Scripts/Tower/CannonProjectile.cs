using UnityEngine;
using System.Collections;
using _CarXTowerDefense.Scripts.Tower;

namespace _CarXTowerDefense.Scripts.Tower
{
    public class CannonProjectile : Projectile
    {
        protected override void Move()
        {
            transform.Translate(Vector3.forward * (speed * Time.fixedDeltaTime), Space.Self);
        }
    }
}
