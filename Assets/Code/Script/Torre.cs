using System;
using UnityEngine;

[Serializable]
public class Torre
{
    public string nome; // Nome da torre
    public int custo; // Custo da torre
    public GameObject prefab; // Prefab que representa a torre no jogo

    // Construtor da torre
    public Torre(string _nome, int _custo, GameObject _prefab) {
        nome = _nome;
        custo = _custo;
        prefab = _prefab;
    }
}
