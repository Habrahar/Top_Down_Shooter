using UnityEngine;

namespace New
{
    [CreateAssetMenu(fileName = "ShootingBehaviourConfig", menuName = "ScriptableObjects/ShootingBehaviourConfig")]
    public class ShootingBehaviourConfig : ScriptableObject
    {
        public ShootingBehaviourType shootingType;
        public int BulletSpread;

        public IShootingBehaviour GetShootingBehaviour()
        {
            switch (shootingType)
            {
                case ShootingBehaviourType.Single:
                    return new SingleShotBehaviour();
                case ShootingBehaviourType.Spread:
                    return new SpreadShotBehaviour(BulletSpread); // Пример: 3 пули
                default:
                    throw new System.NotImplementedException();
            }
        }
    }

    public enum ShootingBehaviourType
    {
        Single,
        Spread
    }
}