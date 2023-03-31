using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public abstract class CloudReader
    {
        public event Action<string, string, Action<bool, Save>> DataLoaded;
        public event Action<string, Action<bool, Save>> DataLoadFailed;

        public abstract void LoadDataAsync(string key, Action<bool, Save> callback);

        protected void InvokeDataLoaded(string key, string data, Action<bool, Save> callback)
        {
            DataLoaded?.Invoke(key, data, callback);
        }

        protected void InvokeDataLoadFailed(string key, Action<bool, Save> callback)
        {
            DataLoadFailed?.Invoke(key, callback);
        }
    }
}
