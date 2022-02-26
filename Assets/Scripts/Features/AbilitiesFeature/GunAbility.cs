using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Tools;
using DG.Tweening;

public class GunAbility : IAbility
{
    private readonly AbilityItemConfig _config;
    private readonly Rigidbody2D _viewPrefab;
    public SubscriptionPropertyWithClassInfo<bool, IAbility> IsOnCooldown { get; }

    public AbilityItemConfig Config => _config;

    public GunAbility(
        AbilityItemConfig abilityItemConfig)
    {
        _config = abilityItemConfig;
        _viewPrefab = abilityItemConfig.View.GetComponent<Rigidbody2D>();
        if (_viewPrefab == null) throw new InvalidOperationException($"{nameof(GunAbility)} view requires {nameof(Rigidbody2D)} component!");
        IsOnCooldown = new SubscriptionPropertyWithClassInfo<bool, IAbility>(this);
    }

    public void Apply(IAbilityActivator activator)
    {
        IsOnCooldown.Value = true;
        var projectile = Object.Instantiate(_viewPrefab);
        projectile.AddForce(activator.GetViewObject().transform.right * _config.value, ForceMode2D.Impulse);
        var seq = DOTween.Sequence().AppendInterval(_config.duration).OnComplete(() => Object.Destroy(projectile.gameObject));

        var cooldownTimer = new Timer(_config.cooldown);
        cooldownTimer.TimerIsOver += EndCooldown;
        TimersList.AddTimer(cooldownTimer);
    }

    private void EndCooldown(Timer timer)
    {
        IsOnCooldown.Value = false;
        timer.TimerIsOver -= EndCooldown;
    }
}