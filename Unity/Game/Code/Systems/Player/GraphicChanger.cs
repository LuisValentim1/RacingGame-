using UnityEngine;
using JamCat.Characters;
using JamCat.Players;

public class GraphicChanger : MonoBehaviour 
{
    public SpriteRenderer spriteRenderer;
    public int offset;
    Character character;
    Player player;
    
    public void Restart(Player player) {
        this.player = player;
        character = player.getCharacter();
    }

    public void OnUpdate() {
        float inc = 360f / character.sprites.Length;
        Sprite sprite = null;

        for (float i = 0, y = 0; y < 360; i++, y += inc) {

            if (player.transform.localEulerAngles.z >= 360 - inc / 2 || player.transform.localEulerAngles.z < inc / 2) {
                sprite = character.sprites[0];
                transform.localEulerAngles = new Vector3(0, 0, offset);
            } else {
                if (player.transform.localEulerAngles.z >= y - inc / 2 && player.transform.localEulerAngles.z < y + inc / 2) {
                    sprite = character.sprites[Mathf.FloorToInt(i)];
                    transform.localEulerAngles = new Vector3(0, 0, offset - inc * i);
                }
            }

        }
        
        spriteRenderer.sprite = sprite;
    }
}