using UnityEngine;

namespace Hmxs.Scripts.MySQL
{
    [CreateAssetMenu(fileName = "ConnectionConfiguration", menuName = "Hmxs/ConnectionConfiguration")]
    public class ConnectionConfiguration : ScriptableObject
    {
        public string server = "127.0.0.1";
        public string database;
        public string uid = "root";
        public string password = "wzh02156514986";
    }
}