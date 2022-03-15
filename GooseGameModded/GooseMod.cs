using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

namespace GooseGameModded
{
    class GooseMod : MonoBehaviour
    {
        // Alloc vars
        const string VERSIONSTR = "1.5";
        
        string sceneName = "undefined";

        string objectListString = "empty";

        // Goose found variable references
        bool gooseFound = false; // If a goose has been found in scene

        GameObject goose = null; // Reference to a goose
        
        Vector3 goosePosition = new Vector3(-1f, -1f, -1f); // Goose position

        Rigidbody gooseRB = null; // Goose Rigidbody

        Goose gooseGooseComponent = null;

        bool gooseCheatActive = false;


        public void OnGUI() // Draw-Step
        {
            // show current scene
            GUI.Label(new Rect(0f, 0f, 200f, 200f), "UntitledGooseMod" + VERSIONSTR
                                                    + "\n| Scene: " + sceneName + "| Goose found: " + Convert.ToString(gooseFound)
                                                    + "\n| Goose X: " + goosePosition.x + " Y: " + goosePosition.y + " Z: " + goosePosition.z);

            // Show FPS
            GUI.Label(new Rect(0f, 60f, 200f, 120f), "Performance: " + Convert.ToString(1.0f / Time.deltaTime));

        }

        public void Start() // On-Create
        {
            
        }

        public void Update() // Step event
        {
            // Unload from game
            if (Input.GetKeyDown(KeyCode.Delete)) // Unload from Game
            {
                Loader.Unload();
            }

            // Try to get a goose-reference
            if( Input.GetKeyDown(KeyCode.Alpha0))
            {
                // Get scene name
                Scene cScene = SceneManager.GetActiveScene();
                sceneName = cScene.name;

                // Get goose
                goose = GameObject.Find("Goose");
                if(goose != null)
                {              
                    gooseFound = true;
                    updateGooseVariables(); // Update the goose vars after finding the goose
                }
                else
                {
                    gooseFound = false;
                }
                
            }

            // Scan all objects / Dbug
            if (Input.GetKeyDown(KeyCode.Alpha1)) 
            {
                dumpGameObjectScripts();
                dumpGameObjectsOld();           
            }

            // Toggle goose cheat mode
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                if(!gooseFound) // If no goose is set, return
                {
                    return;
                }

                // Toggle goose cheat mode
                gooseCheatActive = !gooseCheatActive;
            }

            // Call various scripts
            updateGooseShenanigans();
        }

        public void updateGooseVariables() // This updates the Goose-specific variables
        {
            if(gooseFound)
            {
                goosePosition.x = goose.transform.position.x;
                goosePosition.y = goose.transform.position.y;
                goosePosition.z = goose.transform.position.z;
                gooseRB = goose.GetComponent<Rigidbody>();
                gooseGooseComponent = goose.GetComponent<Goose>();
            }
        }
        public void updateGooseShenanigans() // This does the funny hehe
        {
            // Cheat mode + goose found = chaos
            if(gooseCheatActive && gooseFound)
            {
                if(gooseGooseComponent.isRunning) // Deja-Goose, I've just been in this place before
                {
                    gooseGooseComponent.mover.currentSpeed = 5;
                }
                
            }
        }

        #region Debugging
        public void dumpGameObjectScripts()
        {
            string log = "";
            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                foreach (MonoBehaviour comp in obj.GetComponents<MonoBehaviour>())
                {
                    log = string.Concat(new string[]
                    {
                        log,
                        obj.name,
                        " - ",
                        comp.GetType().Name,
                        "\n"
                    });
                }
            }
            File.WriteAllText("ObjectScriptDump.txt", (log));
        }

        public void dumpGameObjectsOld()
        {
            objectListString = ""; // Reset string
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.activeInHierarchy)
                {
                    objectListString += "GOName:" + go.name + "|TFullName:" + go.GetType().FullName + "|Tag:" + go.tag + " |\n";
                }
            }

            File.WriteAllText("objectDumpOld.txt", (objectListString));
        }

        #endregion Debugging
    }
}
