using System;
using System.Collections.Generic;
using Dictionary;
using LevelCreation;
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
    [Inject] private WordDictionary _wordDictionary;
    private ObjectPool<GameObject> _pool;
    private GameObject _poolParent;

    private void Start()
    {
        _levelPresenter.CreateLevel();

        char[] inputChars = { 'a', 'b', 'b' };
        var permutations = GenerateCombinations(inputChars);

        // Print the generated combinations
        foreach (var combination in permutations)
            Debug.Log(combination + "," + _wordDictionary.ContainsWord(combination));
    }
    
    private List<string> GenerateCombinations(char[] chars)
    {
        List<string> result = new List<string>();
        GenerateCombinations(chars, "",new List<int>(), result);
        return result;
    }

    private void GenerateCombinations(char[] chars, string current, List<int> currentIndices, List<string> result)
    {
        switch (current.Length)
        {
            case > 7:
                return;
            case > 1:
                if(!result.Contains(current))
                    result.Add(current);
                break;
        }

        for (int i = 0; i < chars.Length; i++)
        {
            if (!currentIndices.Contains(i))
            {
                var next = new List<int>(currentIndices) { i };
                GenerateCombinations(chars, current + chars[i], next, result);
            }
        }
    }
}