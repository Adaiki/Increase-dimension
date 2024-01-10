using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// increase dimensions�Q�[���̏������e
/// </summary>
public class DimensionGame : MonoBehaviour
{
    // �������Ƃ́iUnity��Canvas�ƃ����N���Ă���j�e�{�^����e�L�X�g��
    // �������Ƃ̓_���A�O���[�h�A�����x�𒙑�����ϐ�
    [SerializeField] private TextMeshProUGUI pointText_1d;
    [SerializeField] private TextMeshProUGUI gradeText_1d;
    [SerializeField] private TextMeshProUGUI nextGradePointText_1d;
    [SerializeField] private UnityEngine.UI.Button upgradeButton_1d;
    [SerializeField] private UnityEngine.UI.Button prestigeButton_1d;
    private float points_1d;
    private float grades_1d;
    private float acceleration_1d;

    [SerializeField] private TextMeshProUGUI pointText_2d;
    [SerializeField] private TextMeshProUGUI gradeText_2d;
    [SerializeField] private TextMeshProUGUI nextGradePointText_2d;
    [SerializeField] private UnityEngine.UI.Button upgradeButton_2d;
    [SerializeField] private UnityEngine.UI.Button prestigeButton_2d;
    private float points_2d;
    private float grades_2d;
    private float acceleration_2d;

    [SerializeField] private TextMeshProUGUI pointText_3d;
    [SerializeField] private TextMeshProUGUI gradeText_3d;
    [SerializeField] private TextMeshProUGUI nextGradePointText_3d;
    [SerializeField] private UnityEngine.UI.Button upgradeButton_3d;
    [SerializeField] private UnityEngine.UI.Button prestigeButton_3d;
    private float points_3d;
    private float grades_3d;
    private float acceleration_3d;

    // �Q�[���I�u�W�F�N�g
    private GameObject myObject;

    // �J�����I�u�W�F�N�g
    private GameObject cameraObject;

    // �I�u�W�F�N�g�̃T�C�Y�ϐ�
    private float xLength;
    private float yLength;
    private float zLength;

    // Prestige_3d��3�i�K�ω��̐؂�ւ��ϐ�
    private bool lastPrestigeSwitch;

    // �O���[�h�̐ݒ�
    private List<float> gradeCosts;

    /// <summary>
    /// ������
    /// </summary>
    private void Start()
    {
        // �����l�ݒ�
        Initialize();

        // ���X�V
        UpdateUI();
    }

    /// <summary>
    /// �X�V
    /// </summary>
    private void Update()
    {
        // �T�C�Y�X�V
        UpdateLength();

        // ���X�V
        UpdateUI();
    }

    /// <summary>
    /// �����P�̃O���[�h�A�b�v
    /// </summary>
    private void Upgrade_1d()
    {
        GradeUp(grades_1d, ref points_1d, ref acceleration_1d, ref grades_1d, gradeText_1d, nextGradePointText_1d, prestigeButton_1d, false);
    }

    /// <summary>
    /// �����Q�̃O���[�h�A�b�v
    /// </summary>
    private void Upgrade_2d()
    {
       GradeUp(grades_2d, ref points_2d, ref acceleration_2d, ref grades_2d, gradeText_2d, nextGradePointText_2d, prestigeButton_2d, false);
    }

    /// <summary>
    /// �����R�̃O���[�h�A�b�v
    /// </summary>
    private void Upgrade_3d()
    {
        GradeUp(grades_3d, ref points_3d, ref acceleration_3d, ref grades_3d, gradeText_3d, nextGradePointText_3d, prestigeButton_3d, lastPrestigeSwitch);
    }

