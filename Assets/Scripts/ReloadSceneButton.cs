using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadSceneButton : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen; // Loading screen prefab

   // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(LoadSceneCoroutine()); });
    }

    /// <summary>
    /// Loads a scene asynchronously and display loading screen
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneCoroutine()
    {
        // Making the loading screen appear
        var loadingScreenInstance = Instantiate(loadingScreen);
        // Making the loading screen persistent after we unloaded the scene
        DontDestroyOnLoad(loadingScreenInstance);
        // Getting the loading screen animator
        var loadingAnimator = loadingScreenInstance.GetComponent<Animator>();
        // Getting current animator state to retrieve the length in seconds of the actual animation
        var currentAnimTime = loadingAnimator.GetCurrentAnimatorStateInfo(0).length;
        var sceneToLoadName = SceneManager.GetActiveScene().name;
        // Start loading the scene in the background
        var loading = SceneManager.LoadSceneAsync(sceneToLoadName);
        // Disable auto-load 
        loading.allowSceneActivation = false;
        // While the scene is still loading
        while (!loading.isDone)
        {
            // If the scene loaded at 90% (which means 100% in Unity)
            if (loading.progress >= 0.9f)
            {
                // Launch the disappear animation
                loadingAnimator.SetTrigger("Disappear");
                // Make the new scene visible
                loading.allowSceneActivation = true;
            }

            // Wait for the end of the appearing animation before switching scenes
            yield return new WaitForSeconds(currentAnimTime);
        }
    }
}