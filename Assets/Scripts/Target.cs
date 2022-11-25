using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Target : MonoBehaviour
{
    public int value;
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;


    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), 0, ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOnDown();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(12, 16);
    }

    float RandomTorque()
    {
        return Random.Range(-10, 10);
    }

    Vector3 RandomSpawnPos() {
        return new Vector3(Random.Range(-4, 4), -6);
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
             Destroy(gameObject);
            gameManager.UpdateScore(value);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            if(gameObject.CompareTag("Bomb")){
                gameManager.UpdateLife();
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        gameManager.UpdateScore(value);
    }

    private void DestroyOnDown(){
        if(gameObject.transform.position.y < -6){
            Destroy(gameObject);
            if(!gameObject.CompareTag("Bomb")){
                gameManager.UpdateLife();
            }
        }
    }
}
