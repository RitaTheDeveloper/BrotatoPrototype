using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class ManagerEnemyTier : MonoBehaviour
{
    [SerializeField] private Container _containerEnemy;

    private EnemySetting _lastEnemy;

    public EnemyController GetSpawnEnemy(TypeEnemy typeEnemy, TierType tierType, Vector3 position, Quaternion rotation)
    {
        EnemySetting currentEnemy = null;

        if (_lastEnemy != null && _lastEnemy.TypeEnemy == typeEnemy)
        {
            currentEnemy = _lastEnemy;
            goto Skip;
        }

        foreach (EnemySetting enemySetting in _containerEnemy.EnemiesSetting)
            if(enemySetting.TypeEnemy == typeEnemy)
                currentEnemy = enemySetting;

        if(currentEnemy == null)
            return null;

            Skip:
        EnemyTierSettingStandart enemyTierSetting = currentEnemy.GetTierEnemy(tierType);

        if(enemyTierSetting == null)
            throw new System.NotImplementedException("No tier!");

        enemyTierSetting.SetOther(this, currentEnemy.PrefabLoot);

        EnemyController enemy = Instantiate(currentEnemy.PrefabTier, position, rotation);
        _lastEnemy = currentEnemy;
        enemy.LoadPar(enemyTierSetting);

        if(currentEnemy.ViewSetting != null)
            CreateBacklight(enemy, tierType, currentEnemy);

        return enemy;
    }

    private void CreateBacklight(EnemyController enemy, TierType tierType, EnemySetting enemySetting)
    {
        HighlightEffect highlightEffect = enemy.gameObject.AddComponent<HighlightEffect>();

        TierColor tierColor = enemySetting.ViewSetting.GetColor(tierType);

        highlightEffect.outline = enemySetting.ViewSetting.Outline;
        highlightEffect.outlineQuality = enemySetting.ViewSetting.OutlineQuality;
        highlightEffect.outlineContourStyle = enemySetting.ViewSetting.OutlineContourStyle;
        highlightEffect.outlineWidth = enemySetting.ViewSetting.OutlineWidth;
        highlightEffect.outlineColor = tierColor != null ? tierColor.color1 : Color.black;
        highlightEffect.outlineBlurPasses = enemySetting.ViewSetting.OutlineBlurPasses;

        highlightEffect.glow = enemySetting.ViewSetting.Glow;
        highlightEffect.glowQuality = enemySetting.ViewSetting.OutlineQuality;
        highlightEffect.glowWidth = enemySetting.ViewSetting.OutlineWidth;
        highlightEffect.glowHQColor = tierColor != null ? tierColor.color2 : Color.black;
        highlightEffect.glowBlurMethod = enemySetting.ViewSetting.glowBlurMethod;
        highlightEffect.glowDownsampling = enemySetting.ViewSetting.GlowDownsampling;
        
        highlightEffect.highlighted = enemySetting.ViewSetting.Highlighted;
    }
}