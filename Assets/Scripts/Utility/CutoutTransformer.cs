using System;
using UnityEngine;

namespace Utility
{
    public class CutoutTransformer : MonoBehaviour
    {
        [SerializeField] private KeyCode activateKey = KeyCode.Alpha0;
        // [SerializeField] private KeyCode combinationKey = KeyCode.LeftControl;

        [SerializeField] private GameObject cutout;
        [SerializeField] private float stepSize = 0.1f;

        [SerializeField] private bool _active = false;

        [SerializeField] private Canvas canvas;

        [SerializeField] private string name;

        public void Start()
        {
            cutout.transform.position = new Vector3(
                PlayerPrefs.GetFloat(name + "x", cutout.transform.position.x),
                PlayerPrefs.GetFloat(name + "y", cutout.transform.position.y),
                PlayerPrefs.GetFloat(name + "z", cutout.transform.position.z)
            );
            cutout.transform.localScale = new Vector3(
                PlayerPrefs.GetFloat(name + "sx", cutout.transform.localScale.x),
                PlayerPrefs.GetFloat(name + "sy", cutout.transform.localScale.y),
                PlayerPrefs.GetFloat(name + "sz", cutout.transform.localScale.z)
            );
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(activateKey))
            {
                _active = !_active;

                // save scale and position to playerprefs
                PlayerPrefs.SetFloat(name + "x", cutout.transform.position.x);
                PlayerPrefs.SetFloat(name + "y", cutout.transform.position.y);
                PlayerPrefs.SetFloat(name + "z", cutout.transform.position.z);
                PlayerPrefs.SetFloat(name + "sx", cutout.transform.localScale.x);
                PlayerPrefs.SetFloat(name + "sy", cutout.transform.localScale.y);
                PlayerPrefs.SetFloat(name + "sz", cutout.transform.localScale.z);
            }

            if (_active)
            {
                Vector3 position = cutout.transform.position;
                Vector3 scale = cutout.transform.localScale;
                if (Input.GetKeyDown(KeyCode.W))
                {
                    cutout.transform.position = new Vector3(position.x, position.y + stepSize, position.z);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    cutout.transform.position = new Vector3(position.x, position.y - stepSize, position.z);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    cutout.transform.position = new Vector3(position.x - stepSize, position.y, position.z);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    cutout.transform.position = new Vector3(position.x + stepSize, position.y, position.z);
                }

                if (Input.GetKeyDown(KeyCode.H))
                {
                    cutout.transform.localScale = new Vector3(scale.x - stepSize / 4f, scale.y, scale.z);
                }
                else if (Input.GetKeyDown(KeyCode.K))
                {
                    cutout.transform.localScale = new Vector3(scale.x + stepSize / 4f, scale.y, scale.z);
                }
                else if (Input.GetKeyDown(KeyCode.U))
                {
                    cutout.transform.localScale = new Vector3(scale.x, scale.y + stepSize / 4f, scale.z);
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    cutout.transform.localScale = new Vector3(scale.x, scale.y - stepSize / 4f, scale.z);
                }
            }
        }
    }
}