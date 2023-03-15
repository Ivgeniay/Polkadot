using UnityEngine;

namespace Assets._Project.Script
{
    public class DotBehaviour : MonoBehaviour
    {
        [field: SerializeField] public bool isSelected { get; set; }
        [SerializeField] private Color color;
    }
}
