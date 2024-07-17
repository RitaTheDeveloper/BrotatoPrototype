using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradable {

    public int StartLvl { get; }
    public int CurrentLvl { get; }
    public int MaxLvl { get; }
    public void Upgrade();
    public void ResetProgress();

}
    

