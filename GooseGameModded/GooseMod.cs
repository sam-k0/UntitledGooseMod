using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GooseGameModded
{
    class GooseMod : MonoBehaviour
    {
        // Alloc vars
        const string VERSIONSTR = "1.2";
        Goose goose = null;
        string sceneName = "undefined";
        bool gooseFound = false;

        String objectListString = "Press 1 to scan";

        public void OnGUI() // Draw-Step
        {
            // show current scene
            GUI.Label(new Rect(0f, 0f, 200f, 60f), "UntitledGooseMod" + VERSIONSTR +"\n" + " | Scene: " + sceneName + "\n | Goose found: " + Convert.ToString(gooseFound));

            // Show FPS
            GUI.Label(new Rect(0f, 60f, 200f, 120f), "Performance: " + Convert.ToString(1.0f / Time.deltaTime));

            // Obj List
            GUI.Label(new Rect(0f, 120f, 500f, 1000f), objectListString);
        }

        public void Start() // On-Create
        {
            
        }

        public void Update() // Step event
        {
            if (Input.GetKeyDown(KeyCode.Delete)) // Unload from Game
            {
                Loader.Unload();
            }

            if( Input.GetKeyDown(KeyCode.Alpha0))
            {
                // Get scene name
                Scene cScene = SceneManager.GetActiveScene();
                sceneName = cScene.name;

                // Get goose
                goose = FindObjectOfType<Goose>();
                if(goose != null)
                {
                    MeshRenderer renderer = goose.GetComponent<MeshRenderer>();
                    Vector3 gooseSize = renderer.bounds.size;
                    gooseSize.x += 3;
                    gooseFound = true;
                }
                else
                {
                    gooseFound = false;
                }
                
            }

            if(Input.GetKeyDown(KeyCode.Alpha1)) // Scan all objects
            {
                GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                foreach (GameObject go in allObjects)
                {
                    if (go.activeInHierarchy)
                    {
                        objectListString += go.name + "|";
                    }
                }
                   
            }
        }
    }
}
