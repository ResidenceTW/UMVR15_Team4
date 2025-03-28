using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance {get; private set;}

	[SerializeField] private AudioLibrarySO _audioLibrarySO;
	[Range(0, 1)][SerializeField] private float _mainVolume;
	[Range(0, 1)][SerializeField] private float _bgmVolume;
	[Range(0, 1)][SerializeField] private float _sfxVolume;

	[SerializeField] private int _audioPoolSize;
	private Queue<AudioSource> _audioPool = new Queue<AudioSource>();
	private Dictionary<string, Dictionary<GameObject,List<AudioSource>>> _nowPlayAudio = new Dictionary<string, Dictionary<GameObject, List<AudioSource>>>();
	private AudioSource _bgmAudioSource;

	private void Awake()
	{
		// 單例模式
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

        Instance = this;
		DontDestroyOnLoad(gameObject);

        _bgmAudioSource = gameObject.AddComponent<AudioSource>();

		// 預先做好池子
		for(int i = 0; i < _audioPoolSize; i++)
		{
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.playOnAwake = false;
			_audioPool.Enqueue(audioSource);
		}
		
		gameObject.AddComponent<AudioListener>();
	}

	private void Start()
	{
		_mainVolume = GameDataManager.Instance.gameData.mainVolume;
		_bgmVolume = GameDataManager.Instance.gameData.bgmVolume;
		_sfxVolume = GameDataManager.Instance.gameData.sfxVolume;

        if (_bgmAudioSource != null) PlayBGM("BackGroundMusic");
	}

	private void OnValidate()
	{
		if(_bgmAudioSource != null)
		{
			_bgmAudioSource.volume = _mainVolume * _bgmVolume;
		}
		
		// if(GameDataManager.Instance != null)
		// {
		//     GameDataManager.Instance.gameData.mainVolume = _mainVolume;
		// 	GameDataManager.Instance.gameData.bgmVolume = _bgmVolume;
		// 	GameDataManager.Instance.gameData.sfxVolume = _sfxVolume;
			
		// 	GameDataManager.Instance.SaveGame();

		// 	Debug.Log("Is this issue");
		// }
	}

	public void PlayBGM(string key)
	{
		AudioClip audioClip = _audioLibrarySO.GetAudioClip(key);

		if(audioClip == null)
		{
			Debug.Log("AudioManager PlaySound 輸入的 Key 有錯");
			return;
		}
		
		if(_bgmAudioSource.clip == audioClip) return;

		_bgmAudioSource.clip = audioClip;
		_bgmAudioSource.loop = true;
		_bgmAudioSource.volume = _mainVolume * _bgmVolume;
		_bgmAudioSource.Play();
	}
	
	private IEnumerator CrossFadeBGM()
	{
	    yield return null;
	}

	public void PlaySound(string key, Vector3? position = default, GameObject caller = null, bool isLoop = false, float playTimer = 0f)
	{
		// 檢查輸入的 key 是否正確
		AudioClip audioClip = _audioLibrarySO.GetAudioClip(key);
		if(audioClip == null)
		{
			Debug.Log("AudioManager PlaySound 輸入的 Key 有錯, key 為" + key);
			return;
		}

		// 如果物件池數量不夠了, 就在多新增 AudioSource
		AudioSource audioSource = _audioPool.Count > 0 ? _audioPool.Dequeue() : gameObject.AddComponent<AudioSource>();
		if(_audioPool.Count == 0) audioSource.playOnAwake = false;

		// 設置 AudioSource 相關參數並播放
		audioSource.transform.position = position ?? caller.transform.position;
		audioSource.clip = audioClip;
		audioSource.volume = _mainVolume * _sfxVolume;
		audioSource.loop = isLoop;
		audioSource.Play();

		// 將新播放的 AudioSource 放入 Dictionary 方便追蹤
		if(!_nowPlayAudio.ContainsKey(key)) _nowPlayAudio[key] = new Dictionary<GameObject, List<AudioSource>>();
		if(caller == null) caller = this.gameObject;
		if(!_nowPlayAudio[key].ContainsKey(caller)) _nowPlayAudio[key][caller] = new List<AudioSource>();
		_nowPlayAudio[key][caller].Add(audioSource);

		float duration = playTimer == 0f ? audioClip.length : playTimer;
		StartCoroutine(RecycleAudioToPool(key, caller, audioSource, duration));
		
	}

	/// <summary>
	/// 停止指定的音效播放
	/// </summary>
	/// <param name="key"></param>
	public void StopSound(string key, GameObject caller)
	{
		if (!_nowPlayAudio.ContainsKey(key)) return;

		if (caller == null)
		{
			// 停止所有播放該 key 的音效
			foreach (var pair in _nowPlayAudio[key])
			{
				foreach (AudioSource audioSource in pair.Value)
				{
					audioSource.Stop();
					audioSource.clip = null;
					audioSource.loop = false;
					_audioPool.Enqueue(audioSource);
				}
			}
			_nowPlayAudio.Remove(key);
		}
		else
		{
			// 只停止 caller 的音效
			if (_nowPlayAudio[key].ContainsKey(caller))
			{
				foreach (AudioSource audioSource in _nowPlayAudio[key][caller])
				{
					audioSource.Stop();
					audioSource.clip = null;
					audioSource.loop = false;
					_audioPool.Enqueue(audioSource);
				}
				_nowPlayAudio[key].Remove(caller);
			}
		}
	}

	// 將 AudioSource 播完後, 回收進物件池中
	private IEnumerator RecycleAudioToPool(string key, GameObject caller, AudioSource audioSource, float audioTime)
	{
		yield return new WaitForSeconds(audioTime);

		if(_nowPlayAudio.ContainsKey(key) && _nowPlayAudio[key].ContainsKey(caller))
		{
			_nowPlayAudio[key][caller].Remove(audioSource);

			if (_nowPlayAudio[key][caller].Count == 0) _nowPlayAudio[key].Remove(caller);
			if (_nowPlayAudio[key].Count == 0) _nowPlayAudio.Remove(key);
		}

		audioSource.Stop();
		audioSource.clip = null;
		audioSource.loop = false;
		_audioPool.Enqueue(audioSource);
	}

	public void SetMainVolume(float mainVolume)
	{
		_mainVolume = mainVolume;
		_bgmAudioSource.volume = mainVolume * _bgmVolume;

		GameDataManager.Instance.gameData.mainVolume = mainVolume;
	}

	public void SetBGMVolume(float bgmVolume)
	{
		_bgmVolume = bgmVolume;
		_bgmAudioSource.volume = _mainVolume * bgmVolume;

		GameDataManager.Instance.gameData.bgmVolume = bgmVolume;
	}

	public void SetSFXVolume(float sfxVolume)
	{
		_sfxVolume = sfxVolume;

		GameDataManager.Instance.gameData.sfxVolume = sfxVolume;
	}
	
}
