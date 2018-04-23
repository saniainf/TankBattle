using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TankBattle
{
    public class GameManager : MonoBehaviour
    {
        public int m_NumRoundsToWin = 5;
        public float m_StarRoundtDelay = 3f;
        public float m_EndRoundDelay = 3f;
        public CameraControl m_CameraControl;
        public Text m_UIMessageText;
        public UIManager m_UIPlayers;
        public GameObject m_PlayerPrefab;
        public PlayerManager[] m_Players;

        private int roundNumber;
        private WaitForSeconds startRoundWait;
        private WaitForSeconds endRoundWait;
        private PlayerManager thisRoundWinner;
        private PlayerManager thisGameWinner;

        private void Start()
        {
            startRoundWait = new WaitForSeconds(m_StarRoundtDelay);
            endRoundWait = new WaitForSeconds(m_EndRoundDelay);

            SpawnAllTanks();
            SetCameraTargets();

            StartCoroutine(GameLoop());
        }


        private void SpawnAllTanks()
        {
            for (int i = 0; i < m_Players.Length; i++)
            {
                m_Players[i].m_InstancePlayer =
                    Instantiate(m_PlayerPrefab, m_Players[i].m_SpawnPoint.position, m_Players[i].m_SpawnPoint.rotation) as GameObject;
                m_Players[i].m_PlayerNumber = i + 1;
                m_Players[i].Setup();
                m_UIPlayers.SetPlayer(i, m_Players[i].m_PlayerAttributes);
            }
        }


        private void SetCameraTargets()
        {
            Transform[] targets = new Transform[m_Players.Length];

            for (int i = 0; i < targets.Length; i++)
            {
                targets[i] = m_Players[i].m_InstancePlayer.GetComponent<Transform>();
            }

            m_CameraControl.Targets = targets;
        }


        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(RoundStarting());
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());

            if (thisGameWinner != null)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                StartCoroutine(GameLoop());
            }

        }


        private IEnumerator RoundStarting()
        {
            ResetAllTanks();
            DisableTankControl();

            m_CameraControl.SetStartPositionAndSize();

            roundNumber++;
            m_UIMessageText.text = "ROUND " + roundNumber.ToString();

            yield return startRoundWait;
        }


        private IEnumerator RoundPlaying()
        {
            EnableTankControl();
            m_UIMessageText.text = string.Empty;

            while (!OneTankLeft())
            {
                yield return null;
            }
        }


        private IEnumerator RoundEnding()
        {
            DisableTankControl();

            thisRoundWinner = null;
            thisRoundWinner = GetRoundWinner();

            if (thisRoundWinner != null)
            {
                thisRoundWinner.m_WinsRounds++;
            }

            thisGameWinner = GetGameWinner();

            string message = EndMessage();
            m_UIMessageText.text = message;

            yield return endRoundWait;
        }


        private bool OneTankLeft()
        {
            int numTanksLeft = 0;

            for (int i = 0; i < m_Players.Length; i++)
            {
                if (m_Players[i].m_InstancePlayer.activeSelf)
                    numTanksLeft++;
            }

            return numTanksLeft <= 1;
        }


        private PlayerManager GetRoundWinner()
        {
            for (int i = 0; i < m_Players.Length; i++)
            {
                if (m_Players[i].m_InstancePlayer.activeSelf)
                    return m_Players[i];
            }

            return null;
        }


        private PlayerManager GetGameWinner()
        {
            for (int i = 0; i < m_Players.Length; i++)
            {
                if (m_Players[i].m_WinsRounds == m_NumRoundsToWin)
                    return m_Players[i];
            }

            return null;
        }


        private string EndMessage()
        {
            string message = "DRAW!";

            if (thisRoundWinner != null)
                message = thisRoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

            message += "\n\n\n\n";

            for (int i = 0; i < m_Players.Length; i++)
            {
                message += m_Players[i].m_ColoredPlayerText + ": " + m_Players[i].m_WinsRounds + " WINS\n";
            }

            if (thisGameWinner != null)
                message = thisGameWinner.m_ColoredPlayerText + "\n" + " WINS THE GAME!";

            return message;
        }


        private void ResetAllTanks()
        {
            for (int i = 0; i < m_Players.Length; i++)
            {
                m_Players[i].Reset();
            }
        }


        private void EnableTankControl()
        {
            for (int i = 0; i < m_Players.Length; i++)
            {
                m_Players[i].EnableControl();
            }
        }


        private void DisableTankControl()
        {
            for (int i = 0; i < m_Players.Length; i++)
            {
                m_Players[i].DisableControl();
            }
        }
    }
}