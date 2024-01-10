using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        // �{�^�����N���b�N���ꂽ���̃C�x���g�ɏI���֐���ǉ�
        exitButton.onClick.AddListener(ExitGame);
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}