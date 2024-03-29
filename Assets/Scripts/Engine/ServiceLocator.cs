using Engine.ResourceManagement;
using SoloTrainGame.Core;
using UnityEngine;

namespace Engine
{
    public static class ServiceLocator
    {
        private static PrefabManager _prefabManager;
        public static PrefabManager PrefabManager
        {
            get
            {
                if (_prefabManager == null)
                {
                    Debug.LogError("PrefabManager not initialized");
                }
                return _prefabManager;
            }

            private set
            {
                _prefabManager = value;
            }
        }

        static private MaterialManager _materialManager;

        static public MaterialManager MaterialManager
        {
            get
            {
                if (_materialManager == null)
                {
                    Debug.LogError("MaterialManager not initialized");
                }
                return _materialManager;
            }

            private set
            {
                _materialManager = value;
            }
        }


        static private ScriptableObjectManager _scriptableObjectManager;

        static public ScriptableObjectManager ScriptableObjectManager
        {
            get { return _scriptableObjectManager; }
            private set { _scriptableObjectManager = value; }
        }

        static public InputManager InputManager { get; private set; }


        static ServiceLocator()
        {
            InputManager = new InputManager();
        }

        static public void SetScriptableObjectManager()
        {
            _scriptableObjectManager = new ScriptableObjectManager();
        }

        static public void SetPrefabManagerManager(Transform transform)
        {
            _prefabManager = new PrefabManager(transform);
        }

        static public void SetMaterialManager()
        {
            _materialManager = new MaterialManager();
            _materialManager.LoadColorMaterials();
        }
    }
}