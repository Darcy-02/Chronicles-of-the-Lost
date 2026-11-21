using UnityEngine;

public class FighterDefense : MonoBehaviour
{
    [SerializeField] private FighterAnimation fighterAnimation;

    public bool IsBlocking { get; private set; }

    private void Awake()
    {
        if (fighterAnimation == null)
            fighterAnimation = GetComponent<FighterAnimation>();
    }

    public void Block()
    {
        IsBlocking = true;
        fighterAnimation?.PlayBlock(true);
    }

    public void StopBlock()
    {
        IsBlocking = false;
        fighterAnimation?.PlayBlock(false);
    }
}