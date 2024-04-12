using UnityEngine;

public interface ISecondary
{
    void Execute(Transform transform = null);
    void Upgrade();
}

public interface IHealth
{
    void TakeDamage(float amount);
}

public class Attack : MonoBehaviour
{
    public float attackRange;
    public bool inAttackRange;
}