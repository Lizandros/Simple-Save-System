using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class MockCloudWriter : CloudWriter
    {
        public async override void SaveDataAsync(string key, string data, Action<bool> callback)
        {
            //normally here should be a save to cloud method with await operator
            InvokeDataSaved(key, callback);
        }
    }
}
