using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bridge : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public Transform root;
    public GameObject bridgePrefab;
    public GameObject barrierPrefab;
    public LayerMask includedLayerMask;
    public float slerpValue = 0.1f;
    public float bridgeCooldownTime = 10f;
    public float barrierCooldownTime = 10f;

    Vector3 _inputDir;
    GameObject _instance;
    Vector3 _lastInputDir;
    bool _bridgeActive;
    bool _barrierActive;

    private void Start()
    {
        unitStatsSO.bridgeCooldownTime = 0;
        unitStatsSO.barrierCooldownTime = 0;
    }

    void OnBridgeControl(InputValue value)
    {
        _inputDir = new Vector3(value.Get<Vector2>().x, 0f, value.Get<Vector2>().y);
    }

    void OnBridgeInstantiate()
    {
        if (unitStatsSO.bridgeCooldownTime > 0)
            return;

        if (_barrierActive) return;

        if (_instance == null)
        {
            _instance = MyObjectPool.Instance.GetInstance(bridgePrefab);
            _instance.transform.position = transform.position + transform.forward * (_instance.transform.localScale.z / 2 + 2);
            _lastInputDir = Vector3.forward;
            _bridgeActive = true;
        }
        else
        {
            _instance.transform.parent = root;
            _instance = null;
            GlobalObjectsManager.Instance.navMeshSurface.BuildNavMesh();
            _bridgeActive = false;

            unitStatsSO.bridgeCooldownTimeMax = bridgeCooldownTime;
            unitStatsSO.bridgeCooldownTime = bridgeCooldownTime;
        }
    }

    void OnBarrierInstantiate()
    {
        if (unitStatsSO.barrierCooldownTime > 0)
            return;

        if (_bridgeActive) return;

        if (_instance == null)
        {
            _instance = MyObjectPool.Instance.GetInstance(barrierPrefab);
            _instance.transform.position = transform.position + transform.forward * 5;
            _lastInputDir = Vector3.forward;
            _barrierActive = true;
        }
        else
        {
            _instance.transform.parent = root;
            _instance = null;
            GlobalObjectsManager.Instance.navMeshSurface.BuildNavMesh();
            _barrierActive = false;

            unitStatsSO.barrierCooldownTimeMax = barrierCooldownTime;
            unitStatsSO.barrierCooldownTime = barrierCooldownTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (unitStatsSO.bridgeCooldownTime > 0)
        {
            unitStatsSO.bridgeCooldownTime -= Time.deltaTime;
        }

        if (unitStatsSO.barrierCooldownTime > 0)
        {
            unitStatsSO.barrierCooldownTime -= Time.deltaTime;
        }

        if (_instance != null)
        {
            if (_inputDir != Vector3.zero)
            {
                _lastInputDir = _inputDir;
            }

            if (_bridgeActive)
            {
                _instance.transform.position = transform.position + -Vector3.up + _lastInputDir * (_instance.transform.localScale.z / 2 + 2);
                _instance.transform.rotation = Quaternion.Slerp(_instance.transform.rotation, Quaternion.LookRotation(_lastInputDir), slerpValue);
            }

            if (_barrierActive)
            {
                _instance.transform.position = transform.position + new Vector3(0, 0, 0) + _lastInputDir * 5;
                _instance.transform.rotation = Quaternion.Slerp(_instance.transform.rotation, Quaternion.LookRotation(_lastInputDir), slerpValue);
            }
        }
    }
}
