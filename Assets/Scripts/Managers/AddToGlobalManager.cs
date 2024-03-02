using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shooter
{

    public class AddToGlobalManager : MonoBehaviour
    {
        public GlobalManagerSO globalManagerSO;

        void Awake()
        {
            switch (gameObject.name)
            {
                case "Player":
                    globalManagerSO.player = gameObject;
                    break;

                case "Planet":
                    globalManagerSO.planet = gameObject;
                    globalManagerSO.attractor = gameObject.GetComponent<GravityAttractor>();
                    break;

            }
        }
    }
}
