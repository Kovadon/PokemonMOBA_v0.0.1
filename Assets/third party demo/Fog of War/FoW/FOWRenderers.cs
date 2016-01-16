using UnityEngine;

/// <summary>
/// Adding a Fog of War Renderer to any game object will hide that object's renderers if they are not visible according to the fog of war.
/// </summary>

[AddComponentMenu("Fog of War/Renderers")]
public class FOWRenderers : MonoBehaviour
{
	private Transform mTrans;
    private Renderer[] mRenderers;
    private float mNextUpdate = 0f;
    private bool mIsVisible = true;
    private bool mUpdate = true;

    /// <summary>
    /// The checking rate to hide/display a gameobject (in seconds).
    /// </summary>
    public float refreshRate = 0.1f;

	/// <summary>
	/// Whether the renderers are currently visible or not.
	/// </summary>

	public bool isVisible { get { return mIsVisible; } }

	/// <summary>
	/// Rebuild the list of renderers and immediately update their visibility state.
	/// </summary>

	public void Rebuild ()
    {
        mUpdate = true;
    }

	void Awake ()
    {
        mTrans = transform;
    }

	void LateUpdate ()
    {
        if (mNextUpdate < Time.time)
            UpdateNow();
    }

	void UpdateNow ()
	{
		mNextUpdate = Time.time + refreshRate;

		if (FOWSystem.instance == null)
		{
			enabled = false;
			return;
		}

		if (mUpdate)
            mRenderers = GetComponentsInChildren<Renderer>();

		bool visible = FOWSystem.instance.IsVisible(mTrans.position);

		if (mUpdate || mIsVisible != visible)
		{
			mUpdate = false;
			mIsVisible = visible;

			for (int i = 0, imax = mRenderers.Length; i < imax; ++i)
			{
				Renderer ren = mRenderers[i];

				if (ren)
				{
					ren.enabled = mIsVisible;
				}
				else
				{
					mUpdate = true;
					mNextUpdate = Time.time;
				}
			}
		}
	}
}