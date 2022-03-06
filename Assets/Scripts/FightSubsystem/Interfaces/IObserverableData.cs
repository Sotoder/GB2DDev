public interface IObserverableData
{
    void Attach(IUpdatable target);
    void Detach(IUpdatable target);
}