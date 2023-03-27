using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 

public class GameManagerCS : MonoBehaviour
{
    public static int pontuation = 0 ; 
    [SerializeField] GameObject gameOverScreen; 
    [SerializeField] float interval; 
    [SerializeField] float verticalLimit; 
    [SerializeField] int quantVidas = 3; 
    [SerializeField] GameObject playerPrefab; 
    [SerializeField] GameObject plataformPrefab;
    [SerializeField] Vector3 startPosition; 
    [SerializeField] Text pontuationText; 
    [SerializeField] Gradient skyColors; 
    [SerializeField] Camera mainCamera; 

    public static GameObject Player; 
    float timer; 

    void reduzVida()
    {
        //Destruindo todas as plataformas
        for(int i = this.gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(this.gameObject.transform.GetChild(i).gameObject);
        }

        //Destruir o jogador, instanciar ele novamente
        Destroy(Player);
        Player = null; 
        Instantiate(playerPrefab, startPosition, Quaternion.identity);
        Instantiate(plataformPrefab, startPosition - new Vector3(0,2,0), Quaternion.identity, this.transform);

        //Reiniciar velocidade do jogo
        Time.timeScale = 1; 

        //Reduzir o número de vidas
        quantVidas--; 
    }
    void GameOver()
    {
        //Abrir tela de gameOver
        //Destruir o jogador
        //Parar as plataformas
        for(int i = this.gameObject.transform.childCount - 1; i >= 0; i--)
        {
            PlataformaCS.velocity = 0; 
        }
        
        gameOverScreen.SetActive(true);
        pontuationText.transform.SetParent(gameOverScreen.transform);
    }

    void Start()
    {
        PlataformaCS.velocity = -2;
        timer = interval; 
        pontuation = 0; 
    }

    public static void setPlayer(GameObject player)
    {
        Player = player;
    }
    
    public static GameObject getPlayer()
    {
        return Player; 
    }

    public static float getPlayerPos()
    {
        return Player.transform.position.y; 
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime; 
        if(timer < 0)
        {
            timer = interval;
            Time.timeScale += 0.1f; 
        } 

        if(Player.transform.position.y < verticalLimit)
        {
            if(quantVidas > 0)
            {
                reduzVida();
            }
            else
                GameOver();
        }
        if(Time.timeScale - 1 < 1)
        {
            mainCamera.backgroundColor = Color.Lerp(skyColors.Evaluate(Time.timeScale -1.1f), skyColors.Evaluate(Time.timeScale -1f), interval - timer);
        }
        pontuationText.text = pontuation.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
