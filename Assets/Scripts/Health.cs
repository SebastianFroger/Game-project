using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public PlayerData playerData;
    public float damageFlashTime = 0.2f;

    private Material _material;
    private MeshRenderer _renderer;

    void Start()
    {
        playerData.currentHP = playerData.startHP;

        _renderer = GetComponentInChildren<MeshRenderer>();
        _material = _renderer.sharedMaterial;
        _material.EnableKeyword("_EMISSION");
    }

    public void TakeDamage(int amount)
    {
        playerData.currentHP -= amount;

        StartCoroutine("DamageFlash");
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
