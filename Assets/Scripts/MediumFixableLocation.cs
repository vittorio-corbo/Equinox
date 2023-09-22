using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumFixableLocation : MonoBehaviour
{
    [SerializeField]
    private GameObject targetGO;

    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private LineRenderer _lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
        if (targetGO != null) {
            transform.localScale = targetGO.transform.localScale;
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.mesh = targetGO.GetComponent<MeshFilter>().mesh;
        }
        #endif
        _meshRenderer = targetGO.GetComponent<MeshRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();
        Vector3[] positions = {transform.position, targetGO.transform.position};
        _lineRenderer.SetPositions(positions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
