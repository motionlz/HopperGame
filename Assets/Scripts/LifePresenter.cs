using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LifePresenter : MonoBehaviour
{
    [SerializeField] private UnitModule lifeModel;
    [SerializeField] private LifeUI lifeView;

    private async void Awake()
    {
        await Task.Delay(2);
        lifeView.InitLife(lifeModel.GetMaxHealth());
        lifeModel.OnHealthChanged += UpdateView;
    }
    
    private void UpdateView(int _currentHealth)
    {
        lifeView.UpdateLifeUI(_currentHealth);
    }
    
}
