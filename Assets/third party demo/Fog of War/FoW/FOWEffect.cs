using UnityEngine;

/// <summary>
/// Fog of War system needs 3 components in order to work:
/// - Fog of War system that will create a height map of your scene and perform all the updates.
/// - Fog of War Image Effect on the camera that will be displaying the fog of war (this class).
/// - Fog of War Revealer on one or more game objects in the world.
/// </summary>

[RequireComponent(typeof(Camera))]
[AddComponentMenu("Fog of War/Image Effect")]
public class FOWEffect : MonoBehaviour
{
	/// <summary>
	/// Shader used to create the effect. Should reference "Image Effects/Fog of War".
	/// </summary>

	public Shader shader;

	/// <summary>
	/// Color tint given to unexplored pixels.
	/// </summary>
	
	public Color unexploredColor = new Color(0.05f, 0.05f, 0.05f, 1f);

	/// <summary>
	/// Color tint given to explored (but not visible) pixels.
	/// </summary>

	public Color exploredColor = new Color(0.2f, 0.2f, 0.2f, 1f);

	FOWSystem mFog;
	Camera mCam;
	Matrix4x4 mInverseMVP;
	Material mMat;

	/// <summary>
	/// The camera we're working with needs depth.
	/// </summary>

	void OnEnable ()
	{
		mCam = GetComponent<Camera>();
		mCam.depthTextureMode = DepthTextureMode.Depth;
		if (shader == null) shader = Shader.Find("Image Effects/Fog of War");
	}

	/// <summary>
	/// Destroy the material when disabled.
	/// </summary>

	void OnDisable () { if (mMat) DestroyImmediate(mMat); }

	/// <summary>
	/// Automatically disable the effect if the shaders don't support it.
	/// </summary>

	void Start ()
	{
		if (!SystemInfo.supportsImageEffects || !shader || !shader.isSupported)
		{
			enabled = false;
		}
	}

	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (mFog == null)
		{
			mFog = FOWSystem.instance;
			if (mFog == null) mFog = FindObjectOfType(typeof(FOWSystem)) as FOWSystem;
		}

		if (mFog == null || !mFog.enabled)
		{
			enabled = false;
			return;
		}

		// Calculate the inverse modelview-projection matrix to convert screen coordinates to world coordinates
		mInverseMVP = (mCam.projectionMatrix * mCam.worldToCameraMatrix).inverse;

		float invScale = 1f / mFog.worldSize;
		Transform t = mFog.transform;
		float x = t.position.x - mFog.worldSize * 0.5f;
		float z = t.position.z - mFog.worldSize * 0.5f;

		if (mMat == null)
		{
			mMat = new Material(shader);
			mMat.hideFlags = HideFlags.HideAndDontSave;
		}

		Vector4 camPos = mCam.transform.position;

		// This accounts for Anti-aliasing on Windows flipping the depth UV coordinates.
		// Despite the official documentation, the following approach simply doesn't work:
		// http://docs.unity3d.com/Documentation/Components/SL-PlatformDifferences.html

		if (QualitySettings.antiAliasing > 0)
		{
			RuntimePlatform pl = Application.platform;

			if (pl == RuntimePlatform.WindowsEditor ||
				pl == RuntimePlatform.WindowsPlayer ||
				pl == RuntimePlatform.WindowsWebPlayer)
			{
				camPos.w = 1f;
			}
		}

		Vector4 p = new Vector4(-x * invScale, -z * invScale, invScale, mFog.blendFactor);
		mMat.SetColor("_Unexplored", unexploredColor);
		mMat.SetColor("_Explored", exploredColor);
		mMat.SetVector("_CamPos", camPos);
		mMat.SetVector("_Params", p);
		mMat.SetMatrix("_InverseMVP", mInverseMVP);
		mMat.SetTexture("_FogTex0", mFog.texture0);
		mMat.SetTexture("_FogTex1", mFog.texture1);

		Graphics.Blit(source, destination, mMat);
	}
}