using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public abstract class FileWriter
    {
        public abstract string Extension { get; }
        public abstract bool TryWrite<T>(T obj, string path, int version);

        public abstract bool TryCreateSave<T>(T obj, int version, out string saveString);

        public virtual bool IsTypeSerializable(Type type)
        {
            if(!type.IsSerializable)
                return false;
            return true;
        }
    }
}