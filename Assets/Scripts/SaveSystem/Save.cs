using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class Save
    {
        public readonly int version;
        public readonly SerializedData data;

        public Save(int version, SerializedData data)
        {
            this.version = version;
            this.data = data;
        }
    }
}
