using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    [Header("Shield")]
    public bool isShieldUp;
    [SerializeField] private GameObject shield;

    private PlayerRoot player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerRoot>();
        ResetPowers();
    }

    public void ResetPowers()
    {
        isShieldUp = false;
        shield.SetActive(false);
    }

    public void Shield()
    {
        isShieldUp = !isShieldUp;
        shield.SetActive(!shield.activeSelf);        
    }

    private void CoinMultiplier()
    {

    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            Shield();
            //Tocar som de escudo subindo
            Destroy(other.gameObject);
        }

        if (other.CompareTag("StaminaPotion"))
        {
            player.UpdateStamina(10f); //Provavelmente irei permitir que este item seja upado
                                      //Terei que atualizar esse método depois por isso
            //Tocar som de stamina recuperando
            Destroy(other.gameObject);
        }

        if (other.CompareTag("CoinMultiplier"))
        {
            CoinMultiplier();
            //Ativar multiplicador
        }


    }

}
