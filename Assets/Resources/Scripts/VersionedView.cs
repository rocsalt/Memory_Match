using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionedView : MonoBehaviour, IVersioned
{
    ulong cachedVerion = 0;

    public ulong Version { get; set; } = 0;

    protected virtual void Update()
    {
        if (cachedVerion != Version)
        {
            cachedVerion = Version;
            DirtyUpdate();
        }
    }

    public virtual void DirtyUpdate()
    {
        
    }

    public void MarkDirty()
    {
        Version++;
        print(Version);
    }
}
