using UnityEngine;

[CreateAssetMenu(fileName = "SparkGem", menuName = "Gems/SkillGem/Spark")]
public class SparkGem : SkillGem
{
    [SerializeField] int NumberOfProjectiles = 5;
    [SerializeField] float spreadAngle = 30f; // Total spread angle in degrees

    public override void Activate(Player player)
    {
        Vector2 baseDirection = GetShootingDirection(player);

        for (int i = 0; i < NumberOfProjectiles; i++)
        {
            Vector2 spawnDirection = baseDirection;

            if (NumberOfProjectiles > 1)
            {
                // Calculate spread offset
                float angleOffset = (-spreadAngle / 2f) +
                                   (spreadAngle * i / (NumberOfProjectiles - 1));
                spawnDirection = Quaternion.Euler(0, 0, angleOffset) * baseDirection;
            }

            SpawnProjectile(player.transform.position, spawnDirection.normalized);
        }
    }
}