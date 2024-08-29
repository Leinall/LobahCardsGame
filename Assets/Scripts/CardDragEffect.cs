using UnityEngine;

public class CardDragEffect : MonoBehaviour
{
    [HideInInspector]
    public bool canBeDragged = false;
    private Collider cardCollider;
    private Vector3 mousePos;

    private Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = this.transform.position;
    }

    private void OnMouseDown()
    {
        if (canBeDragged)
        {
            GameReferences.instance.mainPlayer.AssignSelectedCard(this.gameObject);
            mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(this.transform.position);
        }
    }

    private void OnMouseDrag()
    {
        if (canBeDragged)
        {
            var tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePos);
            transform.position = new Vector3(tempPos.x, initialPos.y, tempPos.z);
        }
    }

}
