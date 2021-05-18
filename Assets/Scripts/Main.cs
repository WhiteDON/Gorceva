using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;

public class Main : MonoBehaviourPunCallbacks
{
    public InputField PlayerNameInputField;
    public Text Timer;
    public GameObject ErrorMessage;
    public InputField PasswordInputField;
    public GameObject EnterUI;
    public GameObject RoomUI;
    public Text PlayersCounter;
    public GameObject MasterUI;
    public GameObject firstRes;
    public GameObject EnterButton; 
    public GameObject CreateButton; 
    public InputField TestNumberInputField;
    public Text NumberOfTest;
    public Text[] playersInRoom;
    public GameObject playersList;
    public Text PasswordErrorLabel;
    public InputField[] TestsAnswers;
    public GameObject[] TestForJuniors;
    public InputField LobbyNameinput;
    public Dropdown myDropdown;
    int countPlayer = 0;
    [SerializeField]
    private int testnumber;
    private PhotonView photonView2;
    private bool a = true;
    int score = 0;
    private int answcount = 0;
    private string[] answers = {"отрасль","трудовые ресурсы","сектор экономики","трудовые ресурсы (кадры) предприятия","отрасли экономики","трудовые ресурсы","отрасли, производящие товары и отрасли, оказывающие услуги","производственные ресурсы","отраслям, производящим товары (материальные блага)","рынок труда","производство","структура предприятия","предметный, технологический, смешанный (предметно-технологический)","учреждение","технологическим процессом","фирма","консорциум","общество с ограниченной ответственностью","в различных организационно-правовых формах","концерны","на прерывный и непрерывный","ремонтное хозяйство","вспомогательное производство","безрельсовый и рельсовый","транспортное хозяйство","производственная инфраструктура","инструментальное хозяйство","бесперебойную и эффективную работу основного производства","складское хозяйство","внешний, межцеховой, внутрицеховой и внутрискладской","вспомогательный производственный процесс","пропорциональность","цехов","параллельность","смешанный вид движения","длительность производственного цикла","ритмичность производственного процесса","прямоточность","обслуживающий производственный процесс","производственный процесс","лизинг","по полной первоначальной стоимости","аренда","здания, сооружения, передаточные устройства, машины и оборудование, транспортные средства, инструменты и приспособления, производственный и хозяйственный инвентарь","рентинг, хайринг, лизинг","фондоотдача, фондоемкость","денежное возмещение износа основных фондов","рентинг","коэффициент интенсивного использования оборудования","хайринг","часть средств производства, вещественные элементы которые в процессе труда расходуются в каждом производственном цикле","оборотным производственным фондам","совокупность элементов, образующих оборотные средства","собственные и заемные","делением объема реализации продукции на средний остаток оборотных средств на предприятии","в каждом производственном цикле","сразу, в течение 1 оборота","готовая продукция на складах предприятия","средства, постоянно находящиеся в распоряжении предприятия","кредиты банка, кредиторская задолженность","расценкам за единицу произведенной продукции","система должностных окладов","заработная плата рабочего данного разряда в единицу времени (час, день, месяц)","вспомогательных рабочих","вознаграждение, которое получает работник от предприятия в зависимости от количества и качества затраченного им труда и результатов деятельности всего коллектива","совокупность нормативных документов, регулирующих условия получения рабочим основной части заработной платы","определения соотношения в оплате труда различных групп рабочих","возможность точного учета объемов выполняемых работ","при сдельно-прогрессивной","при соответствующем отдельном письменном приказе","себестоимость","мировых цен","включаются в себестоимость","публикуемые цены","не включаются в себестоимость","30%","все перечисленные","калькуляция","публикуемые и расчетные","переменные и постоянные затраты","финансовое обеспечение","чистая прибыль","финансовое состояние","балансовая прибыль","под постоянным наблюдением государства","прибыль","общая рентабельность предприятия","абсолютным приростом суммы прибыли","валовая прибыль","выручка","планирование","все перечисленные","3-5 лет","элементами планирования","он не предположение, а четкое задание","самопланирование","должны быть достигнуты в результате осуществления этих планов","со временем, поскольку они разрабатываются на определенный период","форма внутрифирменного планирования","бизнес-план","экономическая эффективность","сопоставления результатов и затрат на достижение этих результатов","интегральный экономический эффект","суммирования экономических эффектов, рассчитанных для каждого года в отдельности","годовой экономический эффект","предприятие получает дополнительную прибыль за счет сверхпланового выпуска продукции","в финансовых результатах","общей оборачиваемости капитала","число кругооборотов имущества предприятия за определенный период","проследить динамику и структуру изменений финансового состояния предприятия"};

    void Start()
    {
        Connect();
        myDropdown.onValueChanged.AddListener(delegate {
         myDropdownValueChangedHandler(myDropdown);
     });
    
    
    }
    public void SwitchAnswer(GameObject Next)
    {
    Next.SetActive(true);
    }
     public void EndAnswer()
    {
    EnterUI.SetActive(true);
    RoomUI.SetActive(false);
    }
    public void Answerbtn(String btntext)
    {
         PhotonView photonView1 = this.gameObject.GetComponent<PhotonView>();
         
         score++;
        foreach(string i in answers)
        {
            if(btntext == i)
            {
              
             photonView1.RPC("results", RpcTarget.All, photonView2.ViewID.ToString(),score,true);
             return;
            }
            else
            {
                
             photonView1.RPC("results", RpcTarget.All, photonView2.ViewID.ToString(),score,false);
            }
        }
    }
    private void myDropdownValueChangedHandler(Dropdown target) {
    if(target.value == 0){
     EnterButton.SetActive(true);
     CreateButton.SetActive(false);
     PasswordInputField.gameObject.SetActive(false);
     PasswordErrorLabel.gameObject.SetActive(false);
    }
    else if(target.value == 1){
     EnterButton.SetActive(false);
     CreateButton.SetActive(true);
     PasswordInputField.gameObject.SetActive(true);
     PasswordErrorLabel.gameObject.SetActive(true);
    }
 }
 public void SetDropdownIndex(int index) {
     myDropdown.value = index;
 }
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
       
