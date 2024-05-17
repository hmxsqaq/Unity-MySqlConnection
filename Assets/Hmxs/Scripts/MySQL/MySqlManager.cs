using MySql.Data.MySqlClient;
using UnityEngine;

namespace Hmxs.Scripts.MySQL
{
    public class MySqlManager : MonoBehaviour
    {
        [SerializeField] private ConnectionConfiguration connectionConfiguration;

        #region Singleton

        public static MySqlManager instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        private void Start() => ConnectToMySql();

        private void OnDestroy() => MySqlHelper.CloseConnection();

        private void ConnectToMySql()
        {
            if (connectionConfiguration == null)
            {
                Debug.LogError("ConnectionConfiguration is null");
                return;
            }

            var connection = MySqlHelper.CreateConnection(
                connectionConfiguration.server,
                connectionConfiguration.database,
                connectionConfiguration.uid,
                connectionConfiguration.password);

            try
            {
                connection.Open();
                Debug.Log("MySQL连接成功");
            }
            catch (MySqlException e)
            {
                Debug.LogError("MySQL连接失败：" + e.Message);
            }
        }
    }
}