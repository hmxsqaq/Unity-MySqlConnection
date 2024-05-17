using System.Collections;
using Hmxs.Scripts.Game;
using Hmxs.Scripts.MySQL;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts.UI
{
    public class UiManager : MonoBehaviour
    {
        [Title("Panel")]
        [SerializeField] private float transitionSpeed;
        [Required] [SerializeField] private CanvasGroup publicCanvas;
        [Required] [SerializeField] private CanvasGroup mainCanvas;
        [Required] [SerializeField] private CanvasGroup loginCanvas;
        [Required] [SerializeField] private CanvasGroup registerCanvas;
        [Required] [SerializeField] private InfoUI infoUI;

        [Title("MainPanel")]
        [Required] [SerializeField] private Button loginButton;
        [Required] [SerializeField] private Button registerButton;
        [Required] [SerializeField] private Button quitButton;

        [Title("LoginPanel")]
        [Required] [SerializeField] private InputField loginUsername;
        [Required] [SerializeField] private InputField loginPassword;
        [Required] [SerializeField] private Button loginSubmit;
        [Required] [SerializeField] private Button loginBack;

        [Title("RegisterPanel")]
        [Required] [SerializeField] private InputField registerUsername;
        [Required] [SerializeField] private InputField registerPassword;
        [Required] [SerializeField] private InputField registerEmail;
        [Required] [SerializeField] private Button registerSubmit;
        [Required] [SerializeField] private Button registerBack;

        private CanvasGroup _currentCanvas;

        #region Singleton

        public static UiManager instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        private void Start()
        {
            _currentCanvas = mainCanvas;

            loginButton.onClick.AddListener(() => StartCoroutine(ChangePanel(loginCanvas)));
            registerButton.onClick.AddListener(() => StartCoroutine(ChangePanel(registerCanvas)));
            quitButton.onClick.AddListener(Application.Quit);

            loginBack.onClick.AddListener(() => StartCoroutine(ChangePanel(mainCanvas)));
            loginSubmit.onClick.AddListener(Login);

            registerBack.onClick.AddListener(() => StartCoroutine(ChangePanel(mainCanvas)));
            registerSubmit.onClick.AddListener(Register);
        }

        private IEnumerator ChangePanel(CanvasGroup panel)
        {
            _currentCanvas.interactable = false;
            while (_currentCanvas.alpha > 0.1f)
            {
                _currentCanvas.alpha = Mathf.Lerp(_currentCanvas.alpha, 0, Time.deltaTime * transitionSpeed);
                yield return null;
            }
            _currentCanvas.alpha = 0;
            _currentCanvas.gameObject.SetActive(false);

            panel.gameObject.SetActive(true);
            panel.alpha = 0;
            while (panel.alpha < 0.9f)
            {
                panel.alpha = Mathf.Lerp(panel.alpha, 1, Time.deltaTime * transitionSpeed);
                yield return null;
            }

            panel.alpha = 1;
            panel.interactable = true;
            _currentCanvas = panel;
        }

        private void ShowInfo(string text, Color textColor) => infoUI.ShowInfo(text, textColor);

        private void Login()
        {
            var username = loginUsername.text;
            var password = loginPassword.text;
            if (string.IsNullOrEmpty(username))
            {
                ShowInfo("Username is empty", Color.red);
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                ShowInfo("Password is empty", Color.red);
                return;
            }

            var result = MySqlHelper.ExecuteQueryList($"SELECT Password FROM player WHERE Username = '{username}'");
            if (result.Count == 0)
            {
                ShowInfo("Username not found", Color.red);
                return;
            }
            var resultPassword = result[0][0];
            if (resultPassword != password)
            {
                ShowInfo("Password is incorrect", Color.red);
                return;
            }
            ShowInfo("Login successful", Color.green);

            StartCoroutine(CloseCanvas());
            var playerIndex = MySqlHelper.ExecuteQueryList($"SELECT PlayerID FROM player WHERE Username = '{username}'")[0][0];
            GameManager.instance.GameStart(int.Parse(playerIndex));
        }

        private void Register()
        {
            var username = registerUsername.text;
            var password = registerPassword.text;
            var email = registerEmail.text;
            if (string.IsNullOrEmpty(username))
            {
                ShowInfo("Username is empty", Color.red);
                return;
            }
            if (string.IsNullOrEmpty(email))
            {
                ShowInfo("Email is empty", Color.red);
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                ShowInfo("Password is empty", Color.red);
                return;
            }

            var result = MySqlHelper.ExecuteQueryList($"SELECT * FROM player WHERE Username = '{username}'");
            if (result.Count != 0)
            {
                ShowInfo("Username already exists", Color.red);
                return;
            }
            MySqlHelper.ExecuteNonQuery($"INSERT INTO player (Username, Email, Password, RegistrationDate) VALUES ('{username}', '{email}', '{password}', CURRENT_DATE)");
            ShowInfo("Register successful", Color.green);

            StartCoroutine(CloseCanvas());
            var playerIndex = MySqlHelper.ExecuteQueryList($"SELECT PlayerID FROM player WHERE Username = '{username}'")[0][0];
            GameManager.instance.GameStart(int.Parse(playerIndex));
        }

        private IEnumerator CloseCanvas()
        {
            _currentCanvas.interactable = false;
            var lerpValue = 1f;
            while (lerpValue > 0.1f)
            {
                lerpValue = Mathf.Lerp(lerpValue, 0, Time.deltaTime * transitionSpeed);
                _currentCanvas.alpha = lerpValue;
                publicCanvas.alpha = lerpValue;
                yield return null;
            }
            _currentCanvas.alpha = 0;
            publicCanvas.alpha = 0;
            _currentCanvas.gameObject.SetActive(false);
            publicCanvas.gameObject.SetActive(false);
        }
    }
}