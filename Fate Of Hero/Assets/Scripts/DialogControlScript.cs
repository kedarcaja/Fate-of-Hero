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
    [Header("Dialog selection")]
    [Tooltip("From the menu, we can select what the dialog is about")]
    [SerializeField]
    private DialogList selectedDialog = DialogList.None;
    [Header("Object")]
    [SerializeField]
    private GameObject dialogControler;
    [SerializeField]
    private GameObject dialogObject;
    [Header("Choice")]
    [SerializeField]
    private Text partText;
    private int part, step;
    private float timer, waitTime;
    [Header("Sound track")]
    [SerializeField]
    private AudioClip voice;
    bool isPlay;
    bool trigger;

    private void Start()
    {
        if (selectedDialog == DialogList.None) { Debug.LogError("<color=Red><b>ERROR: </b> You forgot to choose what dialogue is going on </color>"); }
        if (!dialogControler) { Debug.LogError("<color=Red><b>ERROR: </b> The dialogControler object was not found </color>"); }
        if (!partText) { Debug.LogError("<color=Red><b>ERROR: </b> Text partText was not found </color>"); }
        if (!dialogObject) { Debug.LogError("<color=Red><b>ERROR: </b> The dialogObject object was not found </color>"); }
        if (!voice) { Debug.LogError("<color=Red><b>ERROR: </b> The voice object was not found </color>"); }
    }

    void Update()
    {
        if (timer >= waitTime)
            {
                part = step; Dialog();

            }
        else { timer += UnityEngine.Time.deltaTime; }
        if (trigger)
        {
            if (Input.GetKeyDown(KeyCode.E) && isPlay==false)
            {
                isPlay = true;
                dialogControler.SetActive(true);
                part = 1; Dialog();
                AudioSource.PlayClipAtPoint(voice, transform.position, 1f);
                PlayerScript.Instance.isMove = false;
            }
        }
    }

    public void Dialog()
    {
        if (selectedDialog == DialogList.D_MS01_02)
        {

            if (part == 1)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> To jako fakt ?";
                waitTime = 2f;
                step = 2;
            }
            else if (part == 2)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> Kytky přece nepláčou, kytky tiše rostou.";
                waitTime = 4.39f;
                step = 3;
            }
            else if (part == 3)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> No tak jó, tak jó, ale budu si připadat jako blázen.";
                waitTime = 4.64f;
                step = 4;
            }
            else if (part == 4)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> Květino ? ";
                waitTime = 4f;
                step = 5;
            }
            else if (part == 5)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Květina: </b></color> Co chceš ?";
                waitTime = 2f;
                step = 6;
            }
            else if (part == 6)
            {
                partText.text = "<color=yellow><b>Květina: </b></color> Jo, já vím! Chceš mi vzít další okvětní lístek.";
                waitTime = 6f;
                step = 7;
            }
            else if (part == 7)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Květina: </b></color> Tak víš co ? Tak já ti ho teda dám dobrovolně, nemusíš mi ho škubat jako posledně.";
                waitTime = 7f;
                step = 8;
            }
            else if (part == 8)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Květina: </b></color>  To vážně bolelo !";
                waitTime = 2f;
                step = 9;
            }
            else if (part == 9)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> Počkej počkej! Nechci tvoje lístky, hledám cestu ven.";
                waitTime = 5f;
                step = 10;
            }
            else if (part == 10)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> Ty  jsi opravdu mluvící kytka ?";
                waitTime = 4f;
                step = 11;
            }
            else if (part == 11)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color>To jako fakt?";
                waitTime = 2f;
                step = 12;
            }
            else if (part == 12)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color>Nepraštil jsem se při tom pádu moc do hlavy?";
                waitTime = 2f;
                step = 13;
            }
            else if (part == 13)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Květina: </b></color> No a co? Umím mluvit, naučila jsem se to, když jsem byla malá";
                waitTime = 6f;
                step = 14;
            }
            else if (part == 14)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Květina: </b></color> Jako každý.";
                waitTime = 2f;
                step = 15;
            }
            else if (part == 15)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Květina: </b></color>To pocházíš z divného kraje, když se to u vás děti neučí.";
                waitTime = 4f;
                step = 16;
            }
            else if (part == 16)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color>No já vlastně asi vůbec nejsem odsud.";
                waitTime = 2f;
                step = 17;
            }
            else if (part == 17)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color>A kde to podle tebe teda jsme?";
                waitTime = 2f;
                step = 18;
            }
            else if (part == 18)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Květina: </b></color>Nacházíš se na Antorském ostrově přímo v srdci Gortarianského impéria.";
                waitTime = 8f;
                step = 19;
            }
            else if (part == 19)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color>To víš že jó,";
                waitTime = 2f;
                step = 20;

            }
            else if (part == 20)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Květina: </b></color>Ale Abych pravdu řekla, na tomhle místě není nic kouzelného. Mám tu jen samé trápení.";
                waitTime = 8f;
                step = 21;
            }
            else if (part == 21)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color>Víš co kytko? Zkusím ti najít ten tvůj vyškubnutý lístek, ať je ti líp.";
                waitTime = 6f;
                step = 22;
            }
            else if (part == 22)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color>Ani mě by se tu nechtělo tiše a poklidně růst jako každá jiná normální květina!";
                waitTime = 9f;
                step = 23;
            }
            else if (part == 23)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color>Třeba mě mezitím to šálení přejde.";
                waitTime = 4f;
                step = 24;

            }
            else if (part == 24)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Květina: </b></color>A neříkej mi kytko, uráží mě to! Jmenuji se Samantha.";
                waitTime = 6f;
                step = 25;
            }
            else if (part == 25)
            {
                timer = 0;
                dialogControler.SetActive(false);
                PlayerScript.Instance.isMove = true;
                Destroy(dialogObject);
            }
        }
        if (selectedDialog == DialogList.D_MS01_03)
        {
            if (part == 1)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> Kvě… SSS… jo Samatho, donesl jsem ti ten tvůj lístek.";
                waitTime = 7f;
                step = 2;
            }
            if (part == 2)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Samantha: </b></color> Jé, děkuji, tady máš za něj klíč k mříži od stoky!";
                waitTime = 5f;
                step = 3;
            }
            if (part == 3)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Samantha: </b></color> Až mi přiroste zpátky, tak budu zase ta nejkrásnější květina široko daleko. ";
                waitTime = 6f;
                step = 4 ;
            }
            if (part == 4)
            {
                timer = 0;
                partText.text = "<color=grey><b>Myška: </b></color> Ty? Ha ha ha vždyť i žížaly jsou krásnější, než ty.";
                waitTime = 6f;
                step = 5;
            }
            if (part == 5)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Samantha: </b></color> A proč je tady tahle ?";
                waitTime = 2f;
                step = 6;
            }
            if (part == 6)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> E...";
                waitTime = 1f;
                step = 7;
            }
            if (part == 7)
            {
                timer = 0;
                partText.text = "<color=grey><b>Myška: </b></color> Jsem tu, abych ti řekla, že si jdu nahoru sehnat nový klobouk, který ty nikdy mít nebudeš, dřív tady uhniješ!";
                waitTime = 12.2f;
                step = 8;
            }
            if (part == 8)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> Ale… holky…nechte toho. Myška se ti přišla omluvit, Samantho.";
                waitTime = 7.8f;
                step = 9;
            }
            if (part == 9)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> Dělej se omluv, sám se odsud už dokážu dostat. Klíč mám.";
                waitTime = 4f;
                step = 10;
            }
            if (part == 10)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> Takže pokud se neomluvíš, zůstáváš tady i s žížalama.";
                waitTime = 3f;
                step = 11;
            }
            if (part == 11)
            {
                timer = 0;
                partText.text = "<color=grey><b>Myška: </b></color> Ne! Ne! Ach jo. Tak já se ti teda omlouvám Samantho.";
                waitTime = 7f;
                step = 12;
            }
            if (part == 12)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Samantha: </b></color> První omluva za posledních sto let!";
                waitTime = 5f;
                step = 13;
            }
            if (part == 13)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Samantha: </b></color> Děkuji hrdino…! Hrdino? Haló hrdino…!";
                waitTime = 6f;
                step = 14;
            }
            if (part == 14)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> To mluvíš na mě? ";
                waitTime = 1.5f;
                step = 15;
            }
            if (part == 15)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Samantha: </b></color> Ano hrdino, chtěla jsem ti za odměnu dát ještě jednu radu.";
                waitTime = 4f;
                step = 16;
            }
            if (part == 16)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> Sem s ní. ";
                waitTime = 2f;
                step = 17;
            }
            if (part == 17)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Samantha: </b></color> Ať bude dračice sebevíc sexy, nelíbej ji.  ";
                waitTime = 4f;
                step = 18;
            }
            if (part == 18)
            {
                timer = 0;
                partText.text = "<color=grey><b>Myška: </b></color> rada nad zlato...! ";
                waitTime = 4f;
                step = 19;
            }
            if (part == 19)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> Ale no tak.  ";
                waitTime = 3f;
                step = 20;

            }
            if (part == 20)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color> Měj se Samantho!  ";
                waitTime = 2f;
                step = 21;

            }
            if (part == 21)
            {
                timer = 0;
                partText.text = "<color=yellow><b>Samantha: </b></color> Měj se hrdino! ";
                waitTime = 2f;
                step = 22;
            }
            if (part == 22)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color>A teď konečně vstříc slunci! Už mi od toho vlhka málem začaly plesnivět nohy. ";
                waitTime = 7.5f;
                step = 23;
            }
            if (part == 23)
            {
                timer = 0;
                partText.text = "<color=grey><b>Myška: </b></color> Myslíš to zelený, co máš mezi prsty?  ";
                waitTime = 3f;
                step = 24;
            }
            if (part == 24)
            {
                timer = 0;
                partText.text = "<color=brown><b>Leonard: </b></color>Ne, to je tam už dlouho, ale málem se to rozrostlo dál. ";
                waitTime = 4f;
                step = 25;
            }
            if (part == 25)
            {
                timer = 0;
                dialogControler.SetActive(false);
                PlayerScript.Instance.isMove = true;
                Destroy(dialogObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trigger = true;
        }
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trigger = false;
        }
    }
}
