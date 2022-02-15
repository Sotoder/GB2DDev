internal class SpeedAbilityCangeHandler
{
    private AbilityItemConfig _config;

    public SpeedAbilityCangeHandler(AbilityItemConfig abilityItemConfig)
    {
        _config = abilityItemConfig;
    }

    public IUpgradeableCar UpgradeWithModifier(IUpgradeableCar car)
    {
        car.Speed *= _config.value;
        return car;
    }

    public IUpgradeableCar DowngradeWithModifier(IUpgradeableCar car)
    {
        car.Speed /= _config.value;
        return car;
    }
}