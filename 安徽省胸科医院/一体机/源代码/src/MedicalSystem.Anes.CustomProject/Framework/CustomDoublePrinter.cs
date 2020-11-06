﻿// ＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
// 文件名称(File Name)：      CustomDoublePrinter.cs
// 功能描述(Description)：    文书双面打印实现
// 数据表(Tables)：		      无
// 作者(Author)：             MDSD
// 日期(Create Date)：        2018/01/23 13:28
// R1:
//    修改作者:
//    修改日期:
//    修改理由:
//＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
using System;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MedicalSystem.Anes.Document.Documents;

namespace MedicalSystem.Anes.CustomProject
{
    public class CustomDoublePrinter : UIElementPrinter
    {
        private int printIndex = 0;                                                          // 打印序号
        private UIElementPrinter[] curDoublePrinters = new UIElementPrinter[2];              // 需要打印的文书
        
        /// <summary>
        /// 有参构造，初始化打印设置
        /// </summary>
        public CustomDoublePrinter(CustomBaseDoc baseDoc, PaperSize paperSize, bool pageFromHeight, float printHeight, string pageName)
            : base(baseDoc, paperSize, pageFromHeight, printHeight, pageName)
        {
        }

        /// <summary>
        /// 设置打印列表
        /// </summary>
        /// <param name="doublePrinters">所需打印的列表</param>
        public void LoadDoubleDoc(UIElementPrinter[] doublePrinters)
        {
            this.curDoublePrinters = doublePrinters;
        }

        /// <summary>
        /// 打印事件
        /// </summary>
        protected override void doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (printIndex == 2)
            {
                return;
            }

            _baseDoc = curDoublePrinters[printIndex].BaseDocument;
            if (_baseDoc.PagerSetting.AllowPage && _baseDoc.PagerSetting.PagerDesc.Count > 0)
            {
                _baseDoc.PageIndexChanged(_baseDoc.PagerSetting.PagerDesc[_pageIndex].PageIndex,
                                          _baseDoc.PagerSetting.PagerDesc[_pageIndex].IsMain,
                                          _baseDoc.DataSource);
            }

            _printMetafile = GetPageMetafile();

            // 调整边距
            Rectangle rect = new Rectangle(0, 0, _printMetafile.Width, _printMetafile.Height);

            double widthZoom = 1;
            double heightZoom = 1;
            double widthSize = (e.MarginBounds.Width);
            double heightSize = (e.MarginBounds.Height);
            // 宽度缩放
            if (widthSize < rect.Width)
            {
                widthZoom = widthSize / rect.Width;
            }

            // 纵轴缩小
            if (heightSize < rect.Height)
            {
                heightZoom = heightSize / rect.Height;
            }
            double zoom = (widthZoom < heightZoom) ? widthZoom : heightZoom;
            Rectangle zoomRect = new Rectangle(rect.X, rect.Y, (int)(rect.Width * zoom), (int)(rect.Height * zoom));
            MemoryStream mStream = new MemoryStream();
            Graphics tempGraphics = _baseDoc.CreateGraphics();
            IntPtr ipHdctemp = tempGraphics.GetHdc();
            Metafile mf = new Metafile(mStream, ipHdctemp);
            tempGraphics.ReleaseHdc(ipHdctemp);
            tempGraphics.Dispose();

            Graphics gMf = Graphics.FromImage(mf);
            gMf.DrawImage(_printMetafile, zoomRect);
            gMf.Save();
            gMf.Dispose();
            _printMetafile = mf;
            metafileDelegate = new Graphics.EnumerateMetafileProc(MetafileCallback);

            // 开始正式打印
            this.PrintPage(_printMetafile, e, zoomRect.Width, zoomRect.Height);
            metafileDelegate = null;

            _pageIndex++;
            if (_pageFromHeight)
            {
                Rectangle r = GetPrintRect();
                if ((_pageIndex) * _pagePrintHeight < r.Height)
                {
                    e.HasMorePages = true;
                }
            }
            else
            {
                if (printIndex == 0)
                {
                    e.HasMorePages = true;
                }

                if (_baseDoc.PagerSetting.AllowPage && _pageIndex < _baseDoc.PagerSetting.TotalPageCount)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    _pageIndex = 0;
                    printIndex++;
                }
            }
        }

        /// <summary>
        /// 绘制打印图片
        /// </summary>
        protected override void PrintPage(System.Drawing.Imaging.Metafile file, PrintPageEventArgs e, int width, int height)
        {
            // 纸张大小
            float paperWidth = CustomBaseDoc.PaperWidth / 2.54f * 100.0f;
            float paperHeight = CustomBaseDoc.PaperHeight / 2.54f * 100.0f;
            float clipWidth = paperWidth < e.Graphics.VisibleClipBounds.Width ? paperWidth : e.Graphics.VisibleClipBounds.Width;
            float clipHeight = paperHeight < e.Graphics.VisibleClipBounds.Height ? paperHeight : e.Graphics.VisibleClipBounds.Height;

            // 打印机边距
            float marginOX = paperWidth - clipWidth + e.Graphics.VisibleClipBounds.Left;
            float marginOY = paperHeight - clipHeight + e.Graphics.VisibleClipBounds.Top;

            // 左侧留2.5 cm 装订
            //float left = 2.5f / 2.54f * 100.0f;
            float left = 0.4f / 2.54f * 100.0f;
            // 左右平均分配空间
            float marginX = (clipWidth + marginOX - left - (float)width) / 2;

            // 上下平均分配空间，但上面最多空1 cm
            float top = 1f / 2.54f * 100.0f;
            float marginY = (clipHeight + marginOY - (float)height) / 2;
            if (marginY > top)
            {
                marginY = top;
            }

            marginX = left + marginX - marginOX / 2;
            marginY = marginY - marginOY / 2;
            e.Graphics.DrawImage(file, new RectangleF(marginX, marginY, (float)width, (float)height));
        }
    }
}
