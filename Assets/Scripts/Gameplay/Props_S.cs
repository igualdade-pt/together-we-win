using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props_S : MonoBehaviour
{

    private GameObject otherGameObject;

    private List<GameObject> others = new List<GameObject>(2);

    private void Update()
    {
        /*if (otherGameObject)
        {
            int indexLayer = otherGameObject.GetComponentInParent<Renderer>().sortingOrder;
            gameObject.GetComponent<Renderer>().sortingOrder = indexLayer + 1;
        }*/

        if (others.Count > 0)
        {
            switch (others.Count)
            {
                case 1:
                    int indexLayer = others[0].GetComponentInParent<Renderer>().sortingOrder;
                    gameObject.GetComponent<Renderer>().sortingOrder = indexLayer + 1;
                    break;
                case 2:
                    int maxIndexLayer2 = Mathf.Max(others[0].GetComponentInParent<Renderer>().sortingOrder, others[1].GetComponentInParent<Renderer>().sortingOrder);
                    gameObject.GetComponent<Renderer>().sortingOrder = maxIndexLayer2 + 1;
                    break;
                case 3:
                    int maxIndexLayer3 = Mathf.Max(others[0].GetComponentInParent<Renderer>().sortingOrder, others[1].GetComponentInParent<Renderer>().sortingOrder, others[2].GetComponentInParent<Renderer>().sortingOrder);
                    gameObject.GetComponent<Renderer>().sortingOrder = maxIndexLayer3 + 1;
                    break;
                default:
                    break;
            }        
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.tag == "PlayerFeet" || other.tag == "EnemyCollider")
        {
            others.Add(other.gameObject);
            //otherGameObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerFeet" || other.tag == "EnemyCollider")
        {
            /*otherGameObject = null;
            int indexLayer = other.gameObject.GetComponentInParent<Renderer>().sortingOrder;
            gameObject.GetComponent<Renderer>().sortingOrder = indexLayer - 1;*/
            if (others.Count <= 1)
            {
                int indexLayer = others[0].GetComponentInParent<Renderer>().sortingOrder;
                gameObject.GetComponent<Renderer>().sortingOrder = indexLayer - 1;
            }
            others.Remove(other.gameObject);
        }
    }
}
