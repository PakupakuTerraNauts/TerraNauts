using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxBackground : MonoBehaviour
{
	[HideInInspector]
	[SerializeField]
	bool isInitialized = false;

//���炩���ߊe�摜��R�t�����Ă����B
	[Header("�w�i�摜 (0���ŉ��A���Ɏ�O)")]
	[SerializeField]
	Sprite[] backgroundSprites = new Sprite[3];

//���E�X�N���[���Ή��̏ꍇ�́A�eX�l��-1920(1�摜����)�ɐݒ肵�Ă��������B
	[Header("�w�i�摜�̃I�t�Z�b�g (�Y�����l)(���E�X�N���[���Ή��̏ꍇ��1�摜���A���ɃY����)")]
	[SerializeField]
	Vector2[] backgroundOffsets = new Vector2[] {
		new Vector2(0, 0),
		new Vector2(0, -400),
		new Vector2(0, -350),
	};

//�T���v���Ŏg�p�����A[Ansimuz] GothicVania Cemetery�̏ꍇ�̉摜�T�C�Y�Ȃ̂ŁA�����̎g�p�A�Z�b�g�ɉ����Ē������Ă��������B
	[Header("�w�i�摜�̃T�C�Y")]
	[SerializeField]
	Vector2[] backgroundSpriteSizes = new Vector2[] {
		new Vector2(1920, 1120),
		new Vector2(1920, 1790),
		new Vector2(1920, 615),
	};

	[Header("�w�i�摜�̃X�N���[���� (��(0)�̕��������߂Ɏw��)")]
	[SerializeField]
	float[] scrollRates = new float[] {
		1.0f,
		3.0f,
		5.0f,
	};

//3D�I�u�W�F�N�g(�L�����N�^�[��)��艜�ɂȂ�悤�ɒ����B�J�����̈ʒu��3D�I�u�W�F�N�g�̃T�C�Y�ɂ�邪30�`�ݒ肷��Ηǂ��B
	[Header("�J��������UI�ւ̋��� (�J�����̈ʒu��3D�I�u�W�F�N�g�̃T�C�Y�ɂ�邪30�`�w��)")]
	[Range(30.0f, 100.0f)]
	[SerializeField]
	float planeDistance = 30.0f;

	[Header("�w�i�摜�����E�ɉ��z�u���邩 (�E�X�N���[���݂̂Ȃ�2�A���E�X�N���[���Ή��Ȃ�3)")]
	[Range(2, 3)]
	[SerializeField]
	int imageMax = 2;

	[Header("�X�N���[������")]
	[Range(0.1f, 3.0f)]
	[SerializeField]
	float scrollDuration = 1.0f;
	float smoothTime;

	[Header("�X�N���[�����x�̏�� (����deltaTime���|����̂ő傫�߂Ɏw��)")]
	[Range(500.0f, 10000.0f)]
	[SerializeField]
	float scrollSpeedMax = 1000.0f;

//�e�w�i�摜��RectTransform�B
	[HideInInspector]
	[SerializeField]
	RectTransform[] backgroundsRt;

//�w�i�摜���B
	[HideInInspector]
	[SerializeField]
	int backgroundMax;

//�e�w�i�摜���X�N���[�������ʁB
	[HideInInspector]
	[SerializeField]
	float[] backgroundScrollValues;

//RectMask2D��L���ɂ�����ԂŎ��s����ƁA�X�N���[�����Ă���ʊO�ɐݒu�����摜����\���ɂȂ�d�l���ۂ��̂ŁA���s���ɗL�������Ă���B
	[HideInInspector]
	[SerializeField]
	RectMask2D parallaxBackgroundRectMask2D;

	//�X�N���[���o�ߎ��ԁB
	float scrollElapsedTime;

	//�X�N���[�������x�BSmoothDamp�ɕK�v�B
	[HideInInspector]
	[SerializeField]
	Vector2[] scrollVelocities;

//�R���[�`���̊Ǘ��Ɏg�p�B
	Coroutine scroll;

//�O�ɃX�N���[�����Ă΂ꂽ���̃v���C���[�̈ʒu�B
//�x�e���̃X�e�[�W �̈ړ��Œl��ێ����Ă������߂�static
	static Vector3 previousPlayerPosition = Player.playerStartPos;

//�ꎞ�I�Ɏg�p�B
	Canvas parallaxBackgroundCanvas;
	GameObject parallaxBackgroundGo;
	RectTransform parallaxBackgroundRt;

	GameObject tempBackgroundGo;
	RectTransform tempBackgroundRt;
	Image tempBackgroundImg;
	Vector2 tempBackgroundPosition;
	Vector2 tempBackgroundsPosition;

	void Awake()
	{
		if (!isInitialized)
			CreateParallaxBackground();

		parallaxBackgroundRectMask2D.enabled = true;

//SmoothDamp��smoothTime�ƁA�X�N���[���̒����������ɂ͈Ⴄ�̂ŁA���菬�����v�Z���Ă����B
		smoothTime = scrollDuration * 0.85f;
	}

//�w�i�摜���X�N���[���������ꍇ�ɃR�����ĂԁB�����ɂ̓v���C���[�̈ʒu��n��(�ʒu���łȂ�)�B
	public void StartScroll(Vector3 playerPosition)
	{
//�u�E�X�N���[���݂̂ɑΉ��v���[�h�̎��A�v���C���[�����ɐi�񂾏ꍇ�͖�������B
		if (imageMax == 2 && playerPosition.x - previousPlayerPosition.x < 0)
			return;
//1�摜���i�񂾎��A�X�N���[�����q����悤�ɗǂ������ɖ߂��Ă���B
		for (int i = 0; i < backgroundMax; i++) {

			backgroundScrollValues[i] -= (playerPosition.x - previousPlayerPosition.x) * scrollRates[i];
			if (backgroundSpriteSizes[i].x < backgroundsRt[i].anchoredPosition.x) {
				backgroundScrollValues[i] -= backgroundSpriteSizes[i].x;
				tempBackgroundsPosition.Set(backgroundSpriteSizes[i].x, 0);
				backgroundsRt[i].anchoredPosition -= tempBackgroundsPosition;
			} else if (backgroundsRt[i].anchoredPosition.x < -backgroundSpriteSizes[i].x) {
				backgroundScrollValues[i] += backgroundSpriteSizes[i].x;
				tempBackgroundsPosition.Set(backgroundSpriteSizes[i].x, 0);
				backgroundsRt[i].anchoredPosition += tempBackgroundsPosition;
			}
		}

//���d���s�h�~�B
		if (scroll != null) {
			StopCoroutine(scroll);
		}

		scroll = StartCoroutine(Scroll());
		previousPlayerPosition = playerPosition;
	}

	IEnumerator Scroll()
	{
		scrollElapsedTime = 0;

		while (true) {
			scrollElapsedTime += Time.deltaTime;

			for (int i = 0; i < backgroundMax; i++) {
				tempBackgroundsPosition.Set(backgroundScrollValues[i], backgroundOffsets[i].y);
				backgroundsRt[i].anchoredPosition = Vector2.SmoothDamp(backgroundsRt[i].anchoredPosition, tempBackgroundsPosition, ref scrollVelocities[i], smoothTime, scrollSpeedMax);
			}

			if (scrollDuration <= scrollElapsedTime) {
//SmoothDamp��Velocity�̒l���Q�l�ɂ��Č��݂̑��x���o���ׁA���������Ă����Ȃ��Ǝ�����s���ɓ������c��B
				for (int i = 0; i < backgroundMax; i++) {
					scrollVelocities[i] = Vector2.zero;
				}
				scroll = null;
				yield break;
			}
			yield return null;
		}
	}

//�X�e�[�W�N���A���ŉ摜�ʒu�������I�Ƀ��Z�b�g���鎞�p�B
	public void Reset()
	{
		for (int i = 0; i < backgroundMax; i++) {
			backgroundScrollValues[i] = 0;
			tempBackgroundsPosition.Set(backgroundScrollValues[i], backgroundOffsets[i].y);
			backgroundsRt[i].anchoredPosition = tempBackgroundsPosition;
		}

		for (int i = 0; i < backgroundMax; i++) {
			scrollVelocities[i] = Vector2.zero;
		}

		previousPlayerPosition = Player.playerStartPos;

		if (scroll != null) {
			StopCoroutine(scroll);
			scroll = null;
		}
	}

//�e��R���|�[�l���g���A�^�b�`���A�w�i�摜���𐶐��B
	public void CreateParallaxBackground()
	{
		if (backgroundSprites == null || backgroundSprites.Length == 0)
			return;

		backgroundMax = backgroundSprites.Length;
		parallaxBackgroundCanvas = GetComponent<Canvas>();
		if (parallaxBackgroundCanvas == null)
			return;
		
		backgroundsRt = new RectTransform[backgroundMax];
		scrollVelocities = new Vector2[backgroundMax];

		backgroundScrollValues = new float[backgroundMax];
		parallaxBackgroundCanvas.renderMode = RenderMode.ScreenSpaceCamera;
		parallaxBackgroundCanvas.worldCamera = Camera.main;
		parallaxBackgroundCanvas.planeDistance = planeDistance;

//�{�^����ݒu���Ȃ��̂ŁA����Canvas�ւ̃^�b�`����𖳌������Ă���(�C���X�y�N�^�[����폜���Ă��ǂ�)�B
		GetComponent<GraphicRaycaster>().enabled = false;
		parallaxBackgroundGo = new GameObject("ParallaxBackground");
		parallaxBackgroundRt = parallaxBackgroundGo.AddComponent<RectTransform>();
		parallaxBackgroundRectMask2D = parallaxBackgroundGo.AddComponent<RectMask2D>();

		parallaxBackgroundRectMask2D.enabled = false;
		parallaxBackgroundRt.SetParent(transform);
		parallaxBackgroundRt.localScale = Vector3.one;
		parallaxBackgroundRt.localPosition = Vector3.zero;

		parallaxBackgroundRt.sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;

		for (int i = 0; i < backgroundMax; i++) {
			backgroundsRt[i] = new GameObject(System.String.Format("Backgrounds{0}", i + 1)).AddComponent<RectTransform>();

			backgroundsRt[i].SetParent(parallaxBackgroundRt);
			backgroundsRt[i].localScale = Vector3.one;
			backgroundsRt[i].localPosition = Vector3.zero;
			tempBackgroundPosition.Set(0, backgroundOffsets[i].y);
			backgroundsRt[i].anchoredPosition = tempBackgroundPosition;

			for (int j = 0; j < imageMax; j++) {
				tempBackgroundGo = new GameObject(System.String.Format("Background{0}", i + 1));
				tempBackgroundRt = tempBackgroundGo.AddComponent<RectTransform>();
				tempBackgroundImg = tempBackgroundGo.AddComponent<Image>();

				tempBackgroundImg.sprite = backgroundSprites[i];
				tempBackgroundImg.raycastTarget = false;
				tempBackgroundRt.SetParent(backgroundsRt[i]);
				tempBackgroundRt.localScale = Vector3.one;
				tempBackgroundRt.localPosition = Vector3.zero;
				tempBackgroundRt.sizeDelta = backgroundSpriteSizes[i];
				tempBackgroundPosition.Set(backgroundOffsets[i].x + backgroundSpriteSizes[i].x * j, 0);
				tempBackgroundRt.anchoredPosition = tempBackgroundPosition;
			}
		}

//�J�����ƕ��s�ɐݒu�������ꍇ�ɂ́AlocalRotation�����Z�b�g���Ă����B
		parallaxBackgroundRt.localRotation = Quaternion.identity;
		isInitialized = true;
	}
}