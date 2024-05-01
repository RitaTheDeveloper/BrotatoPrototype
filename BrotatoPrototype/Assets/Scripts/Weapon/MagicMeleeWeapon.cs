using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMeleeWeapon : MeleeWeapon
{
    [SerializeField] private Explosion _explosionPrefab;
    [SerializeField] private DrawCircle drawRadius;
    [SerializeField] private float _radiusOfAoE;
    private Transform _player;

    private void Start()
    {
        Init();
        _player = GameManager.instance.player.transform;
        if (drawRadius)
        {
            drawRadius.DrawTheCircle(_radiusOfAoE);
            drawRadius.gameObject.SetActive(false);
        }
        
        _timer = 0;
    }

    private void FixedUpdate()
    {
        if (Time.time > _nextShotTime)
        {
            Attack();
            _nextShotTime = Time.time + 1 / currentAttackSpeed;
        }

        _timer += Time.deltaTime;

        if(_timer >= 0.5f && drawRadius)
        {
            drawRadius.gameObject.SetActive(false);
        }
    }

    protected override void Attack()
    {
        if (animator)
        {
            animator.SetTrigger("Hit");
        }
        SetAttackSpeed();
        SetAnimationSpeed(currentAttackSpeed);
        SetTimeOfAnimation(currentAttackSpeed);
        SetDamage();
        SetCritChance();

        

        Vector3 position = new Vector3(_player.position.x , 0.3f, _player.position.z);
        _explosionPrefab.Explode(position, _radiusOfAoE, currentDamage);
        if (drawRadius)
        {
            drawRadius.gameObject.SetActive(true);
        }        
        _timer = 0f;
        base.Attack();
    }
}
