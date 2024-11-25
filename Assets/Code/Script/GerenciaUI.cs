using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GerenciaUI : MonoBehaviour
{
    public static GerenciaUI main;

    private bool estaAtivoUI;

    private void Awake () {
        main = this;
    }

    public void AlteraStatusUI (bool status) {
        estaAtivoUI = status;
    }

    public bool EstaAtivoUI () {
        return estaAtivoUI;
    }
}
