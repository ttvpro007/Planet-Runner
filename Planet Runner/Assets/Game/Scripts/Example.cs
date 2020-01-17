using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViTiet.Examples
{
    public class Example : MonoBehaviour
    {
        public LayerMask mask;
        public bool invertMask;

        void Update()
        {
            LayerMask newMask = ~(invertMask ? ~mask.value : mask.value);

            RaycastHit hitInfo;
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 50, newMask);
            if (hit) Debug.Log("Hit!");
        }
    }
}