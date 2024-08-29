using UnityEngine;

public class GameReferences : MonoBehaviour
{
    public UIHandler uiHandler;
    public AssemblyPointBeh assemblyPoint;
    public MainPlayerBehaviour mainPlayer;

    public static GameReferences instance;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }
}
