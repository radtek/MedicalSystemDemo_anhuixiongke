﻿using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
//＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
//文件名称(File Name)：      Control
//功能描述(Description)：    
//数据表(Tables)：		    
//作者(Author)：             Jones.Zhao
//日期(Create Date)：        2015-11-13 13:29
//R1:
//    修改作者:              吴文蛟
//    修改日期：             2016-06-08
//    修改理由:              加入统一条件属性
//＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// The Control namespace.
/// </summary>
namespace MedicalSystem.Anes.Client.CustomProject
{
    /// <summary>
    /// Class DateYear.
    /// </summary>
    public partial class DateYear : UserControl
    {
        #region 属性
        /// <summary>
        /// 当前整年
        /// </summary>
        /// <value>The temporary int year.</value>
        private int TempIntYear { get; set; }

        /// <summary>
        /// 当前年份
        /// </summary>
        /// <value>The current year.</value>
        public int CurrentYear { get; set; }

        /// <summary>
        /// 返回开始日期
        /// </summary>
        /// <value>The start date.</value>
        public DateTime StartDate
        {
            get { return GetDate(true); }
        }

        /// <summary>
        /// 返回结束日期
        /// </summary>
        /// <value>The end date.</value>
        public DateTime EndDate
        {
            get { return GetDate(false); }
        }

        private int YearCount = 6;

        #endregion

        #region 构造函数
        /// <summary>
        /// Initializes a new instance of the <see cref="DateYear"/> class.
        /// </summary>
        public DateYear()
        {
            InitializeComponent();
            this.Width = 120;
            this.Height = 21;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 取当前年整年
        /// </summary>
        /// <param name="tyear">The tyear.</param>
        /// <returns>System.Int32.</returns>
        private int GetNowYear(int tyear)
        {
            int year = tyear / YearCount;
            return year * YearCount;
        }

        /// <summary>
        /// 初始化年份向上加10分
        /// </summary>
        /// <param name="startYear">The start year.</param>
        private void InitControlYear(int startYear)
        {
            foreach (System.Windows.Forms.Control ctrl in panel.Controls)
            {
                if (ctrl.Name.Contains("labelY"))
                {
                    int index = int.Parse(ctrl.Name.Replace("labelY", ""));
                    ctrl.Text = string.Format("{0}", startYear + index - 1);

                    if (ctrl.Text.Contains(startYear.ToString()))
                        SetCurrtntYear(ctrl);
                }


            }
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="isStartTime">if set to <c>true</c> [is start time].</param>
        /// <returns>DateTime.</returns>
        private DateTime GetDate(bool isStartTime)
        {
            DateTime result = DateTime.MinValue;

            if (isStartTime)
                result = DateTime.Parse(CurrentYear.ToString() + "-1-1");
            else
                result = DateTime.Parse(string.Format("{0}", CurrentYear + 1) + "-1-1");
            return result;
        }

        /// <summary>
        /// 初始化年分控件事件
        /// </summary>
        private void InitControlClick()
        {
            foreach (System.Windows.Forms.Control ctrl in panel.Controls)
            {
                if (ctrl.Name.Contains("labelY"))
                {
                    ctrl.Click += new EventHandler(ctrl_Click);
                    ctrl.MouseEnter += ctrl_MouseEnter;
                    ctrl.MouseLeave += ctrl_MouseLeave;
                }

                if (ctrl.Name == "labelY" + CurrentYear.ToString().Trim())
                    SetCurrtntYear(ctrl);
            }
        }
        void ctrl_MouseLeave(object sender, EventArgs e)
        {
            ((LabelControl)sender).BorderStyle = BorderStyles.Default;
        }

        void ctrl_MouseEnter(object sender, EventArgs e)
        {
            ((LabelControl)sender).BorderStyle = BorderStyles.Office2003;
        }

        /// <summary>
        /// 根据当前年份设置当前值
        /// </summary>
        private void SetCurrentValue()
        {
            popupContainerEdit.Text = CurrentYear.ToString() + "年";
        }
        #endregion

        #region 事件
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DateYear_Load(object sender, EventArgs e)
        {
            if (CurrentYear <= 0)
            {
                CurrentYear = DateTime.Now.Year;
            }
            TempIntYear = GetNowYear(CurrentYear);
            popupContainerEdit.Text = CurrentYear.ToString() + "年";
            InitControlYear(TempIntYear);
            InitControlClick();
        }

        /// <summary>
        /// 减年事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void labelUp_Click(object sender, EventArgs e)
        {
            TempIntYear = TempIntYear - YearCount;
            InitControlYear(TempIntYear);
        }

        /// <summary>
        /// 加年事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void labeDown_Click(object sender, EventArgs e)
        {
            TempIntYear = TempIntYear + YearCount;
            InitControlYear(TempIntYear);
        }

        /// <summary>
        /// 年份单击事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void ctrl_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Control ctrl = sender as System.Windows.Forms.Control;
            CurrentYear = int.Parse(ctrl.Text);
            SetCurrentValue();
            SetCurrtntYear(ctrl);
            popupContainerEdit.ClosePopup();
        }




        System.Windows.Forms.Control currentCtr = null;
        private void SetCurrtntYear(System.Windows.Forms.Control ctr)
        {
            if (currentCtr != null)
            {
                currentCtr.ForeColor = Color.Black;
                currentCtr.BackColor = Color.White;
            }
            ctr.ForeColor = Color.White;
            ctr.BackColor = Color.FromArgb(100, 170, 250);
            currentCtr = ctr;
        }

        #endregion

        private void labelCurrentYear_Click(object sender, EventArgs e)
        {
            CurrentYear = DateTime.Now.Year;
            TempIntYear = GetNowYear(CurrentYear);
            popupContainerEdit.Text = CurrentYear.ToString() + "年";
            InitControlYear(TempIntYear);
        }
    }
}
