using DG.Tweening;
using Tools;

internal class JumpAbility : IAbility
{
    private AbilityItemConfig _config;

    public JumpAbility(AbilityItemConfig config)
    {
        _config = config;

        IsOnCooldown = new SubscriptionPropertyWithClassInfo<bool, IAbility>(this);
    }

    public SubscriptionPropertyWithClassInfo<bool, IAbility> IsOnCooldown { get; }

    public AbilityItemConfig Config => _config;

    public void Apply(IAbilityActivator activator)
    {
        IsOnCooldown.Value = true;

        var seq = DOTween.Sequence();
        seq.Append(activator.GetViewObject().transform.DOJump(activator.GetViewObject().transform.position, _config.value, 1, _config.duration));

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