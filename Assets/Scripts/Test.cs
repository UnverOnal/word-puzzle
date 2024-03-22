using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LevelCreation;
using Services.FileConversionService;
using Services.InputService;
using Services.PoolingService;
using Services.SceneService;
using UnityEngine;
using VContainer;

public class Test : MonoBehaviour
{
    [Inject] private ISceneService _sceneService;
    [Inject] private IInputService _inputService;
    [Inject] private IPoolService _poolService;
    [Inject] private LevelPresenter _levelPresenter;
    private ObjectPool<GameObject> _pool;
    private GameObject _poolParent;

    private void Start()
    {
        
        _levelPresenter.CreateLevel();
        // var sprite = GetComponent<SpriteRenderer>().sprite;
        // Debug.Log(sprite.bounds.size * 12.5f);
        //
        // var nextTileDistance = sprite.bounds.size.x * transform.localScale.x;
        // var newOne = Instantiate(gameObject);
        // Destroy(newOne.GetComponent<Test>());
        // newOne.transform.position += transform.position + Vector3.right * nextTileDistance;


    }

    private async void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            var createdGo = _pool.Get();
            createdGo.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(3));
            createdGo.SetActive(false);
            createdGo.transform.SetParent(_poolParent.transform);
            _pool.Return(createdGo);
        }

        // MoveSides(_inputService.GetDragInput(10f).x, Vector3.right, 5f, 10f, transform.parent);
        // Debug.Log(_inputService.GetSwipe(50f));
        //
        // if(Input.GetKeyDown(KeyCode.I))
        //     _inputService.IgnoreInput(true);
        // if (Input.GetKeyDown(KeyCode.N))
        //     _inputService.IgnoreInput(false);
    }

    // public void MoveSides(float input ,Vector3 axis, float moveFactor, float border, Transform platform, float smoothness = 3f)
    // {
    //     Vector3 targetPosition = transform.position + axis * input * moveFactor;
    //     targetPosition.x = Mathf.Clamp(targetPosition.x, platform.position.x - border, platform.position.x + border);
    //
    //     MoveTo(targetPosition, smoothness);
    // }
    //
    // public void MoveTo(Vector3 targetPosition, float smoothness)
    // {
    //     Vector3 currentPosition = transform.position;
    //
    //     Vector3 smoothedPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * smoothness);
    //     transform.position = smoothedPosition;
    // }
}