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
        //全部题目按钮
        public bool OptionAllQuestionSelected
        {
            get => _OptionAllQuestionSelected;
            set
            {
                _OptionAllQuestionSelected = value;
                RaisePropertyChanged();
            }
        }

        //近代史题目按钮
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

        //毛概题目按钮
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

        //思修题目按钮
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

        //马原题目按钮
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

        //已做过题目
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

        //正确率
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

        //是否是多选题
        public bool IsMultiSelectMode { get; set; }
        //是否考试模式
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

        //是否做过的题直接显示答案
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

        //是否自动跳转到下一题
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

        //是否随机题目
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
        /// 所有题目的列表
        /// </summary>
        public List<Question> QuestionList { get; set; }
        public List<Question> AllQuestions { get; set; }
        public List<Question>[] SubjectsQuestions { get; set; }
        /// <summary>
        /// 记录右侧题目选择按钮的信息
        /// </summary>
        public List<ButtonInfo> ButtonInfoList { get; set; }
        /// <summary>
        /// 记录回答过的问题的状态 0回答错误 1回答正确 -1已选择答案但未提交
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
        /// 右侧题目选择按钮的颜色值
        /// </summary>
        public SolidColorBrush[] ColorBrushes { get; set; } =
        {
            new SolidColorBrush(Colors.Gainsboro), //未作答颜色
            new SolidColorBrush(Colors.LightGreen),//答对颜色
            new SolidColorBrush(Colors.LightCoral) //答错颜色
        };

        private void OnIndexChanged()
        {
            IsMultiSelectMode = QuestionList[Index].Answer.Length > 1;

            QuestionText = (IsMultiSelectMode?"【多选题】":"") + (Index + 1) + "." + QuestionList[Index].QuestionDescription;
            OptionAText = "A." + QuestionList[Index].OptionA;
            OptionBText = "B." + QuestionList[Index].OptionB;
            OptionCText = "C." + QuestionList[Index].OptionC;
            OptionDText = "D." + QuestionList[Index].OptionD;
            
            //更新题目，做过的题目还原现场
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

            //检查更新右侧题目选择按钮
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

            //StatisticsDone = $"当前:{Index + 1}  已答:{AnswerRecord.Count}/{QuestionList.Count}";
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
                AnswerText = $"回答正确\n正确答案为{QuestionList[Index].Answer}";
                AnswerRecord[QuestionList[Index].Id] = (userAnswer, 1);
                
                if(ButtonInfoList == null || ButtonInfoList.Count == 0) { return;}
                ButtonInfoList[Index % MaxQuestionNumber].BackGroundSolidColorBrush =
                    ColorBrushes[1];
            }
            else
            {
                AnswerText = $"回答错误\n正确答案为{QuestionList[Index].Answer}";
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
                case "全部":
                    OptionAllQuestionSelected = true;
                    OptionCMHSelected = false;//近代史
                    OptionBTMSelected = false;//马原
                    OptionIMCSelected = false;//思修
                    OptionMTSelected = false;//毛概
                    InitQuestionList("全部");
                    break;
                case "毛概":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//近代史
                    OptionBTMSelected = false;//马原
                    OptionIMCSelected = false;//思修
                    OptionMTSelected = true;//毛概
                    InitQuestionList("毛概");
                    break;
                case "思修":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//近代史
                    OptionBTMSelected = false;//马原
                    OptionIMCSelected = true;//思修
                    OptionMTSelected = false;//毛概
                    InitQuestionList("思修");

                    break;
                case "近代史":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = true;//近代史
                    OptionBTMSelected = false;//马原
                    OptionIMCSelected = false;//思修
                    OptionMTSelected = false;//毛概
                    InitQuestionList("近代史");

                    break;
                case "马原":
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//近代史
                    OptionBTMSelected = true;//马原
                    OptionIMCSelected = false;//思修
                    OptionMTSelected = false;//毛概
                    InitQuestionList("马原");
                    break;
                default:
                    InitQuestionList(op);
                    break;
            }
        }

        private void UpdateAnswerInformation()
        {
            StatisticsDone = $"当前:{Index + 1}  总共:{QuestionList.Count}";
            CorrectRate = $"已答:{AnswerRecord.Count}/{AllQuestions.Count} 正确率:{(double)AnswerRecord.Count(a => a.Value.status==1) / AnswerRecord.Count * 100:F1}%";
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
                case "全部":
                    QuestionList = AllQuestions;
                    Index = 0;
                    LastIndex = 0;
                    break;
                case "毛概":
                    QuestionList = SubjectsQuestions[(int)Subject.MaoZedongThought];
                    Index = 0;
                    LastIndex = 0;

                    break;
                case "思修":
                    QuestionList = SubjectsQuestions[(int)Subject.IdeologicalAndMoralCultivation];
                    Index = 0;
                    LastIndex = 0;

                    break;
                case "近代史":
                    QuestionList = SubjectsQuestions[(int)Subject.ChineseModernHistory];
                    Index = 0;
                    LastIndex = 0;

                    break;
                case "马原":
                    QuestionList = SubjectsQuestions[(int)Subject.BasicTenetsOfMarxism];
                    Index = 0;
                    LastIndex = 0;

                    break;
                default:
                    if (op.Trim()=="")
                    {
                        MessageBox.Show("输入内容为空");
                        return;
                    }
                    var tempList = AllQuestions.Where(q => q.QuestionDescription.Contains(op)).ToList();
                    if (tempList.Count == 0)
                    {
                        MessageBox.Show("没有搜索结果");
                        return;
                    }
                    OptionAllQuestionSelected = false;
                    OptionCMHSelected = false;//近代史
                    OptionBTMSelected = false;//马原
                    OptionIMCSelected = false;//思修
                    OptionMTSelected = false;//毛概
                    QuestionList = tempList;
                    Index = 0;
                    LastIndex = 0;

                    MessageBox.Show($"搜索到{tempList.Count}个题目");
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



            //从数据库中读取数据
            //var questionDatabase = new QuestionDatabase($@"{Environment.CurrentDirectory}\MyQuestions.mdf");
            //AllQuestions = (from question in questionDatabase.Questions select question).ToList();

            //从json文件中读取数据
            if (File.Exists("Questions.txt"))
                AllQuestions = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText("AllQuestions.json"));

            SubjectsQuestions = new List<Question>[4];
            for (int i = 0; i < 4; i++)
            {
                SubjectsQuestions[i] = AllQuestions.Where(q => q.Type == i).ToList();
            }

           
            //QuestionList = AllQuestions;

            InitQuestionList("全部");
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
        [Column(IsPrimaryKey = true)] public int Id; //题目编号
        [Column] public int Type; //题目类型

        [Column] public string QuestionDescription; //题干
        [Column] public string OptionA;
        [Column] public string OptionB;
        [Column] public string OptionC;
        [Column] public string OptionD;
        [Column] public string Answer;

        [Column] public int AnswerTimes = 0; //回答该问题的次数
        [Column] public int WrongAnswerTimes = 0; //回答错误的次数
        public string LastAnswer = "";
        [Column(CanBeNull = true)] public string Notes = ""; //备注 知识点 解题思路等

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

        //必须有无参构造函数
        public Question()
        {
        }
    }

    public enum Subject
    {
        ChineseModernHistory = 0, //近代史
        BasicTenetsOfMarxism = 1, //马原
        IdeologicalAndMoralCultivation = 2, //思修
        MaoZedongThought = 3 //毛概
    }

  
}