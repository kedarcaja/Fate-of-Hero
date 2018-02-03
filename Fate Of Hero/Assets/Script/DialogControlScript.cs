using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class DialogControlScript : MonoBehaviour
{
    //public dropdown
    public enum DialogList
    { None, D_MS01_02, D_MS01_03, D_MS02_01 }
    [Header("Výběr dialogu")]
    [Tooltip("Z menu mužeme vybrat o jaký dialog se jedná")]
    public DialogList VybranýDialog = DialogList.None;
    [Header("Objekty")]
    public GameObject dialogControler;
    public GameObject DialogObject;
    [Header("Volba")]
    public Text DialogText;
    private bool choice;
    private int DialogValue, Part, Step;
    private float timer, waitTime;
    public AudioClip Voice;
    bool IsPlay;
    bool Trigger;

    private void Start()
    {
      
        if (VybranýDialog == DialogList.None) { Debug.LogError("<color=Red><b>ERROR: </b> Zapoměl jsi vybrat o jaký dialog se jedná </color>"); }
    }

    void Update()
    {
        if (timer >= waitTime)
            {
                Part = Step; Dialog();

            }
        else { timer += UnityEngine.Time.deltaTime; }
        if (Trigger)
        {
            if (Input.GetKeyDown(KeyCode.E) && IsPlay==false)
            {
                IsPlay = true;
                dialogControler.SetActive(true);
                Part = 1; Dialog();
                AudioSource.PlayClipAtPoint(Voice, transform.position, 1f);
                PlayerScript.Instance.isMove = false;
            }
        }
    }

    public void Dialog()
    {
        if (VybranýDialog == DialogList.D_MS01_02)
        {

            if (Part == 1)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> To jako fakt ?";
                waitTime = 2f;
                Step = 2;
            }
            else if (Part == 2)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> Kytky přece nepláčou, kytky tiše rostou.";
                waitTime = 4.39f;
                Step = 3;
            }
            else if (Part == 3)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> No tak jó, tak jó, ale budu si připadat jako blázen.";
                waitTime = 4.64f;
                Step = 4;
            }
            else if (Part == 4)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> Květino ? ";
                waitTime = 4f;
                Step = 5;
            }
            else if (Part == 5)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Květina: </b></color> Co chceš ?";
                waitTime = 2f;
                Step = 6;
            }
            else if (Part == 6)
            {
                DialogText.text = "<color=yellow><b>Květina: </b></color> Jo, já vím! Chceš mi vzít další okvětní lístek.";
                waitTime = 6f;
                Step = 7;
            }
            else if (Part == 7)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Květina: </b></color> Tak víš co ? Tak já ti ho teda dám dobrovolně, nemusíš mi ho škubat jako posledně.";
                waitTime = 7f;
                Step = 8;
            }
            else if (Part == 8)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Květina: </b></color>  To vážně bolelo !";
                waitTime = 2f;
                Step = 9;
            }
            else if (Part == 9)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> Počkej počkej! Nechci tvoje lístky, hledám cestu ven.";
                waitTime = 5f;
                Step = 10;
            }
            else if (Part == 10)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> Ty  jsi opravdu mluvící kytka ?";
                waitTime = 4f;
                Step = 11;
            }
            else if (Part == 11)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color>To jako fakt?";
                waitTime = 2f;
                Step = 12;
            }
            else if (Part == 12)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color>Nepraštil jsem se při tom pádu moc do hlavy?";
                waitTime = 2f;
                Step = 13;
            }
            else if (Part == 13)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Květina: </b></color> No a co? Umím mluvit, naučila jsem se to, když jsem byla malá";
                waitTime = 6f;
                Step = 14;
            }
            else if (Part == 14)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Květina: </b></color> Jako každý.";
                waitTime = 2f;
                Step = 15;
            }
            else if (Part == 15)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Květina: </b></color>To pocházíš z divného kraje, když se to u vás děti neučí.";
                waitTime = 4f;
                Step = 16;
            }
            else if (Part == 16)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color>No já vlastně asi vůbec nejsem odsud.";
                waitTime = 2f;
                Step = 17;
            }
            else if (Part == 17)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color>A kde to podle tebe teda jsme?";
                waitTime = 2f;
                Step = 18;
            }
            else if (Part == 18)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Květina: </b></color>Nacházíš se na Antorském ostrově přímo v srdci Gortarianského impéria.";
                waitTime = 8f;
                Step = 19;
            }
            else if (Part == 19)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color>To víš že jó,";
                waitTime = 2f;
                Step = 20;

            }
            else if (Part == 20)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Květina: </b></color>Ale Abych pravdu řekla, na tomhle místě není nic kouzelného. Mám tu jen samé trápení.";
                waitTime = 8f;
                Step = 21;
            }
            else if (Part == 21)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color>Víš co kytko? Zkusím ti najít ten tvůj vyškubnutý lístek, ať je ti líp.";
                waitTime = 6f;
                Step = 22;
            }
            else if (Part == 22)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color>Ani mě by se tu nechtělo tiše a poklidně růst jako každá jiná normální květina!";
                waitTime = 9f;
                Step = 23;
            }
            else if (Part == 23)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color>Třeba mě mezitím to šálení přejde.";
                waitTime = 4f;
                Step = 24;

            }
            else if (Part == 24)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Květina: </b></color>A neříkej mi kytko, uráží mě to! Jmenuji se Samantha.";
                waitTime = 6f;
                Step = 25;
            }
            else if (Part == 25)
            {
                timer = 0;
                dialogControler.SetActive(false);
                PlayerScript.Instance.isMove = true;
                Destroy(DialogObject);
            }
        }
        if (VybranýDialog == DialogList.D_MS01_03)
        {
            if (Part == 1)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> Kvě… SSS… jo Samatho, donesl jsem ti ten tvůj lístek.";
                waitTime = 7f;
                Step = 2;
            }
            if (Part == 2)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Samantha: </b></color> Jé, děkuji, tady máš za něj klíč k mříži od stoky!";
                waitTime = 5f;
                Step = 3;
            }
            if (Part == 3)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Samantha: </b></color> Až mi přiroste zpátky, tak budu zase ta nejkrásnější květina široko daleko. ";
                waitTime = 6f;
                Step = 4 ;
            }
            if (Part == 4)
            {
                timer = 0;
                DialogText.text = "<color=grey><b>Myška: </b></color> Ty? Ha ha ha vždyť i žížaly jsou krásnější, než ty.";
                waitTime = 6f;
                Step = 5;
            }
            if (Part == 5)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Samantha: </b></color> A proč je tady tahle ?";
                waitTime = 2f;
                Step = 6;
            }
            if (Part == 6)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> E...";
                waitTime = 1f;
                Step = 7;
            }
            if (Part == 7)
            {
                timer = 0;
                DialogText.text = "<color=grey><b>Myška: </b></color> Jsem tu, abych ti řekla, že si jdu nahoru sehnat nový klobouk, který ty nikdy mít nebudeš, dřív tady uhniješ!";
                waitTime = 12.2f;
                Step = 8;
            }
            if (Part == 8)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> Ale… holky…nechte toho. Myška se ti přišla omluvit, Samantho.";
                waitTime = 7.8f;
                Step = 9;
            }
            if (Part == 9)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> Dělej se omluv, sám se odsud už dokážu dostat. Klíč mám.";
                waitTime = 4f;
                Step = 10;
            }
            if (Part == 10)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> Takže pokud se neomluvíš, zůstáváš tady i s žížalama.";
                waitTime = 3f;
                Step = 11;
            }
            if (Part == 11)
            {
                timer = 0;
                DialogText.text = "<color=grey><b>Myška: </b></color> Ne! Ne! Ach jo. Tak já se ti teda omlouvám Samantho.";
                waitTime = 7f;
                Step = 12;
            }
            if (Part == 12)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Samantha: </b></color> První omluva za posledních sto let!";
                waitTime = 5f;
                Step = 13;
            }
            if (Part == 13)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Samantha: </b></color> Děkuji hrdino…! Hrdino? Haló hrdino…!";
                waitTime = 6f;
                Step = 14;
            }
            if (Part == 14)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> To mluvíš na mě? ";
                waitTime = 1.5f;
                Step = 15;
            }
            if (Part == 15)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Samantha: </b></color> Ano hrdino, chtěla jsem ti za odměnu dát ještě jednu radu.";
                waitTime = 4f;
                Step = 16;
            }
            if (Part == 16)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> Sem s ní. ";
                waitTime = 2f;
                Step = 17;
            }
            if (Part == 17)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Samantha: </b></color> Ať bude dračice sebevíc sexy, nelíbej ji.  ";
                waitTime = 4f;
                Step = 18;
            }
            if (Part == 18)
            {
                timer = 0;
                DialogText.text = "<color=grey><b>Myška: </b></color> rada nad zlato...! ";
                waitTime = 4f;
                Step = 19;
            }
            if (Part == 19)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> Ale no tak.  ";
                waitTime = 3f;
                Step = 20;

            }
            if (Part == 20)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color> Měj se Samantho!  ";
                waitTime = 2f;
                Step = 21;

            }
            if (Part == 21)
            {
                timer = 0;
                DialogText.text = "<color=yellow><b>Samantha: </b></color> Měj se hrdino! ";
                waitTime = 2f;
                Step = 22;
            }
            if (Part == 22)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color>A teď konečně vstříc slunci! Už mi od toho vlhka málem začaly plesnivět nohy. ";
                waitTime = 7.5f;
                Step = 23;
            }
            if (Part == 23)
            {
                timer = 0;
                DialogText.text = "<color=grey><b>Myška: </b></color> Myslíš to zelený, co máš mezi prsty?  ";
                waitTime = 3f;
                Step = 24;
            }
            if (Part == 24)
            {
                timer = 0;
                DialogText.text = "<color=brown><b>Leonard: </b></color>Ne, to je tam už dlouho, ale málem se to rozrostlo dál. ";
                waitTime = 4f;
                Step = 25;
            }
            if (Part == 25)
            {
                timer = 0;
                dialogControler.SetActive(false);
                PlayerScript.Instance.isMove = true;
                Destroy(DialogObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Trigger = true;
        }
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Trigger = false;
        }
    }
}
