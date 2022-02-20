using Tools;
using UnityEngine.Events;

public interface IAbilityRepository<Tkey, TValue> : IRepository<Tkey, TValue>
{
    UnityAction<bool, IAbility> CooldownNotification { get; set; }
}