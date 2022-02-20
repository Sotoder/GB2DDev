using Tools;
using UnityEngine;

public class SpeedAbility : IAbility
{
    private readonly AbilityItemConfig _config;
    private readonly SpeedAbilityChangeHandler _handler;
    private readonly Car _car;
    private GameObject _rocket;

    public SubscriptionPropertyWithClassInfo<bool, IAbility> IsOnCooldown { get; }

    public AbilityItemConfig Config => _config;

    public SpeedAbility(
        AbilityItemConfig abilityItemConfig,
        Car car)
    {
        _handler = new SpeedAbilityChangeHandler(abilityItemConfig);
        _car = car;
        _config = abilityItemConfig;
        IsOnCooldown = new SubscriptionPropertyWithClassInfo<bool, IAbility>(this);
    }

    public void Apply(IAbilityActivator activator)
    {
        IsOnCooldown.Value = true;

        _rocket = Object.Instantiate(_config.View);
        _handler.UpgradeWithModifier(_car);
        var durationTimer = new Timer(_config.duration);
        durationTimer.TimerIsOver += Deactivate;
        TimersList.AddTimer(durationTimer);

        var cooldownTimer = new Timer(_config.cooldown);
        cooldownTimer.TimerIsOver += EndCooldown;
        TimersList.AddTimer(cooldownTimer);
    }

    private void EndCooldown(Timer timer)
    {
        IsOnCooldown.Value = false;
        timer.TimerIsOver -= EndCooldown;
    }

    private void Deactivate(Timer timer)
    {
        _handler.DowngradeWithModifier(_car);
        GameObject.Destroy(_rocket);
        timer.TimerIsOver -= Deactivate;
    }
}