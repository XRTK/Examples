// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        var operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        operation.completed += Operation_completed;
    }

    private void Operation_completed(AsyncOperation operation)
    {
        operation.completed -= Operation_completed;
        Destroy(gameObject);
    }
}
