using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HP;
    public float damageFlashTime = 0.1f;

    private Material _material;
    private MeshRenderer _renderer;

    protected void Start()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        _material = _renderer.sharedMaterial;
        _material.EnableKeyword("_EMISSION");
    }

    public virtual void TakeDamage(int amount)
    {
        HP -= amount;
        StartCoroutine("DamageFlash");

        if (HP <= 0)
            Destroy(gameObject);
    }

    IEnumerator DamageFlash()
    {
        _material.SetColor("_EmissionColor", Color.red * 1000f);
        yield return new WaitForSeconds(damageFlashTime);
        _material.SetColor("_EmissionColor", Color.black);
    }

    private void OnDisable()
    {
        _material.SetColor("_EmissionColor", Color.black);
    }
}
