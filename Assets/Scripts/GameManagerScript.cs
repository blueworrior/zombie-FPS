using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseMenuUI; // Assign your pause menu UI canvas in Inspector

    public GameObject playerController; // Assign the FirstPersonController GameObject
    public MonoBehaviour gunMechanicScript; // Assign your shooting script here

    private bool isPaused = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false); // Make sure pause menu starts hidden
    }

    void Update()
    {
        // Escape key toggles pause menu (only if Game Over isn't active)
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverUI.activeInHierarchy)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        // Cursor management
        if (gameOverUI.activeInHierarchy || isPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playerController.GetComponent<StarterAssets.FirstPersonController>().enabled = false;
        gunMechanicScript.enabled = false;

        Time.timeScale = 0f; // Optional: Freeze game when over
    }

    public void respawn()
    {
        Time.timeScale = 1f; // Reset time if you froze it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Respawn");
    }

    public void mainMenu()
    {
        Time.timeScale = 1f; // Reset time if paused
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Main Menu");
    }

    public void quit()
    {
        #if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
                Debug.Log("Quit");
			#endif
        
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);

        playerController.GetComponent<StarterAssets.FirstPersonController>().enabled = false;
        gunMechanicScript.enabled = false;

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);

        playerController.GetComponent<StarterAssets.FirstPersonController>().enabled = true;
        gunMechanicScript.enabled = true;

        Time.timeScale = 1f;
    }
}
