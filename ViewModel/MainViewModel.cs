using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace WpfAppDemo1.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _QuestionText;

        public string QuestionText
        {
            get => _QuestionText;
            set
            {
                _QuestionText = value;
                RaisePropertyChanged();
            }
        }

        private string _OptionAText;

        public string OptionAText
        {
            get => _OptionAText;
            set
            {
                _OptionAText = value;
                RaisePropertyChanged();
            }
        }

        private string _OptionBText;

        public string OptionBText
        {
            get => _OptionBText;
            set
            {
                _OptionBText = value;
                RaisePropertyChanged();
            }
        }

        private string _OptionCText;

        public string OptionCText
        {
            get => _OptionCText;
            set
            {
                _OptionCText = value;
                RaisePropertyChanged();
            }
        }

        private string _OptionDText;

        public string OptionDText
        {
            get => _OptionDText;
            set
            {
                _OptionDText = value;
                RaisePropertyChanged();
            }
        }


        private bool _OptionASelected;
        public bool OptionASelected
        {
            get => _OptionASelected;
            set
            {
                _OptionASelected = value;
                RaisePropertyChanged();
            }
        }

        private bool _OptionBSelected;
        public bool OptionBSelected
        {
            get => _OptionBSelected;
            set
            {
                _OptionBSelected = value;
                RaisePropertyChanged();
            }
        }

        private bool _OptionCSelected;
        public bool OptionCSelected
        {
            get => _OptionCSelected;
            set
            {
                _OptionCSelected = value;
                RaisePropertyChanged();
            }
        }

        private bool _OptionDSelected;
        public bool OptionDSelected
        {
            get => _OptionDSelected;
            set
            {
                _OptionDSelected = value;
                RaisePropertyChanged();
            }
        }


        private bool _OptionAllQuestionSelected = true;
        //ȫ����Ŀ��ť
        public bool OptionAllQuestionSelected
        {
            get => _OptionAllQuestionSelected;
            set
            {
                _OptionAllQuestionSelected = value;
                RaisePropertyChanged();
            }
        }

        //����ʷ��Ŀ��ť
        private bool _OptionCMHSelected;
        public bool OptionCMHSelected
        {
            get => _OptionCMHSelected;
            set
            {
                _OptionCMHSelected = value;
                RaisePropertyChanged();
            }
        }

        //ë����Ŀ��ť
        private bool _OptionMTSelected;
        public bool OptionMTSelected
        {
            get => _OptionMTSelected;
            set
            {
                _OptionMTSelected = value;
                RaisePropertyChanged();
            }
        }

        //˼����Ŀ��ť
        private bool _OptionIMCSelected;
        public bool OptionIMCSelected
        {
            get => _OptionIMCSelected;
            set
            {
                _OptionIMCSelected = value;
                RaisePropertyChanged();
            }
        }

        //��ԭ��Ŀ��ť
        private bool _OptionBTMSelected;
        public bool OptionBTMSelected
        {
            get => _OptionBTMSelected;
            set
            {
                _OptionBTMSelected = value;
                RaisePropertyChanged();
            }
        }
        
        private string _AnswerText;
        public string AnswerText
        {
            get => _AnswerText;
            set
            {
                _AnswerText = value;
                RaisePropertyChanged();
            }
        }

        //��������Ŀ
        private string _StatisticsDone;
        public string StatisticsDone
        {
            get => _StatisticsDone;
            set
            {
                _StatisticsDone = value;
                RaisePropertyChanged();
            }
        }

        //��ȷ��
        private string _CorrectRate;
        public string CorrectRate
        {
            get => _CorrectRate;
            set
            {
                _CorrectRate = value;
                RaisePropertyChanged();
            }

        }

        private int _Index;

        public int Index
        {
            get => _Index;
            set
            {
                if (value >= 0 && value < QuestionList.Count)
                {
                    LastIndex = _Index;
                    _Index = value;
                    OnIndexChanged();
                }
            }
        }

        public int LastIndex { get; set; }


        private int _MaxQuestionNumber;
        public int MaxQuestionNumber
        {
            get => _MaxQuestionNumber;
            set
            {
                if (value >= 0 && value < 100)
                {
                    _MaxQuestionNumber = value;
                }
            }
        }

        //�Ƿ��Ƕ�ѡ��
        public bool IsMultiSelectMode { get; set; }
        //�Ƿ���ģʽ
        private bool _IsTestMode;
        public bool IsTestMode
        {
            get => _IsTestMode;
            set
            {
                _IsTestMode = value;
                RaisePropertyChanged();
            }
        }

        //�Ƿ���������ֱ����ʾ��
        private bool _IsAnswerDisplay = true;
        public bool IsAnswerDisplay
        {
            get => _IsAnswerDisplay;
            set
            {
                _IsAnswerDisplay = value;
                RaisePropertyChanged();
            }

        }

        //�Ƿ��Զ���ת����һ��
        private bool _IsAutomaticMoveNext;
        public bool IsAutomaticMoveNext
        {
            get => _IsAutomaticMoveNext;
            set
            {
                _IsAutomaticMoveNext = value;
                RaisePropertyChanged();
            }
        }

        //�Ƿ������Ŀ
        private bool _IsRandomQuestion;
        public bool IsRandomQuestion
        {
            get => _IsRandomQuestion;
            set
            {
                _IsRandomQuestion = value;
                RaisePropertyChanged();
            }

        }


        /// <summary>
        /// ������Ŀ���б�
        /// </summary>
        public List<Question> QuestionList { get; set; }
        public List<Question> AllQuestions { get; set; }
        public List<Question>[] SubjectsQuestions { get; set; }
        /// <summary>
        /// ��¼�Ҳ���Ŀѡ��ť����Ϣ
        /// </summary>
        public List<ButtonInfo> ButtonInfoList { get; set; }
        /// <summary>
        /// ��¼�ش���������״̬ 0�ش���� 1�ش���ȷ -1��ѡ��𰸵�δ�ύ
        /// </summary>
        public Dictionary<int, (string userAnswer, int status)> AnswerRecord { get; set; }
        public Dictionary<int, (string userAnswer, int status)>[] Records { get; set; }


        public RelayCommand NextButtonCommand { get; set; }
        public RelayCommand LastButtonCommand { get; set; }
        public RelayCommand SubmitButtonCommand { get; set; }
        public RelayCommand NextButtonInfoPage { get; set; }
        public RelayCommand LastButtonInfoPage { get; set; }

        public RelayCommand<string> OptionButtonCommand { get; set; }

        /// <summary>
        /// �Ҳ���Ŀѡ��ť����ɫֵ
        /// </summary>
        public SolidColorBrush[] ColorBrushes { get; set; } =
        {
            new SolidColorBrush(Colors.Gainsboro), //δ������ɫ
            new SolidColorBrush(Colors.LightGreen),//�����ɫ
            new SolidColorBrush(Colors.LightCoral) //�����ɫ
        };

        private void OnIndexChanged()
        {
            IsMultiSelectMode = QuestionList[Index].Answer.Length > 1;

            QuestionText = (IsMultiSelectMode?"����ѡ�⡿":"") + (Index + 1) + "." + QuestionList[Index].QuestionDescription;
            OptionAText = "A." + QuestionList[Index].OptionA;
            OptionBText = "B." + QuestionList[Index].OptionB;
            OptionCText = "C." + QuestionList[Index].OptionC;
            OptionDText = "D." + QuestionList[Index].OptionD;
            
            //������Ŀ����������Ŀ��ԭ�ֳ�
            if (AnswerRecord.ContainsKey(QuestionList[Index].Id) && IsAnswerDisplay)
            {
                var ans = AnswerRecord[QuestionList[Index].Id].userAnswer;
                OptionASelected = ans.Contains("A");
                OptionBSelected = ans.Contains("B");
                OptionCSelected = ans.Contains("C");
                OptionDSelected = ans.Contains("D");
                OnSubmitButtonClick(false);
            }
            else
            {
                AnswerText = "";
                OptionASelected = OptionBSelected = OptionCSelected = OptionDSelected = false;

            }

            //�������Ҳ���Ŀѡ��ť
            if (ButtonInfoList == null || ButtonInfoList.Count == 0)
            {
                return;
            }
            if (Index + 1 > int.Parse(ButtonInfoList.Last().Number))
            {
                ChangeButtonInfoPage(MaxQuestionNumber,false);
            }
            else if (Index + 1 < int.Parse(ButtonInfoList.First().Number))
            {
                ChangeButtonInfoPage(-MaxQuestionNumber,false);
            }

            //StatisticsDone = $"��ǰ:{Index + 1}  �Ѵ�:{AnswerRecord.Count}/{QuestionList.Count}";
            UpdateAnswerInformation();
        }

        private void OnNextButtonClick()
        {
            var next = Index + 1;
            if (IsRandomQuestion)
            {
                //if (AnswerRecord.Count>=QuestionList.Count)
                //{
                //    return;
                //}
                //do
                //{
                //    next = new Random().Next(0, QuestionList.Count);
                //} while (AnswerRecord.ContainsKey(QuestionList[Index].Id));
                next = new Random().Next(0, QuestionList.Count);
            }

            Index = next;
        }

        private void OnLastButtonClick()
        {
            --Index;
        }

        private void OnSubmitButtonClick(bool isAutoMoveNext)
        {
            var userAnswer = (OptionASelected ? "A" : "") + (OptionBSelected ? "B" : "") +
                             (OptionCSelected ? "C" : "") + (OptionDSelected ? "D" : "");

            if (userAnswer == QuestionList[Index].Answer)
            {
                AnswerText = $"�ش���ȷ\n��ȷ��Ϊ{QuestionList[Index].Answer}";
                AnswerRecord[QuestionList[Index].Id] = (userAnswer, 1);
                
                if(ButtonInfoList == null || ButtonInfoList.Count == 0) { return;}
                ButtonInfoList[Index % MaxQuestionNumber].BackGroundSolidColorBrush =
                    ColorBrushes[1];
            }
            else
            {
                AnswerText = $"�ش����\n��ȷ��Ϊ{QuestionList[Index].Answer}";
                AnswerRecord[QuestionList[Index].Id] = (userAnswer, 0);

                if (ButtonInfoList == null || ButtonInfoList.Count == 0) { return; }
                ButtonInfoList[Index % MaxQuestionNumber].BackGroundSolidColorBrush =
                    ColorBrushes[2];
            }

            UpdateAnswerInformation();

            if (IsAutomaticMoveNext && isAutoMoveNext)
            {
                OnNextButtonClick();
            }
        }

        private void OnOptionButtonClick(string op)
        {
            switch (op)
            {
                case "_A_":
                    if (IsMultiSelectMode)
                    {
                        OptionASelected = !OptionASelected;
                    }
                    else
                    {
                        if (OptionASelected)
                        {
                            OptionASelected = false;
                        }
                        else
                        {
                            OptionASelected = true;
                            OptionBSelected = false;
                            OptionCSelected = false;
                            OptionDSelected = false;
                        }
                    }
                    break;
                case "_B_":
                    if (IsMultiSelectMode)
                    {
                        OptionBSelected = !OptionBSelected;
                    }
                    else
                    {
                        if (OptionBSelected)
                        {
                            OptionBSelected = false;
                        }
                        else
                        {
                            OptionASelected = false;
                            OptionBSelected = true;
                            OptionCSelected = false;
                            OptionDSelected = false;
                        }
                    }
                    break;
                case "_C_":
                    if (IsMultiSelectMode)
                    {
                        OptionCSelected = !OptionCSelected;
                    }
                    else
                    {
                        if (OptionCSelected)
                        {
                            OptionCSelected = false;
                        }
                        else
                        {
                            OptionASelected = false;
                            OptionBSelected = false;
                            OptionCSelected = true;
                            OptionDSelected = false;
                        }
                    }
                    break;
                case "_D_":
                    if (IsMultiSelectMode)
                    {
                        OptionDSelected = !OptionDSelected;
                    }
                    else
                    {
                        if (OptionDSelected)
                        {
                            OptionDSelected = false;
                        }
                        else
                        {
                            OptionASelected = false;
                            OptionBSelected = false;
                            OptionCSelected = false;
                            OptionDSelected = true;
                        }
                    }
                    break;
                case "ȫ��":
                    OptionAllQuestionSelected = true;
                    OptionCMHSelected = false;//����ʷ
                    OptionBTMSelected = false;//��ԭ
                    OptionIMCSelected = false;//˼��
                    OptionMTSelected = false;//ë��
                    InitQuestionList("ȫ��");
                    break;
                case "ë��":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//����ʷ
                    OptionBTMSelected = false;//��ԭ
                    OptionIMCSelected = false;//˼��
                    OptionMTSelected = true;//ë��
                    InitQuestionList("ë��");
                    break;
                case "˼��":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//����ʷ
                    OptionBTMSelected = false;//��ԭ
                    OptionIMCSelected = true;//˼��
                    OptionMTSelected = false;//ë��
                    InitQuestionList("˼��");

                    break;
                case "����ʷ":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = true;//����ʷ
                    OptionBTMSelected = false;//��ԭ
                    OptionIMCSelected = false;//˼��
                    OptionMTSelected = false;//ë��
                    InitQuestionList("����ʷ");

                    break;
                case "��ԭ":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//����ʷ
                    OptionBTMSelected = true;//��ԭ
                    OptionIMCSelected = false;//˼��
                    OptionMTSelected = false;//ë��
                    InitQuestionList("��ԭ");
                    break;
                default:
                    InitQuestionList(op);
                    break;
            }
        }

        private void UpdateAnswerInformation()
        {
            StatisticsDone = $"��ǰ:{Index + 1}  �ܹ�:{QuestionList.Count}";
            CorrectRate = $"�Ѵ�:{AnswerRecord.Count}/{AllQuestions.Count} ��ȷ��:{(double)AnswerRecord.Count(a => a.Value.status==1) / AnswerRecord.Count * 100:F1}%";
        }

        private void ChangeButtonInfoPage(int change,bool moveOnePage)
        {
            if (change + int.Parse(ButtonInfoList.Last().Number) <= 0 ||
                change + int.Parse(ButtonInfoList.First().Number) >= QuestionList.Count)
            {
                return;
            }

            if (!moveOnePage)
            {
                if (Index > LastIndex)
                {
                    change += change*((Index + 1 - int.Parse(ButtonInfoList.Last().Number)) / MaxQuestionNumber) ;
                }
                else
                {
                    change = -MaxQuestionNumber;
                    change += change*((int.Parse(ButtonInfoList.First().Number) - Index - 1) / MaxQuestionNumber) ;
                }
            }

            for (int i = 0; i < MaxQuestionNumber; i++)
            {
                var newNumber = int.Parse(ButtonInfoList[i].Number) + change;
                ButtonInfoList[i].Number = newNumber.ToString();
                ButtonInfoList[i].BackGroundSolidColorBrush = ColorBrushes[0];
                if (newNumber < 1 || newNumber > QuestionList.Count) continue;

                if (AnswerRecord.ContainsKey(QuestionList[newNumber - 1].Id))
                {
                    ButtonInfoList[i].BackGroundSolidColorBrush = AnswerRecord[QuestionList[newNumber - 1].Id].status==1 
                        ? ColorBrushes[1] : ColorBrushes[2];
                }
            }

        }

        private void InitQuestionList(string op)
        {
            switch (op)
            {
                case "ȫ��":
                    QuestionList = AllQuestions;
                    Index = 0;
                    LastIndex = 0;
                    break;
                case "ë��":
                    QuestionList = SubjectsQuestions[(int)Subject.MaoZedongThought];
                    Index = 0;
                    LastIndex = 0;

                    break;
                case "˼��":
                    QuestionList = SubjectsQuestions[(int)Subject.IdeologicalAndMoralCultivation];
                    Index = 0;
                    LastIndex = 0;

                    break;
                case "����ʷ":
                    QuestionList = SubjectsQuestions[(int)Subject.ChineseModernHistory];
                    Index = 0;
                    LastIndex = 0;

                    break;
                case "��ԭ":
                    QuestionList = SubjectsQuestions[(int)Subject.BasicTenetsOfMarxism];
                    Index = 0;
                    LastIndex = 0;

                    break;
                default:
                    if (op.Trim()=="")
                    {
                        MessageBox.Show("��������Ϊ��");
                        return;
                    }
                    var tempList = AllQuestions.Where(q => q.QuestionDescription.Contains(op)).ToList();
                    if (tempList.Count == 0)
                    {
                        MessageBox.Show("û���������");
                        return;
                    }
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//����ʷ
                    OptionBTMSelected = false;//��ԭ
                    OptionIMCSelected = false;//˼��
                    OptionMTSelected = false;//ë��
                    QuestionList = tempList;
                    Index = 0;
                    LastIndex = 0;

                    MessageBox.Show($"������{tempList.Count}����Ŀ");
                    break;
            }

            var begin = (Index / MaxQuestionNumber) * MaxQuestionNumber;
            if (ButtonInfoList.Count==0)
            {
                for (int i = 0; i < MaxQuestionNumber; i++)
                {
                    ButtonInfoList.Add(new ButtonInfo());
                }
            }
            for (int i = 0; i < MaxQuestionNumber; i++)
            {
                var newNumberIndex = begin + i + 1;
                ButtonInfoList[i].Number = newNumberIndex.ToString();
                ButtonInfoList[i].BackGroundSolidColorBrush = ColorBrushes[0];
                //var buttonInfo = new ButtonInfo
                //{
                //    Number = (newNumberIndex + 1).ToString(),
                //    BackGroundSolidColorBrush = ColorBrushes[0]
                //};
                //if (ButtonInfoList.Count!=50)
                //{
                //    ButtonInfoList.Add(buttonInfo);
                //}

                if (newNumberIndex < 1 || newNumberIndex > QuestionList.Count) continue;
                if (AnswerRecord.ContainsKey(QuestionList[newNumberIndex - 1].Id))
                {
                    ButtonInfoList[i].BackGroundSolidColorBrush = AnswerRecord[QuestionList[newNumberIndex - 1].Id].status==1 ? ColorBrushes[1] : ColorBrushes[2];
                }

            }

            UpdateAnswerInformation();
        }
        public MainViewModel()
        {
            MaxQuestionNumber = 50;
            AnswerRecord = new Dictionary<int, (string userAnswer, int status)>();
            ButtonInfoList = new List<ButtonInfo>(MaxQuestionNumber);

            NextButtonCommand = new RelayCommand(OnNextButtonClick);
            LastButtonCommand = new RelayCommand(OnLastButtonClick);
            SubmitButtonCommand = new RelayCommand(() => OnSubmitButtonClick(IsAutomaticMoveNext));
            OptionButtonCommand = new RelayCommand<string>(OnOptionButtonClick);
            LastButtonInfoPage = new RelayCommand(() => ChangeButtonInfoPage(-MaxQuestionNumber,true));
            NextButtonInfoPage = new RelayCommand(() => ChangeButtonInfoPage(MaxQuestionNumber,true));

            if (File.Exists("AnswerRecord.txt"))
            {
                AnswerRecord = JsonConvert.DeserializeObject<Dictionary<int, (string userAnswer, int status)>>(File.ReadAllText("AnswerRecord.txt"));
            }



            //�����ݿ��ж�ȡ����
            //var questionDatabase = new QuestionDatabase($@"{Environment.CurrentDirectory}\MyQuestions.mdf");
            //AllQuestions = (from question in questionDatabase.Questions select question).ToList();

            //��json�ļ��ж�ȡ����
            if (File.Exists("Questions.txt"))
                AllQuestions = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText("AllQuestions.json"));

            SubjectsQuestions = new List<Question>[4];
            for (int i = 0; i < 4; i++)
            {
                SubjectsQuestions[i] = AllQuestions.Where(q => q.Type == i).ToList();
            }

           
            //QuestionList = AllQuestions;

            InitQuestionList("ȫ��");
            //Index = File.Exists("Index.txt") ? int.Parse(File.ReadAllText("Index.txt")) : 0;
            //LastIndex = Index;

            //var begin = (Index / MaxQuestionNumber) * MaxQuestionNumber;
            //for (int i = 0; i < MaxQuestionNumber; i++)
            //{
            //    var newNumberIndex = begin + i;
            //    var buttonInfo = new ButtonInfo
            //    {
            //        Number = (newNumberIndex + 1).ToString(), BackGroundSolidColorBrush = ColorBrushes[0]
            //    };
            //    ButtonInfoList.Add(buttonInfo);
            //    if (newNumberIndex>=QuestionList.Count)
            //    {
            //        continue;
            //    }
            //    if (AnswerRecord.ContainsKey(QuestionList[newNumberIndex].Id))
            //    {
            //        buttonInfo.BackGroundSolidColorBrush = AnswerRecord[QuestionList[newNumberIndex].Id].isCorrect ? ColorBrushes[1] : ColorBrushes[2];
            //    }
            //}
            //UpdateAnswerInformation();
        }
    }

    public class ButtonInfo:ViewModelBase
    {
        public ButtonInfo() { }
        private string _Number;
        public string Number
        {
            get => _Number;
            set
            {
                _Number = value;
                RaisePropertyChanged();
            }
        }

        private SolidColorBrush _BackGroundSolidColorBrush;
        public SolidColorBrush BackGroundSolidColorBrush
        {
            get => _BackGroundSolidColorBrush;
            set
            {
                _BackGroundSolidColorBrush = value;
                RaisePropertyChanged();
            }
        }
    }

    public class QuestionDatabase : DataContext
    {
        public Table<Question> Questions;

        public QuestionDatabase(string fileOrServerOrConnection) : base(fileOrServerOrConnection)
        {
        }
    }

    [Table(Name = "QuestionTable")]
    public class Question
    {
        [Column(IsPrimaryKey = true)] public int Id; //��Ŀ���
        [Column] public int Type; //��Ŀ����

        [Column] public string QuestionDescription; //���
        [Column] public string OptionA;
        [Column] public string OptionB;
        [Column] public string OptionC;
        [Column] public string OptionD;
        [Column] public string Answer;

        [Column] public int AnswerTimes = 0; //�ش������Ĵ���
        [Column] public int WrongAnswerTimes = 0; //�ش����Ĵ���
        public string LastAnswer = "";
        [Column(CanBeNull = true)] public string Notes = ""; //��ע ֪ʶ�� ����˼·��

        public Question(int id, Subject type, string questionDescription, string optionA, string optionB,
            string optionC,
            string optionD, string answer)
        {
            Id = id;
            Type = (int) type;
            QuestionDescription = questionDescription;
            OptionA = optionA;
            OptionB = optionB;
            OptionC = optionC;
            OptionD = optionD;
            Answer = answer;
        }

        //�������޲ι��캯��
        public Question()
        {
        }
    }

    public enum Subject
    {
        ChineseModernHistory = 0, //����ʷ
        BasicTenetsOfMarxism = 1, //��ԭ
        IdeologicalAndMoralCultivation = 2, //˼��
        MaoZedongThought = 3 //ë��
    }

  
}