using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MalipuladorAtualização : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private bool mouse_over = false;

    public void OnPointerEnter(PointerEventData ev) {
        mouse_over = true;
        GerenciaUI.main.AlteraStatusUI(true);
    }

    public void OnPointerExit(PointerEventData ev) {
        mouse_over = false;
        GerenciaUI.main.AlteraStatusUI(false);
        gameObject.SetActive(false);
    }
}
