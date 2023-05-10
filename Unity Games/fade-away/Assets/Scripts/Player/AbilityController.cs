using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class AbilityController : MonoBehaviour
{
    internal bool bossstarted;
    private void Awake()
    {
        player = GetComponent<Player>();
        bossstarted = false;
    }

    Player player;
    [Header("TextBoxes")]
    [SerializeField] Text AbilityField;
    [SerializeField] Text TeleportBox;
    [SerializeField] GameObject zone3;
    [SerializeField] GameObject zone4;
    [SerializeField] GameObject Bossroom;
    internal bool npc1killed, npc2killed = false;
    public void ActivateAbility(string ability)
    {
        float PopUpTime = Time.time;
        if (ability == "Teleport")
        {
            AbilityField.text = "You Have Unlocked " + ability;
            TeleportBox.gameObject.SetActive(true);
            player.canTeleport = true;
            Destroy(AbilityField, 3f);
        }
        if(ability == "Zone3")
        {
            zone3.SetActive(true);
        }
        if(ability == "Finale")
        {
            if(npc1killed && npc2killed)
            {
                player.audioHandler.SoundtrackGame.pitch = 1.5f;
                zone4.SetActive(true);
                Finale();
            }
        }
        if (ability == "FinaleNPC1")
        {
            npc1killed = true;
        }
        if (ability == "FinaleNPC2")
        {
            npc2killed = true;
        }
    }
    [SerializeField] internal GameObject tpTarget;
    internal IEnumerator  Teleport()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = tpTarget.transform.position;
        Physics.SyncTransforms();
        player.YcamEnabled = false;
    }
    [SerializeField] internal Boss boss;
    internal void Finale()
    {
        bossstarted = true;
        player.transform.position = Bossroom.transform.position;
        player.look.x = Bossroom.transform.rotation.eulerAngles.y;
        player.look.y = Bossroom.transform.rotation.eulerAngles.z;
        Physics.SyncTransforms();

        boss.startBoss = true;
    }


}
