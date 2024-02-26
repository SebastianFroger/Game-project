using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public int HP;
    public Material damageFlashMat;
    public float damageFlashTime = 0.1f;
    public static UnityAction<Transform> OnDeath;

    private Material[] _orgMaterials;
    private MeshRenderer[] _renderers;
    private int _curretnHP;

    protected void Start()
    {
        // Get all materials 
        _renderers = GetComponentsInChildren<MeshRenderer>();
        _orgMaterials = new Material[_renderers.Length];

        for (int i = 0; i < _renderers.Length; i++)
        {
            _orgMaterials[i] = _renderers[i].sharedMaterial;
        }

    }

    private void OnEnable()
    {
        _curretnHP = HP;
    }

    public virtual void TakeDamage(int amount)
    {
        _curretnHP -= amount;
        if (_curretnHP <= 0)
        {
            MyObjectPool.enemyA.Release(gameObject);
            OnDeath?.Invoke(transform);
            return;
        }

        StartCoroutine("DamageFlash");
    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].sharedMaterial = damageFlashMat;
        }

        yield return new WaitForSeconds(damageFlashTime);

        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].sharedMaterial = _orgMaterials[i];
        }
    }
}
