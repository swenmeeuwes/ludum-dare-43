using UnityEngine;

[CreateAssetMenu(fileName = "Level Settings")]
public class LevelSettings : ScriptableObject {
    public bool LimitedAvailableBodies;
    public int AvailableBodies = 1;
}
