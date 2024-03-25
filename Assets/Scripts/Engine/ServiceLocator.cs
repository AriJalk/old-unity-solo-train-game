using Engine.ResourceManagement;
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

        private static ScriptableObjectManager _scriptableObjectManager;

        public static ScriptableObjectManager ScriptableObjectManager
        {
            get { return _scriptableObjectManager; }
            private set { _scriptableObjectManager = value; }
        }



        public static void SetScriptableObjectManager()
        {
            _scriptableObjectManager = new ScriptableObjectManager();
        }

        public static void SetPrefabManagerManager(Transform transform)
        {
            _prefabManager = new PrefabManager(transform);
        }
    }
}