using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Events;

public class AbilityRepository : BaseController, IAbilityRepository<int, IAbility>
{
    public IReadOnlyDictionary<int, IAbility> Content { get => _abilitiesMap; }
    public UnityAction<bool, IAbility> CooldownNotification { get; set; }

    private Dictionary<int, IAbility> _abilitiesMap = new Dictionary<int, IAbility>();
    private ProfilePlayer _profilePlayer;

    public AbilityRepository(IReadOnlyList<AbilityItemConfig> abilities, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        foreach (var config in abilities)
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