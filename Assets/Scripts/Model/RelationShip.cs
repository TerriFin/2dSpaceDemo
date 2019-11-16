using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationShip {
    public List<string> startedWars;
    public List<string> targetOfWars;
    public HashSet<string> wars;

    public List<string> startedBlockades;
    public List<string> targetOfBlockades;
    public HashSet<string> blockades;

    public RelationShip() {
        startedWars = new List<string>();
        targetOfWars = new List<string>();
        wars = new HashSet<string>();

        startedBlockades = new List<string>();
        targetOfBlockades = new List<string>();
        blockades = new HashSet<string>();
    }
}
