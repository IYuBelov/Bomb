using UnityEngine;
using UnityEngine.Serialization;

namespace Lib.Unity.Serialization
{
    public class SerializablePersistentObject<T> : PersistentObject where T : new()
    {
        [Header("Serialization")]
        [SerializeField] private string saveFileName = "persistentObjects.json";
    
        public T data;

        protected override void InitializePersistentObject()
        {
            base.InitializePersistentObject();
            Load();
        }

        protected virtual void OnDisable()
        {
            Save();
        }

        public void Save()
        {
            if (data == null)
            {
                data = new T();
            }
            OnSaveState();
            SerializationManager.Save(data, saveFileName);
        }

        public void Load()
        {
            if (SerializationManager.IsExists(saveFileName))
            {
                data = SerializationManager.Load<T>(saveFileName);
            }
            else
            {
                data = new T();
            }
            OnLoadState();
        }

        protected virtual void OnSaveState() { }
        protected virtual void OnLoadState() { }
    }
}