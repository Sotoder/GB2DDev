using Model.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public class ShopController: BaseController
{
    private ProfilePlayer _profilePlayer;
    private ShopTools _shopTools;
    private List<ShopProduct> _productsList = new List<ShopProduct>();

    public ShopController(ProfilePlayer profilePlayer, ShopTools shopTools, List<ShopProduct> productsList)
    {
        _profilePlayer = profilePlayer;
        _shopTools = shopTools;
        _productsList = productsList;

        _shopTools.OnSuccessPurchase.SubscribeOnChange(SuccessPurshase);
        AddController(this);
    }

    private void SuccessPurshase(string productID)
    {
        switch (productID)
        {
            case "com.c1.racing.gold":
                _profilePlayer.CurentGold.Value += 100;
                break;
            default:
                break;
        }
    }

    public void StartPurchase(int internalProductID)
    {
        var shopID = _productsList.FirstOrDefault(product => product.InternalID == internalProductID).ShopId;
        if(shopID is null)
        {
            throw new Exception("Wrong product ID on purchase button");
        }
        _shopTools.Buy(shopID);
    }

    protected override void OnDispose()
    {
        _shopTools.OnSuccessPurchase.UnSubscriptionOnChange(SuccessPurshase);
        base.OnDispose();
    }
}