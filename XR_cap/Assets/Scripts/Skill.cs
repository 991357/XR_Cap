using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public string Type;
    public float Dmg;
    float x;
    float y;
    SpriteRenderer SR;

    bool IsLeft;

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        switch (Type)
        {
            case "LevelUP":
                float PosX = GameManager.Instance.Player.transform.position.x;
                float PosY = GameManager.Instance.Player.transform.position.y;
                transform.position = new Vector2(PosX,PosY);
                break;
            case "Wind_0":
                float Wind_0PosX = GameManager.Instance.Player.transform.position.x;
                float Wind_0PosY = GameManager.Instance.Player.transform.position.y;
                transform.position = new Vector2(Wind_0PosX, Wind_0PosY);
                if (GameManager.Instance.PlayerLogic.SR.flipX)
                {
                    SR.flipX = true;
                    IsLeft = true;
                }
                else
                {
                    //오른쪽을 보고 있음
                    SR.flipX = false;
                    IsLeft = false;
                }
                break;
            case "Wind_1":
                float Wind_1PosX = GameManager.Instance.Player.transform.position.x;
                float Wind_1PosY = GameManager.Instance.Player.transform.position.y;
                transform.position = new Vector2(Wind_1PosX, Wind_1PosY);

                x = Random.Range(-8, 8);
                y = Random.Range(-4, 4);
                transform.position = new Vector3(transform.position.x + x, transform.position.y + y);
                break;
            case "Wind_2":
                float Wind_2PosX = GameManager.Instance.Player.transform.position.x;
                float Wind_2PosY = GameManager.Instance.Player.transform.position.y;
                transform.position = new Vector2(Wind_2PosX, Wind_2PosY);
                transform.rotation = Quaternion.identity;
                for (int i = 0; i < 12; i++)
                {
                    GameObject tornado = GameManager.Instance.P_Manager.Get(12);
                    tornado.transform.position = transform.position;
                    tornado.transform.rotation = Quaternion.identity;

                    Rigidbody2D rigid = tornado.GetComponent<Rigidbody2D>();
                    Vector2 dirvec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 12), Mathf.Sin(Mathf.PI * 2 * i / 12));
                    rigid.AddForce(dirvec.normalized * 8, ForceMode2D.Impulse);

                    //Vector3 rotvec = Vector3.forward * 360 * i / 8 + Vector3.forward * 180;
                    //bullet.transform.Rotate(rotvec);
                }
                break;
            case "Wind_3":
                float Wind_3PosX = GameManager.Instance.Player.transform.position.x;
                float Wind_3PosY = GameManager.Instance.Player.transform.position.y;
                transform.position = new Vector2(Wind_3PosX, Wind_3PosY);
                transform.rotation = Quaternion.identity;
                GetComponent<Bullet>().I_Per = 999;
                break;
            case "Fire_0":
                if (GameManager.Instance.PlayerLogic.SR.flipX)
                {
                    float Fire_0PosX = GameManager.Instance.Player.transform.position.x - 1f;
                    float Fire_0PosY = GameManager.Instance.Player.transform.position.y;
                    transform.position = new Vector2(Fire_0PosX, Fire_0PosY);
                    transform.rotation = Quaternion.identity;
                }
                else
                {
                    float Fire_0PosX = GameManager.Instance.Player.transform.position.x + 1f;
                    float Fire_0PosY = GameManager.Instance.Player.transform.position.y;
                    transform.position = new Vector2(Fire_0PosX, Fire_0PosY);
                    transform.rotation = Quaternion.identity;
                }
                break;
            case "Fire_1":
                float Fire_1PosX = GameManager.Instance.Player.transform.position.x;
                float Fire_1PosY = GameManager.Instance.Player.transform.position.y;
                transform.position = new Vector2(Fire_1PosX, Fire_1PosY);
                transform.rotation = Quaternion.identity;
                //for (int i = 0; i < 4; i++)
                //{
                //    GetComponentsInChildren<Bullet>()[i].I_Per = 999;
                //}
                break;
            case "Fire_2":
                GetComponent<Bullet>().I_Per = 999;
                float Fire_2PosX = GameManager.Instance.Player.transform.position.x;
                float Fire_2PosY = GameManager.Instance.Player.transform.position.y;
                transform.position = new Vector2(Fire_2PosX, Fire_2PosY);

                x = Random.Range(-20, 20);
                y = Random.Range(1, 10);
                transform.position = new Vector3(transform.position.x + x, transform.position.y + y);

                if (gameObject.activeInHierarchy)
                {
                    StartCoroutine(Meteo());
                }
                break;
            case "Cloud":
                Vector2 cloudpos = GameManager.Instance.Player.transform.position;
                transform.position = cloudpos;

                x = Random.Range(-8, 8);
                y = Random.Range(-4, 4);
                transform.position = new Vector3(transform.position.x + x, transform.position.y + y);
                StartCoroutine(Thunder(gameObject));
                break;
            case "Acid":
                Vector2 acidpos = GameManager.Instance.Player.transform.position;
                transform.position = acidpos;

                x = Random.Range(-4, 4);
                y = Random.Range(-2, 2);
                transform.position = new Vector3(transform.position.x + x, transform.position.y + y);

                StartCoroutine(AcidDelete());
                break;
            case "Meteo":
                GetComponent<Bullet>().I_Per = 999;
                Vector2 meteopos = GameManager.Instance.Player.transform.position;
                transform.position = meteopos;

                x = Random.Range(-20, 20);
                y = Random.Range(1, 10);
                transform.position = new Vector3(transform.position.x + x, transform.position.y + y);
                break;
            case "Sword":
                Vector2 swordpos = GameManager.Instance.Player.transform.position;
                transform.position = swordpos;

                x = Random.Range(-2.5f,2.5f );
                y = Random.Range(0.5f, 1.5f);
                transform.position = new Vector3(transform.position.x + x, transform.position.y + y);
                break;
        }
    }

    private void Update()
    {
        switch(Type)
        {
            case "Wind_0":
                if(IsLeft)
                {
                    Rigidbody2D rigid = GetComponent<Rigidbody2D>();
                    rigid.AddForce(Vector2.left * 1.5f, ForceMode2D.Impulse);
                    Invoke("TurnOff", 1.5f);
                }
                else
                {
                    Rigidbody2D rigid = GetComponent<Rigidbody2D>();
                    rigid.AddForce(Vector2.right * 1.5f, ForceMode2D.Impulse);
                    Invoke("TurnOff", 1.5f);
                }
                break;
            case "Wind_1":                
                Invoke("TurnOff", 1f);
                break;
            case "Wind_2":
                Invoke("TurnOff", 0.1f);
                break;
            case "Wind_3":
                Invoke("TurnOff", 1.3f);
                break;
            case "Fire_0":
                Invoke("TurnOff", 1.5f);
                break;
            case "Fire_1":
                float Fire_1PosX = GameManager.Instance.Player.transform.position.x + 2.5f;
                float Fire_1PosY = GameManager.Instance.Player.transform.position.y;
                transform.position = new Vector2(Fire_1PosX, Fire_1PosY);
                Invoke("TurnOff", 2f);
                break;
            case "Fire_2":
                if(!GameManager.Instance.LevelUp.IsLevelUp)
                    transform.position = new Vector2(transform.position.x + 0.001f, transform.position.y - 0.005f);
                GameManager.Instance.PlayerLogic.CamShake.IsShake = true;
                Invoke("TurnOff", 2.5f);
                break;
            case "Meteo":
                if (!GameManager.Instance.LevelUp.IsLevelUp)
                    transform.position = new Vector2(transform.position.x + 0.001f, transform.position.y - 0.005f);
                GameManager.Instance.PlayerLogic.CamShake.IsShake = true;
                Invoke("TurnOff", 2.5f);
                break;
            case "Sword":
                transform.position = new Vector2(0, transform.position.y - 0.01f);
                //GameManager.Instance.PlayerLogic.CamShake.IsShake = true;
                Invoke("TurnOff", 2f);
                break;
        }
    }
    
    void TurnOff()
    {
        gameObject.SetActive(false);
        GameManager.Instance.PlayerLogic.CamShake.IsShake = false;
    }

    IEnumerator Thunder(GameObject cloud)
    {
        yield return new WaitForSeconds(2f);
        GameObject thunder = GameManager.Instance.P_Manager.Get(19);
        thunder.transform.position = new Vector2(transform.position.x, transform.position.y - 2f);

        yield return new WaitForSeconds(1.7f);
        cloud.SetActive(false);
        thunder.SetActive(false);
    }

    IEnumerator AcidDelete()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    IEnumerator Meteo()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.15f);
            GameManager.Instance.P_Manager.Get(21);
        }
    }
}
