using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVersioned
{
    void MarkDirty();
    void DirtyUpdate();
    ulong Version { get; set; }
}
