using UnityEngine;
using TMPro;

public class LifeUI : MonoBehaviour
{
    public SimpleMovement player;
    public TMP_Text lifeText;

    void Update()
    {
        if (player != null)
        {
            lifeText.text = "Life: " + player.life;
        }
    }
}
