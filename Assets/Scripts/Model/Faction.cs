using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour {
    public string factionName;
    public string factionTag;
    public int money;
    public bool player;

    public int maxFactionSmallCargoes;
    public int maxFactionMediumCargoes;
    public int maxFactionBigCargoes;

    public int CurrentFactionSmallCargoes { get; set; }
    public int CurrentFactionMediumCargoes { get; set; }
    public int CurrentFactionBigCargoes { get; set; }
}
