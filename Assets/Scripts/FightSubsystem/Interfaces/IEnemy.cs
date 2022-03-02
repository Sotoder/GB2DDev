public interface IEnemy: IUpdatable
{
    void ChangeFightState(FightStates state);
    int Power { get; }
}
