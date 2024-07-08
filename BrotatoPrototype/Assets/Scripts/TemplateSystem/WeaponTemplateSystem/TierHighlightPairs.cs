using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Highlight Pairs", menuName = "Templates/Tier Highlight Pairs")]
public class TierHighlightPairs : ScriptableObject
{
    [System.Serializable]
    public class TierHighlightData
    {
        public string name;
        public TierType tier;
        public HighlightProfile profile;
    }

    public TierHighlightData[] highlightProfiles;
}
