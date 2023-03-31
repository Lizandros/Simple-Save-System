using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

namespace SaveSystem
{
    public class MockCloudReader : CloudReader
    {
        public async override void LoadDataAsync(string key, Action<bool, Save> callback)
        {
            //normally here should be a method with await operator
            string data = "{\"version\":1,\"data\":{\"data\":\"{\\\"name\\\":\\\"Stachu\\\",\\\"longestJump\\\":64.3,\\\"equipment\\\":{\\\"helmet\\\":{\\\"id\\\":0,\\\"hexColor\\\":\\\"#FFFF00\\\"},\\\"skiJumpingSuit\\\":{\\\"id\\\":0,\\\"hexColor\\\":\\\"#00FFFF\\\"},\\\"skis\\\":{\\\"id\\\":0,\\\"hexColor\\\":\\\"#FF0000\\\"}}}\"}}";
            InvokeDataLoaded(key, data, callback);
        }
        
}
}
