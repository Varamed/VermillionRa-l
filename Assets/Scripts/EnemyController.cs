using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Dos variables que determinan la cantidad de vida y mana del enemigo siendo modificables
    [SerializeField] float vida;
    [SerializeField] float mana;

    //Variable que determina el valor maximo de mana que tiene el enemigo
    [SerializeField] float manaMaximo;

    //Variables que determinan la velocidad a la que recupera el mana y
    //cuanto tiempo hay de cooldown para que empiece a recuperar
    [SerializeField] float velocidadRecuperacion; 
    [SerializeField] float tiempoEsperaRecuperacionMana;

    private DiceBehaviour diceBehaviour;

    //Nos permite modificar el listado de ataques
    [System.Serializable]
    public struct Ataque
    {
        //Se declaran las variables que serán almacenadas en el array
        public string nombreAtaque;
        public int cantidadDaño;
        public int costeMana;
        public int tipoDeDado;
    }
    //creacion de la lista donde se guardan los ataques indicando que su longitud maxima es 3
    public Ataque[] ataques = new Ataque[3];

    //Se crean esos ataques desde codigo aunque se pueden añadir y modificar desde el inspector
    private void Start()
    {
        diceBehaviour = FindAnyObjectByType<DiceBehaviour>();


        ataques[0] = new Ataque { nombreAtaque = "Ataque1", cantidadDaño = 0, costeMana = 0, tipoDeDado = 0 };
        ataques[1] = new Ataque { nombreAtaque = "Ataque2", cantidadDaño = 0, costeMana = 0, tipoDeDado = 0 };
        ataques[2] = new Ataque { nombreAtaque = "Ataque3", cantidadDaño = 0, costeMana = 0, tipoDeDado = 0 };
    }
    public void Atacar(int indiceAtaque)
    {
        //Seguridad de que si se llama a un espacio del array que no este comprendido
        //entre 0 y 2 nos devuelva un error y no se quede congelado
        //por intentar acceder a un espacio fuera del array
        if (indiceAtaque < 0 || indiceAtaque >= ataques.Length)
        {
            return;
        }

        Ataque ataque = ataques[indiceAtaque];

        //Comprobacion de que el enemigo tenga la cantidad necesaria de mana para hacer el ataque,
        //en ese caso se resta esa cantidad de mana al total del enemigo
        if (mana >= ataque.costeMana)
        {
            mana -= ataque.costeMana;
            
            //Aqui habria que llamar a un metodo o enviar esa cantidad de daño que hace el enemigo

            switch (ataque.tipoDeDado)
            {
                case 4:
                    diceBehaviour.Dado4();
                    break;

                case 6:
                    diceBehaviour.Dado6();
                    break;

                case 8:
                    diceBehaviour.Dado8();
                    break;

                case 10:
                    diceBehaviour.Dado10();
                    break;

                case 12:
                    diceBehaviour.Dado12();
                    break;

                case 20:
                    diceBehaviour.Dado20();
                    break;

                default:
                    Debug.Log("Error");
                    break;
            }


            //Comienza la corrutina donde se recupera el mana tras unos segundos
            StartCoroutine(RecuperacionMana());
        }
        else
        {
            //No se realiza el ataque por falta de mana
        }
    }

    //Metodo donde se recibe el daño causado por los personajes controlados por el player
    //donde int cantidad es el daño que haga cada ataque
    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }
    //Corrutina que aumenta el mana con el tiempo
    private IEnumerator RecuperacionMana()
    {
        yield return new WaitForSeconds(tiempoEsperaRecuperacionMana);

        //Condicional que añade mana solamente si no tiene el tope
        if (mana < manaMaximo)
        {
            mana += velocidadRecuperacion * Time.deltaTime;
            //Nos aseguramos de que no supere nunca el maximo de mana al sumarle
            mana = Mathf.Min(mana, manaMaximo);
        }
    }

}
