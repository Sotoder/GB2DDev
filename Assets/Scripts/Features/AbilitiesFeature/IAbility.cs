using Tools;

public interface IAbility
{
    void Apply(IAbilityActivator activator);
    SubscriptionPropertyWithClassInfo<bool, IAbility> IsOnCooldown { get; }
    AbilityItemConfig Config { get; }
}