        Debug.Log("Connected To the master");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"OnJoinRandomFailed, returnCode:{returnCode} message:{message}");

        PhotonNetwork.CreateRoom(null, new RoomOptions
        {
            MaxPlayers = 4
        });
    }
    public override void OnJoinedRoom()
    {
        EnterUI.SetActive(false);
        RoomUI.SetActive(true);
        PlayersCounter.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/" + "4";
        if (PhotonNetwork.IsMasterClient)
        {
            RoomUI.SetActive(false);
            MasterUI.SetActive(true);
        }
        GameObject Hui = PhotonNetwork.Instantiate("GameObject", new Vector3(0, 0, 0), Quaternion.identity, 0);
         photonView2 = Hui.GetComponent<PhotonView>();
        Debug.Log(PhotonNetwork.CurrentRoom);
        Debug.Log(PhotonNetwork.NickName.ToString());
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayersCounter.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        UpdatePlayerList();
    }
       public void UpdatePlayerList()
    {
        countPlayer = 0;
        foreach (var players in PhotonNetwork.PlayerListOthers)
        {
            if (players.NickName == PhotonNetwork.MasterClient.NickName)
            {
                playersInRoom[countPlayer].text = players.NickName + " (Учитель)";
            }
            else
            {
                playersInRoom[countPlayer].text = players.NickName;
            }
            
            countPlayer++;
        }
    }
    public void Play()
    {
        if(PlayerNameInputField.text == "Горцева Т.Н" && PasswordInputField.text == "123456"){
        PhotonNetwork.NickName = $"{PlayerNameInputField.text}";
        if(LobbyNameinput.text != null)
        {
            PhotonNetwork.CreateRoom("test");
        }
        }
        else {
            PasswordErrorLabel.text = "Пароль неверен, попробуйте еще раз.";
        }
    }

    public void EnterLobby()
    {
        PhotonNetwork.NickName = $"{PlayerNameInputField.text}";
        if (LobbyNameinput.text != null)
        {
            PhotonNetwork.JoinRoom("test");
        }

    }
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void StartTest()
    {
        testnumber = int.Parse(TestNumberInputField.text.ToString());
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("Test", RpcTarget.All, testnumber);
    }
    IEnumerator ExecuteAfterTime(float timeInSec)
      {
     float counter = timeInSec;
     while (counter >= 0) {
         yield return new WaitForSeconds (1.0f);
         counter--;
          Timer.text = counter.ToString();
          if(counter == 0)
          {
               EnterUI.SetActive(true);
                RoomUI.SetActive(false);
          }
     }
       
      }
    [PunRPC]
    public void Test(int testnumber)
    {
        
        if(!PhotonNetwork.IsMasterClient)
        {
            TestForJuniors[testnumber].SetActive(true);
            StartCoroutine(ExecuteAfterTime(120));
        }
        
    }
     public void answerdo(bool errororsucs, string testname, string viewID)
     {
         if(viewID == "2001")
         {
          firstRes = GameObject.Find("Junior1");
         }
         else if (viewID == "3001")
         {
              firstRes = GameObject.Find("Junior2");
         }
         else if (viewID == "4001")
         {
              firstRes = GameObject.Find("Junior3");
         }
         else if (viewID == "5001")
         {
              firstRes = GameObject.Find("Junior4");
         }
          
             firstRes = firstRes.transform.Find(testname).gameObject;
             firstRes.SetActive(true);
             if(errororsucs)
             {
                 firstRes.GetComponent<Image>().color = new Color32(0,128,0,255);
             }
             else
             {
                 firstRes.GetComponent<Image>().color = new Color32(255,0,0,255);
             }
     }

    [PunRPC]
    public void results(String viewID, int answrnmbr, bool errororsucs)
    {
          if(PhotonNetwork.IsMasterClient)
          {
           if(answrnmbr == 1)
           {
             answerdo(errororsucs, "firsttest",viewID);
           }
           else if(answrnmbr == 2)
           {
               answerdo(errororsucs, "secondtest",viewID);
           }
           else if(answrnmbr == 3)
           {
               answerdo(errororsucs, "thirdtest",viewID);
           }
           else if(answrnmbr == 4)
           {
               answerdo(errororsucs, "fourtest",viewID);
           }
           else if(answrnmbr == 5)
           {
               answerdo(errororsucs, "fivetest",viewID);
           }
           else if(answrnmbr == 6)
           {
               answerdo(errororsucs, "sixtest",viewID);
           }
           else if(answrnmbr == 7)
           {
               answerdo(errororsucs, "seventest",viewID);
           }
           else if(answrnmbr == 8)
           {
               answerdo(errororsucs, "eigthtest",viewID);
           }
           else if(answrnmbr == 9)
           {
               answerdo(errororsucs, "ninetest",viewID);
           }
           else if(answrnmbr == 10)
           {
               answerdo(errororsucs, "tentest",viewID);
           }
           else{

           }
          }
    }
    public void LobbyList()
    {
       
        if (a == true)
        {
            a = false;
            playersList.SetActive(true);
            UpdatePlayerList();
        }
        else if ( a == false)
        {
            a = true;
            playersList.SetActive(false);
        }

    }
    
    


}
