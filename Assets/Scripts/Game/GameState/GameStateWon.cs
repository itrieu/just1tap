
using UnityEngine;

public class GameStateWon: GameStateBase
{
   public override void Enter(UnityEngine.GameObject entity)
    {
        base.Enter(entity);
        if (UserData.getInstance().CurrentLevel >= UserData.MAX_LEVEL - 1)
        {
            m_Game.StartCoroutine(LazyZoomout(()=>{
                m_Game.m_ingameUI.setReplayActive(true);
            }));
        }
        else
        {
            m_Game.StartCoroutine(LazyZoomout(()=>{
                m_Game.m_ingameUI.setActiveWonPopup(true);
            }));
        }

    }

    private System.Collections.IEnumerator LazyZoomout(System.Action callback)
    {
        yield return new WaitForSeconds(1.5f);
        m_Game.m_gameCam.zoomout();
        yield return new WaitForSeconds(0.3f);
        callback();
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
//        if (Input.anyKeyDown || Utils.GetTouch())
//        {
//            UnityEngine.SceneManagement.SceneManager.LoadScene("game");
//        }
    }
}
