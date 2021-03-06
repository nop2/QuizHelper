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
        //????????????
        public bool OptionAllQuestionSelected
        {
            get => _OptionAllQuestionSelected;
            set
            {
                _OptionAllQuestionSelected = value;
                RaisePropertyChanged();
            }
        }

        //??????????????
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

        //????????????
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

        //????????????
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

        //????????????
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

        //??????????
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

        //??????
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

        //????????????
        public bool IsMultiSelectMode { get; set; }
        //????????????
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

        //????????????????????????
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

        //????????????????????
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

        //????????????
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
        /// ??????????????
        /// </summary>
        public List<Question> QuestionList { get; set; }
        public List<Question> AllQuestions { get; set; }
        public List<Question>[] SubjectsQuestions { get; set; }
        /// <summary>
        /// ??????????????????????????
        /// </summary>
        public List<ButtonInfo> ButtonInfoList { get; set; }
        /// <summary>
        /// ?????????????????????? 0???????? 1???????? -1??????????????????
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
        /// ????????????????????????
        /// </summary>
        public SolidColorBrush[] ColorBrushes { get; set; } =
        {
            new SolidColorBrush(Colors.Gainsboro), //??????????
            new SolidColorBrush(Colors.LightGreen),//????????
            new SolidColorBrush(Colors.LightCoral) //????????
        };

        private void OnIndexChanged()
        {
            IsMultiSelectMode = QuestionList[Index].Answer.Length > 1;

            QuestionText = (IsMultiSelectMode?"??????????":"") + (Index + 1) + "." + QuestionList[Index].QuestionDescription;
            OptionAText = "A." + QuestionList[Index].OptionA;
            OptionBText = "B." + QuestionList[Index].OptionB;
            OptionCText = "C." + QuestionList[Index].OptionC;
            OptionDText = "D." + QuestionList[Index].OptionD;
            
            //????????????????????????????
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

            //????????????????????????
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

            //StatisticsDone = $"????:{Index + 1}  ????:{AnswerRecord.Count}/{QuestionList.Count}";
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
                AnswerText = $"????????\n??????????{QuestionList[Index].Answer}";
                AnswerRecord[QuestionList[Index].Id] = (userAnswer, 1);
                
                if(ButtonInfoList == null || ButtonInfoList.Count == 0) { return;}
                ButtonInfoList[Index % MaxQuestionNumber].BackGroundSolidColorBrush =
                    ColorBrushes[1];
            }
            else
            {
                AnswerText = $"????????\n??????????{QuestionList[Index].Answer}";
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
                case "????":
                    OptionAllQuestionSelected = true;
                    OptionCMHSelected = false;//??????
                    OptionBTMSelected = false;//????
                    OptionIMCSelected = false;//????
                    OptionMTSelected = false;//????
                    InitQuestionList("????");
                    break;
                case "????":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//??????
                    OptionBTMSelected = false;//????
                    OptionIMCSelected = false;//????
                    OptionMTSelected = true;//????
                    InitQuestionList("????");
                    break;
                case "????":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//??????
                    OptionBTMSelected = false;//????
                    OptionIMCSelected = true;//????
                    OptionMTSelected = false;//????
                    InitQuestionList("????");

                    break;
                case "??????":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = true;//??????
                    OptionBTMSelected = false;//????
                    OptionIMCSelected = false;//????
                    OptionMTSelected = false;//????
                    InitQuestionList("??????");

                    break;
                case "????":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//??????
                    OptionBTMSelected = true;//????
                    OptionIMCSelected = false;//????
                    OptionMTSelected = false;//????
                    InitQuestionList("????");
                    break;
                default:
                    InitQuestionList(op);
                    break;
            }
        }

        private void UpdateAnswerInformation()
        {
            StatisticsDone = $"????:{Index + 1}  ????:{QuestionList.Count}";
            CorrectRate = $"????:{AnswerRecord.Count}/{AllQuestions.Count} ??????:{(double)AnswerRecord.Count(a => a.Value.status==1) / AnswerRecord.Count * 100:F1}%";
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
                case "????":
                    QuestionList = AllQuestions;
                    Index = 0;
                    LastIndex = 0;
                    break;
                case "????":
                    QuestionList = SubjectsQuestions[(int)Subject.MaoZedongThought];
                    Index = 0;
                    LastIndex = 0;

                    break;
                case "????":
                    QuestionList = SubjectsQuestions[(int)Subject.IdeologicalAndMoralCultivation];
                    Index = 0;
                    LastIndex = 0;

                    break;
                case "??????":
                    QuestionList = SubjectsQuestions[(int)Subject.ChineseModernHistory];
                    Index = 0;
                    LastIndex = 0;

                    break;
                case "????":
                    QuestionList = SubjectsQuestions[(int)Subject.BasicTenetsOfMarxism];
                    Index = 0;
                    LastIndex = 0;

                    break;
                default:
                    if (op.Trim()=="")
                    {
                        MessageBox.Show("????????????");
                        return;
                    }
                    var tempList = AllQuestions.Where(q => q.QuestionDescription.Contains(op)).ToList();
                    if (tempList.Count == 0)
                    {
                        MessageBox.Show("????????????");
                        return;
                    }
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//??????
                    OptionBTMSelected = false;//????
                    OptionIMCSelected = false;//????
                    OptionMTSelected = false;//????
                    QuestionList = tempList;
                    Index = 0;
                    LastIndex = 0;

                    MessageBox.Show($"??????{tempList.Count}??????");
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



            //??????????????????
            //var questionDatabase = new QuestionDatabase($@"{Environment.CurrentDirectory}\MyQuestions.mdf");
            //AllQuestions = (from question in questionDatabase.Questions select question).ToList();

            //??json??????????????
            if (File.Exists("Questions.txt"))
                AllQuestions = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText("AllQuestions.json"));

            SubjectsQuestions = new List<Question>[4];
            for (int i = 0; i < 4; i++)
            {
                SubjectsQuestions[i] = AllQuestions.Where(q => q.Type == i).ToList();
            }

           
            //QuestionList = AllQuestions;

            InitQuestionList("????");
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
        [Column(IsPrimaryKey = true)] public int Id; //????????
        [Column] public int Type; //????????

        [Column] public string QuestionDescription; //????
        [Column] public string OptionA;
        [Column] public string OptionB;
        [Column] public string OptionC;
        [Column] public string OptionD;
        [Column] public string Answer;

        [Column] public int AnswerTimes = 0; //????????????????
        [Column] public int WrongAnswerTimes = 0; //??????????????
        public string LastAnswer = "";
        [Column(CanBeNull = true)] public string Notes = ""; //???? ?????? ??????????

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

        //??????????????????
        public Question()
        {
        }
    }

    public enum Subject
    {
        ChineseModernHistory = 0, //??????
        BasicTenetsOfMarxism = 1, //????
        IdeologicalAndMoralCultivation = 2, //????
        MaoZedongThought = 3 //????
    }

  
}