using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.AbilitiesFeature
{
    public class AbilitiesCollectionViewStub : IAbilityCollectionView
    {
        private List<AbilityItemView> _currentViews = new List<AbilityItemView>();
        public List<AbilityItemView> AbilityViews => _currentViews;

        public event EventHandler<IItem> UseRequested;
        public void Display(IReadOnlyList<IItem> abilityItems, IAbilityRepository<int, IAbility> abilityRepository)
        {
            foreach (var item in abilityItems)
            {
                Debug.Log($"Equiped item : {item.Id}");
                UseRequested?.Invoke(this, item);
            }
        }

        public void Show()
        {
            //throw new NotImplementedException();
        }

        public void Hide()
        {
            //throw new NotImplementedException();
        }
    }
}