using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Bomb : MonoBehaviour, IPoolableObject
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed, Expanded = true)]
    [SerializeField] private BombType _bombParameters;
    private float _timer = 0f;

    public void OnSpawn()
    {
        if (_bombParameters == null)
        {
            this.gameObject.SetActive(false);
            return;
        }
    
        _timer = _bombParameters.TimeToExplode;
    }

    private void Update()
    {
        if (_timer >= 0f) 
            _timer -= Time.deltaTime;
        else
            Explode();
    }

    private void Explode()
    {
        Debug.Log("Explode");
        this.gameObject.SetActive(false);
    }
    
    public GameObject ThisGameObject => this.gameObject;
    public bool IsActiveInHierarchy => this.gameObject.activeInHierarchy;

#if UNITY_EDITOR
    
    private void OnValidate()
    {
        if (_bombParameters != null && !_bombParameters.Equals(null)) return;
        
        _bombParameters = HelperFunctions.FindDefaultParameterAssetOfType<BombType>();
    }

    private void OnDrawGizmosSelected()
    {
        if (_bombParameters == null) return;
        
        Handles.zTest = CompareFunction.LessEqual;

        var position = transform.position;

        Handles.color = new Color(1f, 0f, 0f, 0.05f);
        Handles.DrawSolidDisc(position, Vector3.up, _bombParameters.Radius);

        Handles.color = Color.red;
        Handles.DrawWireDisc(position, Vector3.up, _bombParameters.Radius);
        Vector3[] points = new[]
        {
            position,
            position + (Vector3.forward * _bombParameters.Radius),
            position,
            position + (Vector3.right * _bombParameters.Radius), 
            position,
            position + (Vector3.back * _bombParameters.Radius),
            position,
            position + (Vector3.left * _bombParameters.Radius)
        };
        
        Handles.DrawLines(points);
    }
#endif
}
