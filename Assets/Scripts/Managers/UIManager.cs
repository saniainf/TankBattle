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
            PlayerHP[0].color = players[0].PlayerColor;
            PlayerEnergy[0].text = Mathf.RoundToInt(players[0].PlayerCurrentEnergy).ToString();

            PlayerHP[1].text = Mathf.RoundToInt(players[1].PlayerCurrentHealth).ToString();
            PlayerHP[1].color = players[1].PlayerColor;
            PlayerEnergy[1].text = Mathf.RoundToInt(players[1].PlayerCurrentEnergy).ToString();
        }

        public void SetPlayer(int i, PlayerAttributes player)
        {
            players[i] = player;
        }
    }
}