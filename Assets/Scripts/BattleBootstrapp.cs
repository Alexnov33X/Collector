using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Точка входа при запуске сцены BattleScene. 
/// Здесь можно четко прописать порядок инициализации всех объектов.(Не обязятельно всех, но желательно)
/// </summary>
public class BattleBootstrapp : MonoBehaviour
{
    void Awake()
    {
        //Загружаем инфу с воображаемого сервера(В будущем вынесем в MainBootstrapp)
        PlayerStats.LoadPlayerData();
    }

}
