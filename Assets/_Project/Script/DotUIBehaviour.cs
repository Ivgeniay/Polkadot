using UnityEngine;
using UnityEngine.UI;

public class DotUIBehaviour : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private Image image;

    void Start() {
        image.color = color;
    }

    void Update()
    {
        
    }
}
