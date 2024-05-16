using System.Collections.Generic;
using UnityEngine;

namespace Hmxs.Scripts
{
    public class MySqlManager : MonoBehaviour
    {
        [SerializeField] private ConnectionConfiguration connectionConfiguration;

        public List<string> data;

        #region Singleton

        public static MySqlManager instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        private void Start()
        {
            ConnectToMySql();
        }

        private void OnDestroy()
        {
            MySqlHelper.CloseConnection();
        }

        private void ConnectToMySql()
        {
            if (connectionConfiguration == null)
            {
                Debug.LogError("ConnectionConfiguration is null");
                return;
            }

            MySqlHelper.OpenConnection(
                connectionConfiguration.server,
                connectionConfiguration.database,
                connectionConfiguration.uid,
                connectionConfiguration.password);
        }

        public void InspectTable(string table)
        {
            data = MySqlHelper.GetTableStringList(table);
            foreach (var d in data)
                Debug.Log(d);
        }
    }
}