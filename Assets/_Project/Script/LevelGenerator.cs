using Architecture.Pools.ObjectsPool;
using Assets._Project.Script;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private DotBehaviour dotBehaviour;
    [SerializeField] private Collider parentTransform;
    [SerializeField] private int dotCount;
    [SerializeField] private bool isAutoExpandable;
    private MonoPool<DotBehaviour> monoPool;

    void Start()
    {
        //monoPool = new MonoPool<DotBehaviour> (dotBehaviour, dotCount, parentTransform.transform);
        monoPool = new MonoPool<DotBehaviour> (dotBehaviour, dotCount);
        monoPool.isAutoExpandable = isAutoExpandable;

        var width = parentTransform.bounds.size.x;
        var height = parentTransform.bounds.size.y;

        var sqr = Mathf.Sqrt(dotCount);
        var stepW = width / sqr ;
        var stepH = height / sqr ;

        Debug.Log($"Width: {width}, height: {height}");

        var startPoint = parentTransform.transform.position + new Vector3(-width / 2, height / 2, 0);

        for (int i = 1; i <= sqr; i++) {
            for (int j = 1; j <= sqr; j++) {
                if (monoPool.HasFreeElement(out var _object)) {
                    _object.transform.position = (startPoint - new Vector3(stepH/2, -stepW/2, 0) ) + new Vector3(stepW * i, -stepH * j, 0);
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        var width = parentTransform.bounds.size.x;
        var height = parentTransform.bounds.size.y;

        var sqr = Mathf.Sqrt(dotCount);
        var stepW = width / sqr / 2;
        var stepH = height / sqr / 2;

        var startPoint = parentTransform.transform.position + new Vector3(-width/2, height/2, 0);

        for(int i = 1; i <= stepW; i++) {
            for(int j = 1; j <= stepH; j++ ) {
                Gizmos.DrawIcon(startPoint + new Vector3(stepW * i, -stepH * j, 0), "firstpoint");
            }
        }
    }

    void Update()
    {
        
    }
}