    /// <summary>
    /// �����P�̃v���X�e�[�W(�����Q�̊J��)
    /// </summary>
    private void Prestige_1d()
    {
        if (grades_1d >= 13 && yLength == 0)
        {
            myObject.transform.localScale = new Vector3(0f, 2f, 0f);
            cameraObject.transform.position = new Vector3(4050, 300, -1000);
            points_1d = 0;
            points_2d = 0;
            grades_2d = 1;
            prestigeButton_1d.gameObject.SetActive(false);
            gradeText_2d.text = "Grade: " + grades_2d.ToString();
            nextGradePointText_1d.text = "Upgrade: " + gradeCosts[(int)grades_1d - 1].ToString("F0") + "Points";
            nextGradePointText_2d.text = "Upgrade: " + gradeCosts[(int)grades_2d - 1].ToString("F0") + "Points \nPrestige: 13Grade";
            prestigeButton_1d.interactable = false;
            upgradeButton_2d.interactable = true;
            prestigeButton_2d.interactable = true;
            upgradeButton_2d.onClick.AddListener(Upgrade_2d);
            prestigeButton_2d.onClick.AddListener(Prestige_2d);
            xLength = 0;
        }
        UpdateUI();
    }

    /// <summary>
    /// �����Q�̃v���X�e�[�W(�����R�̊J��)
    /// </summary>
    private void Prestige_2d()
    {
        if (grades_2d >= 13 && zLength == 0)
        {
            myObject.transform.localScale = new Vector3(0f, 1f, 1f);
            cameraObject.transform.position = new Vector3(3700, 600, -500);
            cameraObject.transform.localRotation = Quaternion.Euler(new Vector3(20, 30, 0));
            points_1d = 0;
            points_2d = 0;
            points_3d = 0;
            grades_3d = 1;
            prestigeButton_2d.gameObject.SetActive(false);
            gradeText_3d.text = "Grade: " + grades_3d.ToString();
            nextGradePointText_2d.text = "Upgrade: " + gradeCosts[(int)grades_2d - 1].ToString("F0") + "Points";
            nextGradePointText_3d.text = "Upgrade: " + gradeCosts[(int)grades_3d - 1].ToString("F0") + "Points \nPrestige: 13Grade";
            prestigeButton_2d.interactable = false;
            upgradeButton_3d.interactable = true;
            prestigeButton_3d.interactable = true;
            upgradeButton_3d.onClick.AddListener(Upgrade_3d);
            prestigeButton_3d.onClick.AddListener(Prestige_3d);
            xLength = 0;
            yLength = 0;
        }
        UpdateUI();
    }

    /// <summary>
    /// �����R�̃v���X�e�[�W(�`��ω���1��ځF���A2��ځF�g�Q�g�Q�{�[��)
    /// </summary>
    private void Prestige_3d()
    {
        if (grades_3d >= 13 && zLength > 0 && lastPrestigeSwitch == false)
        {
            myObject.transform.localScale = new Vector3(1f, 1f, 1f);
            cameraObject.transform.position = new Vector3(4050, 300, -1000);
            cameraObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            points_1d = 0;
            points_2d = 0;
            points_3d = 0;
            xLength = 0;
            yLength = 0;
            zLength = 0;
            lastPrestigeSwitch = true;
            nextGradePointText_3d.text = "Upgrade: " + gradeCosts[(int)grades_3d - 1].ToString("F0") + "Points \nPrestige: 20Grade";
            ChangeMesh(GetSphereMesh());
        }

        if (grades_3d >= 20 && zLength > 0 && lastPrestigeSwitch == true)
        {
            myObject.transform.localScale = new Vector3(0, 0, 0);
            cameraObject.transform.position = new Vector3(4050, 300, -1000);
            points_1d = 0;
            points_2d = 0;
            points_3d = 0;
            xLength = 0;
            yLength = 0;
            zLength = 0;
            prestigeButton_3d.gameObject.SetActive(false);
            prestigeButton_3d.interactable = false;
            nextGradePointText_3d.text = "Upgrade: " + gradeCosts[(int)grades_3d - 1].ToString("F0") + "Points";
            ChangeMesh(GetSpikedSphereMesh());
        }
        UpdateUI();
    }

    /// <summary>
    /// �X�V���̏���(�e�L�X�g�X�V�A��ʒ���)
    /// </summary>
    private void UpdateUI()
    {
        TextUpdate();
        WithinTheScreen();
    }

