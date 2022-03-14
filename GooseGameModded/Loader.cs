using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GooseGameModded
{
    public class Loader
    {
        private static GameObject Load;
        public static void Init()
        {
            Loader.Load = new GameObject();
            Loader.Load.AddComponent<GooseMod>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load); // Do not throw away immediately
        }

        public static void Unload()
        {
            _Unload();
        }

        private static void _Unload()
        {
            GameObject.Destroy(Load);
        }
    }
}
