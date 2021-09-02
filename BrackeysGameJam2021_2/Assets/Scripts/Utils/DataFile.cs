using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataFile
{
    public static Dictionary<string, int> stats = new Dictionary<string, int>() {
        {"nbGoats", 0 },
        {"nbWaves", 0 },
        {"Wood", 0 },
        {"Rock", 0 },
        {"Horn", 0 },
        {"nbBuild", 0 },
        {"nbDestroyed", 0 },
    };
}
