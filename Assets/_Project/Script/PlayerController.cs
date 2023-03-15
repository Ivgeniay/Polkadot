using Assets._Project.Script;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask layerMask;

    [Header("Render")]
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private bool isButtonDown = false;
    [SerializeField] private List<DotBehaviour> dotBehaviours = new List<DotBehaviour>();

    void Update()
    {
        isButtonDown = Input.GetMouseButton(0);

        if (!isButtonDown) {
            lineRenderer.positionCount = 0;
            if (dotBehaviours.Count > 0)
            {
                var tempDotList = new List<DotBehaviour>();
                dotBehaviours.ForEach(el => {
                    tempDotList.Add(el);
                    });
                tempDotList.ForEach(el => {
                    RemoveLastPosition(el);
                });
            }
        }

        if (isButtonDown) {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out RaycastHit hitInfo)) {
                var dot = hitInfo.collider.GetComponent<DotBehaviour>();
                if (dot) {
                    if (AddNewPosition(dot)) {

                        lineRenderer.positionCount = dotBehaviours.Count;
                        Vector3[] vectors = new Vector3[lineRenderer.positionCount];

                        for(int i = 0; i < dotBehaviours.Count; i++) {
                            vectors[i] = dotBehaviours[i].transform.position;
                        }
                        lineRenderer.SetPositions(vectors);
                    }
                    else {
                        if (dot.isSelected)
                        {
                            if (dotBehaviours.Count > 1)
                            {
                                var transformPosition = hitInfo.collider.transform.position;
                                if (transformPosition == dotBehaviours[dotBehaviours.Count - 2].transform.position)
                                {

                                }
                            }
                        }
                    }
                }
            }
        }

        
    }

    private bool AddNewPosition(DotBehaviour dotBehaviour) {
        if (!dotBehaviours.Contains(dotBehaviour)) {
            if (!dotBehaviour.isSelected) {
                dotBehaviour.isSelected = true;
                dotBehaviours.Add(dotBehaviour);
                return true;
            }
            return false;
        }
        return false;
    }

    private void RemoveLastPosition(DotBehaviour dotBehaviour) { 
        if(dotBehaviours.Contains(dotBehaviour)) { 
            dotBehaviour.isSelected = false;
            dotBehaviours.Remove(dotBehaviour);
        }
    }
}