    /// <summary>
    /// �e�평���l�ݒ�
    /// </summary>
    private void Initialize()
    {
        myObject = GameObject.Find("EvolvingObject");
        myObject.transform.localScale = new Vector3(0f, 2f, 0f);

        cameraObject = GameObject.Find("Main Camera");
        cameraObject.transform.position = new Vector3(4050, 300, -350);
        cameraObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        gradeCosts = new List<float> { 10, 30 };
        for (int i = 1; i < 99; i++)
        {
            float nextNumber = gradeCosts[i] + (gradeCosts[i] - gradeCosts[i - 1]) * 2;
            gradeCosts.Add(nextNumber);
        }


        grades_1d = 1;
        points_1d = 0;
        acceleration_1d = 1;
        acceleration_2d = 1;
        acceleration_3d = 1;
        gradeText_1d.text = "Grade: " + grades_1d.ToString();
        nextGradePointText_1d.text = "Upgrade: " + gradeCosts[(int)grades_1d - 1].ToString("F0") + "Points \nPrestige: 13Grade";
        xLength = 0;
        yLength = 0;
        zLength = 0;
        lastPrestigeSwitch = false;
        upgradeButton_2d.interactable = false;
        prestigeButton_2d.interactable = false;
        upgradeButton_3d.interactable = false;
        prestigeButton_3d.interactable = false;
        upgradeButton_1d.onClick.AddListener(Upgrade_1d);
        prestigeButton_1d.onClick.AddListener(Prestige_1d);
    }

    /// <summary>
    /// �I�u�W�F�N�g�̊e���W�X�V
    /// </summary>
    private void UpdateLength()
    {
        UpdateAxis(ref points_1d, ref xLength, grades_1d, acceleration_1d, 3000000, Vector3.right);
        UpdateAxis(ref points_2d, ref yLength, grades_2d, acceleration_2d, 2900000, Vector3.up);
        UpdateAxis(ref points_3d, ref zLength, grades_3d, acceleration_3d, 2800000, Vector3.forward);
    }

    /// <summary>
    /// �I�u�W�F�N�g�̊e���W�X�V�̌v�Z
    /// </summary>
    /// <param name="points">�|�C���g</param>
    /// <param name="length">����</param>
    /// <param name="grades">�O���[�h</param>
    /// <param name="acceleration">�����x</param>
    /// <param name="divisor">���Z�l</param>
    /// <param name="axis">�X�V������W��</param>
    private void UpdateAxis(ref float points, ref float length, float grades, float acceleration, float divisor, Vector3 axis)
    {
        if (grades >= 1)
        {
            points += grades * Time.deltaTime * acceleration;
            float scale = points / divisor;
            length += scale;
            myObject.transform.localScale += axis * scale;
        }
    }

    /// <summary>
    /// �e�L�X�g�X�V
    /// </summary>
    private void TextUpdate()
    {
        pointText_1d.text = "Points: " + points_1d.ToString();
        pointText_2d.text = "Points: " + points_2d.ToString();
        pointText_3d.text = "Points: " + points_3d.ToString();
    }

    /// <summary>
    /// �O���[�h�A�b�v�̏���
    /// </summary>
    /// <param name="gradeIndex">�O���[�h</param>
    /// <param name="points">�|�C���g</param>
    /// <param name="acceleration">�����x</param>
    /// <param name="grades">�O���[�h</param>
    /// <param name="gradeText">�O���[�h�̕\������</param>
    /// <param name="nextGradePointText">�l�N�X�g�O���[�h�|�C���g�̕\������</param>
    /// <param name="prestigeButton">�v���X�e�[�W�{�^��</param>
    /// <param name="lastPrestigeSwitch">���X�g�v���X�e�[�W�X�C�b�`</param>
    private void GradeUp(float gradeIndex, ref float points, ref float acceleration, ref float grades, TextMeshProUGUI gradeText, TextMeshProUGUI nextGradePointText, UnityEngine.UI.Button prestigeButton, bool lastPrestigeSwitch)
    {
        if (points >= gradeCosts[(int)grades - 1] && grades < 100)
        {
            points -= gradeCosts[(int)grades - 1];
            acceleration *= 1.2f;
            grades++;

            gradeText.text = "Grade: " + grades.ToString();
            nextGradePointText.text = "Upgrade: " + gradeCosts[(int)grades - 1].ToString("F0") + "Points";

            if (prestigeButton.interactable == true && !lastPrestigeSwitch)
            {
                nextGradePointText.text += "\nPrestige: 13Grade";
            }

            if (prestigeButton.interactable == true && lastPrestigeSwitch)
            {
                nextGradePointText.text += "\nPrestige: 20Grade";
            }
        }

        if (grades == 100)
        {
            if (gradeText != null)
            {
                gradeText.gameObject.SetActive(false);
                gradeText.text += "(MAX)";
            }
        }
        UpdateUI();
    }

