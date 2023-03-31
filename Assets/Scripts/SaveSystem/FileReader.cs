using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public abstract class FileReader
    {
        public abstract string Extension { get; }
        public abstract bool TryRead(string path, out Save save);
        public abstract bool TryReadFromString(string data, out Save save);
    }
}
