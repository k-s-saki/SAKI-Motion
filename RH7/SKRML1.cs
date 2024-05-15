using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Rhino;
using Rhino.DocObjects;
using Rhino.Commands;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.Geometry;


namespace ProfileCut7
{
    class SKRML1
    {

    }

    class SKRML1Modal: MachineCodeConverter
    {

        private string _LastFeedStr;
        private string _LastPosStr;


        public override void ClearLast(bool Feed = true)
        {
            _LastFeedStr = "";
            _LastPosStr = "";
        }

        public override bool IsSupportArc()
        {
            return false;
        }

        private string GetPosStr(Point3d pt)
        {
            return string.Format("X{0:F0}Y{1:F0}Z{2:F0}", pt.X * 100, pt.Y * 100, pt.Z * 100);     
        }

        private string GetFeedStr(double feed)
        {
            if (feed == 0)
            {
                feed = 1200;
            }
            return string.Format("{0:F2}", feed / 60);
        }

        public override bool OutputCodeArc(System.IO.StreamWriter sw, CodeType codetype, Point3d ptEnd, Vector3d ptCenterOffset, double Feed)
        {
            throw new Exception("OutputCodeArc is not supported in " + this.GetType().Name);
        }

        public override bool OutputCodeLinearIfNeeded(System.IO.StreamWriter aSw, CodeType codetype, Point3d aPos, double aFeed)
        {
            if (codetype == CodeType.G00)
            {
                // Pos が同じだったら何も出力しない
                if (GetPosStr(aPos) != _LastPosStr)
                {
                    if (GetFeedStr(aFeed) != _LastFeedStr)
                    {
                        //VZ = Z方向の速度を設定
                        aSw.WriteLine(string.Format("!VZ" + GetFeedStr(aFeed) + ";"));
                        //VS = XY方向の速度を設定
                        aSw.WriteLine(string.Format("!VS" + GetFeedStr(aFeed) + ";"));
                        _LastFeedStr = GetFeedStr(aFeed);
                    }
                    aSw.WriteLine("!ZE" + GetPosStr(aPos) + ";");
                    _LastPosStr = GetPosStr(aPos);
                }
            }
            else if (codetype == CodeType.G01)
            {
                // Posが同じかどうかにかかわらず、速度だけで判断する
                if (GetFeedStr(aFeed) != _LastFeedStr)
                {
                    //VZ = Z方向の速度を設定
                    aSw.WriteLine(string.Format("!VZ" + GetFeedStr(aFeed) + ";"));
                    //VS = XY方向の速度を設定
                    aSw.WriteLine(string.Format("!VS" + GetFeedStr(aFeed) + ";"));
                    _LastFeedStr = GetFeedStr(aFeed);
                }

                // Posのみで判断する
                if (GetPosStr(aPos) != _LastPosStr)
                {
                    aSw.WriteLine("!ZE" + GetPosStr(aPos) + ";");
                    _LastPosStr = GetPosStr(aPos);
                }
            }

            return true;
        }

    }
}
