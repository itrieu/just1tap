using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {
  
    public void playSfx(int id)
    {
        SoundManager.getInstance().playSfx(id);
    }
	
}
