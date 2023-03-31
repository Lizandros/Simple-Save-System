using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public abstract class CloudWriter
    {
        public event Action<string, Action<bool>> DataSaved;
        public event Action<string, Action<bool>> DataSaveFailed;

        public abstract void SaveDataAsync(string key, string data, Action<bool> callback);

        protected void InvokeDataSaved(string key, Action<bool> callback)
        {
            DataSaved?.Invoke(key, callback);
        }

        protected void InvokeDataSaveFailed(string key, Action<bool> callback)
        {
            DataSaveFailed?.Invoke(key, callback);
        }
    }
}
