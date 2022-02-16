using System;
using System.Collections.Generic;
using Tools;

public class UpgradeHandlerRepository : BaseController, IRepository<int, IUpgradeCarHandler>
{
    public IReadOnlyDictionary<int, IUpgradeCarHandler> Content => _content;

    private Dictionary<int, IUpgradeCarHandler> _content = new Dictionary<int, IUpgradeCarHandler>();

    public UpgradeHandlerRepository(IReadOnlyList<UpgradeItemConfig> configs)
    {
        _content = new Dictionary<int, IUpgradeCarHandler>();
    }

    public void EquipUpgradeItems(IReadOnlyList<UpgradeItemConfig> configs)
    {
        _content.Clear();
        foreach (var config in configs)
        {
            _content[config.Id] = CreateHandler(config);
        }
    }

    private IUpgradeCarHandler CreateHandler(UpgradeItemConfig config)
    {
        switch (config.UpgradeType)
        {
            case UpgradeType.None:
                return UpgradeHandelrStub.Default;
                break;
            case UpgradeType.Speed:
                return new SpeedUpgradeHandler(config);
                break;
            case UpgradeType.Control:
                return UpgradeHandelrStub.Default;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public interface IShedController
{
    void Enter();
    void Exit();
}