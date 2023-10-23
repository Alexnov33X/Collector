using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������. ������������ ����� ������
/// </summary>
public class ServerSurrogate : MonoBehaviour
{
    public static ServerSurrogate Instance;

    public DeckOnServer currentDeckOnServer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
