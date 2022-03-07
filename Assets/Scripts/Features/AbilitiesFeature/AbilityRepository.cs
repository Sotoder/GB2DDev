using System;
using System.Collections.Generic;
using Tools;
using UnityEngine.Events;

public class AbilityRepository : BaseController, IAbilityRepository<int, IAbility>
{
    public IReadOnlyDictionary<int, IAbility> Content { get => _abilitiesMap; }
    public UnityAction<bool, IAbility> CooldownNotification { get; set; }

    private Dictionary<int, IAbility> _abilitiesMap = new Dictionary<int, IAbility>();
    private ProfilePlayer _profilePlayer;
    private IReadOnlyList<AbilityItemConfig> _abilities;

    public AbilityRepository(IReadOnlyList<AbilityItemConfig> abilities, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _abilities = abilities;

        foreach (var config in _abilities)
        {
            _abilitiesMap[config.Id] = CreateAbility(config);
            _abilitiesMap[config.Id].IsOnCooldown.SubscribeOnChange(NotifyAboutCooldownState);
        }
    }

    private void NotifyAboutCooldownState(bool isOnCooldown, IAbility ability)
    {
        CooldownNotification?.Invoke(isOnCooldown, ability);
    }

    private IAbility CreateAbility(AbilityItemConfig config)
    {
        switch (config.Type)
        {
            case AbilityType.None:
                return AbilityStub.Default;
            case AbilityType.Gun:
                return new GunAbility(config);
            case AbilityType.Speed:
                return new SpeedAbility(config, _profilePlayer.CurrentCar);
            case AbilityType.Jump:
                return new JumpAbility(config);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        foreach (var config in _abilities)
        {
            _abilitiesMap[config.Id].IsOnCooldown.UnSubscriptionOnChange(NotifyAboutCooldownState);
        }
    }
}

public class AbilityStub : IAbility
{
    private readonly AbilityItemConfig _config;
    public AbilityItemConfig Config => _config;
    public static AbilityStub Default { get; } = new AbilityStub();

    public SubscriptionPropertyWithClassInfo<bool, IAbility> IsOnCooldown { get; }

    public void Apply(IAbilityActivator activator)
    {
    }
}