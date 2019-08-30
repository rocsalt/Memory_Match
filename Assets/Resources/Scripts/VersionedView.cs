using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionedView : MonoBehaviour, IVersioned
{
    ulong cachedVerion = 0;
    ulong version = 0;


    protected virtual void Update()
    {
        if (cachedVerion != Version)
        {
            cachedVerion = Version;
            DirtyUpdate();
        }
    }

    public ulong Version { get => version; set => version = value; } // see pt9 7:26 for original if this shorthand doesn't work

    public virtual void DirtyUpdate()
    {
        
    }

    public void MarkDirty()
    {
        Version++;
        print(Version);
    }
}
