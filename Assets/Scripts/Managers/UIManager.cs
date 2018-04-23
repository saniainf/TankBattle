using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle
{
    public class UIManager : MonoBehaviour
    {
        public Canvas[] UIPlayer;
        public Text[] PlayerHP;
        public Text[] PlayerEnergy;

        private PlayerAttributes[] players = new PlayerAttributes[4];

        void Start()
        {
            
        }

        void Update()
        {
            PlayerHP[0].text = Mathf.RoundToInt(players[0].PlayerCurrentHealth).ToString();
            PlayerEnergy[0].text = Mathf.RoundToInt(players[0].PlayerCurrentEnergy).ToString();

            PlayerHP[1].text = Mathf.RoundToInt(players[1].PlayerCurrentHealth).ToString();
            PlayerEnergy[1].text = Mathf.RoundToInt(players[1].PlayerCurrentEnergy).ToString();
        }

        public void SetPlayer(int i, PlayerAttributes player)
        {
            players[i] = player;
        }
    }
}