using EnumCollection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameStatus
{
    private static List<ConstructId> constructsCantBuild = new List<ConstructId>();

    public static void NewConstructBuilded(Construct construct)
    {
        ConstructId[] unlock = construct.Owned;
        for (int i = 0; i < unlock.Length; i++)
        {
            constructsCantBuild.Add(unlock[i]);
#if UNITY_EDITOR
            Debug.Log(unlock[i] + " add cant build list");
#endif
        }
    }

    public static void ConstructDestroyed(Construct construct)
    {
        ConstructId[] unlock = construct.Owned;
        for (int i = 0; i < unlock.Length; i++)
        {
            constructsCantBuild.RemoveAt(constructsCantBuild.IndexOf((unlock[i])));
#if UNITY_EDITOR
            Debug.Log(unlock[i] + " remove cant build list");
#endif
        }
    }
}