    /// <summary>
    /// ���b�V���̕ύX����
    /// </summary>
    /// <param name="getMesh">�擾�������b�V��</param>
    private void ChangeMesh(Mesh getMesh)
    {
        // EvolvingObject�ɕύX���鏈��
        GameObject gameObject = GameObject.Find("EvolvingObject"); // ���݂�gameObject���擾

        // ���݂�gameObject��MeshFilter���擾
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();

        // EvolvingObject��Mesh���擾
        Mesh currentMesh = getMesh;

        // MeshFilter��Mesh��EvolvingObject��Mesh�ɐ؂�ւ�
        meshFilter.mesh = currentMesh;

        if (gameObject.GetComponent<BoxCollider>() != null)
        {
            Destroy(gameObject.GetComponent<BoxCollider>());
            gameObject.AddComponent<SphereCollider>();
        }

        Debug.Log("Object changed to EvolvingObject!");
    }

    /// <summary>
    /// �����b�V���̎擾
    /// </summary>
    /// <returns>�����b�V��</returns>
    private Mesh GetSphereMesh()
    {
        // Sphere��Mesh���擾���鏈��
        GameObject tempSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Mesh sphereMesh = tempSphere.GetComponent<MeshFilter>().sharedMesh;
        Destroy(tempSphere);
        return sphereMesh;
    }

    /// <summary>
    /// �g�Q�g�Q�{�[�����b�V���̎擾
    /// </summary>
    /// <returns>�g�Q�g�Q�{�[�����b�V��</returns>
    private Mesh GetSpikedSphereMesh()
    {
        // SpikedSphere��Mesh���擾���鏈��
        GameObject tempSphere = GameObject.Find("SpikedSphere");
        Mesh sphereMesh = tempSphere.GetComponent<MeshFilter>().sharedMesh;
        Destroy(tempSphere);
        return sphereMesh;
    }

    /// <summary>
    /// ��ʒ���
    /// </summary>
    private void WithinTheScreen()
    {
        if (prestigeButton_1d.interactable == true && (1200 <= myObject.transform.localScale.x))
        {
            myObject.transform.localScale *= 0.05f;
        }

        if (prestigeButton_2d.interactable == true && (3500 <= myObject.transform.localScale.x || 1900 <= myObject.transform.localScale.y))
        {
            myObject.transform.localScale *= 0.05f;
        }

        if (prestigeButton_3d.interactable == true)
        {
            if (lastPrestigeSwitch == false && (1000 <= myObject.transform.localScale.x || 1000 <= myObject.transform.localScale.y || 1000 <= myObject.transform.localScale.z))
            {
                myObject.transform.localScale *= 0.05f;
            }

            if (lastPrestigeSwitch == true && (2000 <= myObject.transform.localScale.x || 2000 <= myObject.transform.localScale.y || 2000 <= myObject.transform.localScale.z))
            {
                myObject.transform.localScale *= 0.05f;
            }
        }

        if (prestigeButton_3d.interactable == false && lastPrestigeSwitch == true && (600 <= myObject.transform.localScale.x || 600 <= myObject.transform.localScale.y || 600 <= myObject.transform.localScale.z))
        {
            myObject.transform.localScale *= 0.05f;
        }
    }
}
