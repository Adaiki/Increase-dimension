using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// increase dimensionsゲームの処理内容
/// </summary>
public class DimensionGame : MonoBehaviour
{
    // 次元ごとの（UnityのCanvasとリンクしている）各ボタンやテキストと
    // 次元ごとの点数、グレード、加速度を貯蔵する変数
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

    // ゲームオブジェクト
    private GameObject myObject;

    // カメラオブジェクト
    private GameObject cameraObject;

    // オブジェクトのサイズ変数
    private float xLength;
    private float yLength;
    private float zLength;

    // Prestige_3dの3段階変化の切り替え変数
    private bool lastPrestigeSwitch;

    // グレードの設定
    private List<float> gradeCosts;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        // 初期値設定
        Initialize();

        // 情報更新
        UpdateUI();
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        // サイズ更新
        UpdateLength();

        // 情報更新
        UpdateUI();
    }

    /// <summary>
    /// 次元１のグレードアップ
    /// </summary>
    private void Upgrade_1d()
    {
        GradeUp(grades_1d, ref points_1d, ref acceleration_1d, ref grades_1d, gradeText_1d, nextGradePointText_1d, prestigeButton_1d, false);
    }

    /// <summary>
    /// 次元２のグレードアップ
    /// </summary>
    private void Upgrade_2d()
    {
       GradeUp(grades_2d, ref points_2d, ref acceleration_2d, ref grades_2d, gradeText_2d, nextGradePointText_2d, prestigeButton_2d, false);
    }

    /// <summary>
    /// 次元３のグレードアップ
    /// </summary>
    private void Upgrade_3d()
    {
        GradeUp(grades_3d, ref points_3d, ref acceleration_3d, ref grades_3d, gradeText_3d, nextGradePointText_3d, prestigeButton_3d, lastPrestigeSwitch);
    }

    /// <summary>
    /// 次元１のプレステージ(次元２の開放)
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
    /// 次元２のプレステージ(次元３の開放)
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
    /// 次元３のプレステージ(形状変化⇒1回目：球、2回目：トゲトゲボール)
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
    /// 更新時の処理(テキスト更新、画面調整)
    /// </summary>
    private void UpdateUI()
    {
        TextUpdate();
        WithinTheScreen();
    }

    /// <summary>
    /// 各種初期値設定
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
    /// オブジェクトの各座標更新
    /// </summary>
    private void UpdateLength()
    {
        UpdateAxis(ref points_1d, ref xLength, grades_1d, acceleration_1d, 3000000, Vector3.right);
        UpdateAxis(ref points_2d, ref yLength, grades_2d, acceleration_2d, 2900000, Vector3.up);
        UpdateAxis(ref points_3d, ref zLength, grades_3d, acceleration_3d, 2800000, Vector3.forward);
    }

    /// <summary>
    /// オブジェクトの各座標更新の計算
    /// </summary>
    /// <param name="points">ポイント</param>
    /// <param name="length">長さ</param>
    /// <param name="grades">グレード</param>
    /// <param name="acceleration">加速度</param>
    /// <param name="divisor">除算値</param>
    /// <param name="axis">更新する座標軸</param>
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
    /// テキスト更新
    /// </summary>
    private void TextUpdate()
    {
        pointText_1d.text = "Points: " + points_1d.ToString();
        pointText_2d.text = "Points: " + points_2d.ToString();
        pointText_3d.text = "Points: " + points_3d.ToString();
    }

    /// <summary>
    /// グレードアップの処理
    /// </summary>
    /// <param name="gradeIndex">グレード</param>
    /// <param name="points">ポイント</param>
    /// <param name="acceleration">加速度</param>
    /// <param name="grades">グレード</param>
    /// <param name="gradeText">グレードの表示文言</param>
    /// <param name="nextGradePointText">ネクストグレードポイントの表示文言</param>
    /// <param name="prestigeButton">プレステージボタン</param>
    /// <param name="lastPrestigeSwitch">ラストプレステージスイッチ</param>
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
    /// メッシュの変更処理
    /// </summary>
    /// <param name="getMesh">取得したメッシュ</param>
    private void ChangeMesh(Mesh getMesh)
    {
        // EvolvingObjectに変更する処理
        GameObject gameObject = GameObject.Find("EvolvingObject"); // 現在のgameObjectを取得

        // 現在のgameObjectのMeshFilterを取得
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();

        // EvolvingObjectのMeshを取得
        Mesh currentMesh = getMesh;

        // MeshFilterのMeshをEvolvingObjectのMeshに切り替え
        meshFilter.mesh = currentMesh;

        if (gameObject.GetComponent<BoxCollider>() != null)
        {
            Destroy(gameObject.GetComponent<BoxCollider>());
            gameObject.AddComponent<SphereCollider>();
        }

        Debug.Log("Object changed to EvolvingObject!");
    }

    /// <summary>
    /// 球メッシュの取得
    /// </summary>
    /// <returns>球メッシュ</returns>
    private Mesh GetSphereMesh()
    {
        // SphereのMeshを取得する処理
        GameObject tempSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Mesh sphereMesh = tempSphere.GetComponent<MeshFilter>().sharedMesh;
        Destroy(tempSphere);
        return sphereMesh;
    }

    /// <summary>
    /// トゲトゲボールメッシュの取得
    /// </summary>
    /// <returns>トゲトゲボールメッシュ</returns>
    private Mesh GetSpikedSphereMesh()
    {
        // SpikedSphereのMeshを取得する処理
        GameObject tempSphere = GameObject.Find("SpikedSphere");
        Mesh sphereMesh = tempSphere.GetComponent<MeshFilter>().sharedMesh;
        Destroy(tempSphere);
        return sphereMesh;
    }

    /// <summary>
    /// 画面調整
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
