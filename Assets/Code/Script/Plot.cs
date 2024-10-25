using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start() {
        startColor = sr.color;
    }

    private void OnMouseEnter() {

        if (tower != null) return;//se tem uma torre construida não muda a cor do Plot
        
        sr.color = hoverColor;
    }

     private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() {
       
        if (tower != null) return;//verifica se já tem uma torre construida

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if(towerToBuild.cost > LevelManager.main.currency) {
            Debug.Log("Você não pode pagar por essa torre");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);

        sr.color = startColor;//volta a cor normal quando a torre é construida
    }
}
