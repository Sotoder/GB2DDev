using System.Collections.Generic;

public interface IAbilityRepository
{
    IReadOnlyDictionary<int, IAbility> AbilitiesMap { get; }
}