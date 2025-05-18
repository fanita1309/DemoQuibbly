using UnityEngine;
using productions; // si lo necesitas para CardOwner

public class DrawCardButtonHandler : MonoBehaviour
{
    public DrawPileManager drawPileManager; // arrastra tu DrawPileManager en el inspector
    public HandManager handManager;         // arrastra tu HandManager en el inspector
    public CardOwner owner;                 // asigna si es Player o Enemy

    public void OnDrawCardButtonClick()
    {
        if (drawPileManager != null && handManager != null)
        {
            drawPileManager.DrawCard(handManager, owner);
        }
        else
        {
            Debug.LogWarning("Faltan referencias para robar carta.");
        }
    }
}

