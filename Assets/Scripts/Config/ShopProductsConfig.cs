using Model.Shop;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ShopListConfig", menuName = "Configs/ShopList", order = 0)]
public class ShopProductsConfig : ScriptableObject
{
    [SerializeField] List<ShopProduct> _shopProducts;

    public List<ShopProduct> ShopProducts { get => _shopProducts; }
}
