using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resistance", menuName = "Resistances")]
public class Resistances : ScriptableObject {

    public float physicalResistance;

    public float burnResistance;

    public float freezeResistance;

    public float crowdControlResistance;

    public float bleedResistance;

    public float venomResistance;

    /// 
    /// DEFAULT RESISTANCES: Resistances querried when no resistance is defined 
    /// 
    public static float defaultPhysicalResistance = 0;

    public static float defaultBurnResistance = 0;

    public static float defaultFreezeResistance = 0;

    public static float defaultCrowdControlResistance = 0;

    public static float defaultBleedResistance = 0;

    public static float defaultVenomResistance = 0;

    public float GetResistanceTo(int resistanceID) {
        switch (resistanceID) {
            default:
            case Resistance.PHYSICAL:
                return physicalResistance;
            case Resistance.BURNING:
                return burnResistance;
            case Resistance.FREEZING:
                return freezeResistance;
            case Resistance.CROWD_CONTROL:
                return crowdControlResistance;
            case Resistance.BLEEDING:
                return bleedResistance;
            case Resistance.VENOM:
                return venomResistance;
            case Resistance.UNRESISTABLE:
                return 100f;
        }
    }
    
    public static float GetDefaultResistanceTo(int resistanceID) {
        switch (resistanceID) {
            default:
            case Resistance.PHYSICAL:
                return defaultPhysicalResistance;
            case Resistance.BURNING:
                return defaultBurnResistance; 
            case Resistance.FREEZING:
                return defaultFreezeResistance;
            case Resistance.CROWD_CONTROL:
                return defaultCrowdControlResistance;
            case Resistance.BLEEDING:
                return defaultBleedResistance;
            case Resistance.VENOM:
                return defaultVenomResistance;
            case Resistance.UNRESISTABLE:
                return 100f;
        }
    }
}
