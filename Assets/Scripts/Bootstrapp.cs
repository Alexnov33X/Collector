using UnityEngine;

/// <summary>
/// Точка входа. Здесь можно четко прописать порядок инициализации всех объектов.(Не обязветельно всех, но желательно)
/// </summary>
public class Bootstrapp : MonoBehaviour
{
    void Awake()
    {
        PlayerStats.LoadPlayerData();       
    }
}
