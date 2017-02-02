using UnityEngine;
using Tobii.EyeTracking;

/// <summary>
/// Changes the color of the game object's material, when the the game object 
/// is in focus of the user's eye-gaze.
/// </summary>
/// <remarks>
/// Referenced by the Target game objects in the Simple Gaze Selection example scene.
/// </remarks>
[RequireComponent(typeof(GazeAware))]
//[RequireComponent(typeof(SpriteRenderer))]
public class ChangeColorGaze : MonoBehaviour {

    public Color selectionColor;
	private Enemy enemy;
 
    private GazeAware       _gazeAwareComponent;
    private SpriteRenderer    _meshRenderer;

    private Color           _deselectionColor;
    private Color           _lerpColor;
    private float           _fadeSpeed = 0.1f;

    /// <summary>
    /// Set the lerp color
    /// </summary>
    void Start()
    {
		enemy = this.transform.GetComponentInParent<Enemy>();
        _gazeAwareComponent = GetComponent<GazeAware>();
		_meshRenderer = this.transform.GetComponentInParent<SpriteRenderer>();
        _lerpColor = _meshRenderer.material.color;
        _deselectionColor = Color.white;
    }

    /// <summary>
    /// Lerping the color
    /// </summary>
    void Update ()
    {

        if (_meshRenderer.material.color != _lerpColor)
        {
            _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, _lerpColor, _fadeSpeed);
        }

        // Change the color of the cube
        if (_gazeAwareComponent.HasGazeFocus)
        {
            SetLerpColor(selectionColor);
			if (Input.GetKeyDown ("space"))
				enemy.setAliveState (false);
			//Destroy (this.transform.parent.parent.gameObject);
        }
        else
        {
            SetLerpColor(_deselectionColor);
        }
    }

    /// <summary>
    /// Update the color, which should used for the lerping
    /// </summary>
    /// <param name="lerpColor"></param>
    public void SetLerpColor(Color lerpColor)
    {
        this._lerpColor = lerpColor;
    }
}
