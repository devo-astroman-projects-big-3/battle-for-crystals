using UnityEngine;

public class HeroTagLayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _heroBuddyGO;

    public void ChangeToDeadTag()
    {
        _heroBuddyGO.tag = "DeadPlayer";
    }
    public void ChangeToDeadLayer()
    {
        _heroBuddyGO.layer = LayerMask.NameToLayer("DeadPlayer");
    }

    public void ChangeToPlayerTag()
    {
        _heroBuddyGO.tag = "Player";
    }
    public void ChangeToPlayerLayer()
    {
        _heroBuddyGO.layer = LayerMask.NameToLayer("Player");
    }
}
