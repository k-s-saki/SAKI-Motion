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


namespace SakiMotion
{

    public abstract class MachineCodeConverter
    {
        public enum CodeType { G00=0, G01=1, G02=2, G03=3 , GNone=1000};

        public abstract bool IsSupportArc();
        public abstract void ClearLast(bool Feed = true);
        //public abstract bool IsNeedOutput(int MoveCode, Point3d pt, double Feed);
        //public abstract string ModalCodeStr(int MoveCode, Point3d pt, double Feed);
        public abstract bool OutputCodeLinearIfNeeded(System.IO.StreamWriter sw, CodeType codetype, Point3d ptEnd, double Feed);
        public abstract bool OutputCodeArc(System.IO.StreamWriter sw, CodeType codetype, Point3d ptEnd, Vector3d ptCenterOffset, double Feed);
    }

    class SKGCode
    {

    }

    class SKGCodeModal:MachineCodeConverter
    {
        private Point3d? _LastPt;
        private double _LastFeed;
        //private string _LastX, _LastY, _LastZ;
        private CodeType _LastCodeType = CodeType.GNone;

        static double output_tor = 0.001;

        public SKGCodeModal()
        {
        }

        public override bool IsSupportArc()
        {
            return true;
        }

        public override void ClearLast(bool Feed = true)
        {
            _LastCodeType = CodeType.GNone;
            _LastPt = null;
            //_LastX = "";
            //_LastY = "";
            //_LastZ = "";
            if ( Feed )
                _LastFeed = 0.0;

        }

        /* そのポイントの出力が必要かどうか？ */
        private bool IsNeedOutput(CodeType codetype, Point3d pt, double Feed)
        {
            if (_LastPt == null)
            {
                return true;
            }
            else
            {
                Point3d lp = (Point3d)_LastPt;
                Vector3d v = pt - lp;
                if (v.IsTiny(0.003))
                {
                    return false;
                    /*
                    if(MoveCode !=0)
                    {
                        return (Feed != _LastFeed || MoveCode != _LastMoveCode);
                    }
                    else
                    {
                        return (MoveCode != _LastMoveCode);
                    }
                    */
                }
                else
                {
                    return true;
                }
            }
        }


        /* ポイントの座標出力 */
        public string ModalCodeStr(CodeType codetype, Point3d pt, double Feed)
        {
            bool bMoveCode = false;
            bool bX = false;
            bool bY = false;
            bool bZ = false;
            bool bF = false;
            string s = "";

            bMoveCode = (codetype != _LastCodeType);
            bF = (Feed != _LastFeed) && (codetype != CodeType.GNone);

            if (_LastPt!= null)
            {
                Point3d lp = (Point3d)_LastPt;
                bX = (Math.Abs(pt.X - lp.X) >= output_tor);
                bY = (Math.Abs(pt.Y - lp.Y) >= output_tor);
                bZ = (Math.Abs(pt.Z - lp.Z) >= output_tor);
            }
            else
            {
                bX = true;
                bY = true;
                bZ = true;
            }
            if (bMoveCode)
                s = s + string.Format("G{0:00}", (int) codetype);
            if (bX)
                s = s + string.Format(" X{0:N3}",pt.X);
            if (bY)
                s = s + string.Format(" Y{0:N3}", pt.Y);
            if (bZ)
                s = s + string.Format(" Z{0:N3}", pt.Z);
            if (bF)
            {
                s = s + string.Format(" F{0:N0}", Feed);
                _LastFeed = Feed;
            }
            _LastCodeType = codetype;
            _LastPt = new Point3d(pt);
            return s;
        }

        public override bool OutputCodeLinearIfNeeded(System.IO.StreamWriter sw, CodeType codetype, Point3d ptEnd, double Feed)
        {
            if(codetype==CodeType.G00 || codetype == CodeType.G01)
            {
                if (IsNeedOutput(codetype, ptEnd, Feed))
                {
                    sw.WriteLine(ModalCodeStr(codetype, ptEnd, Feed));
                    return true;
                }
                else
                    return false;
            }
            else
            {
                //コード異常
                return false;
            }
        }

        private string ArcCodeStr(CodeType codetype, Point3d pt, Vector3d center_offset, double Feed)
        {
            //Feed以外のコードは毎回常にクリア
            ClearLast(false);

            string s = string.Format("G{0:00} X{1:N3} Y{2:N3} Z{3:N3} I{4:N3] J{5:N3}",
                (int)codetype, pt.X, pt.Y, pt.Z,
                center_offset.X, center_offset.Y);

            var bF = (Feed != _LastFeed) ;
            if (bF)
            {
                s = s + string.Format(" F{0:N0}", Feed);
                _LastFeed = Feed;
            }
            return s;
        }


        public override bool OutputCodeArc(System.IO.StreamWriter sw, CodeType codetype, Point3d ptEnd, Vector3d ptCenterOffset, double Feed)
        {
            if (codetype == CodeType.G02 || codetype == CodeType.G03)
            {
                if (IsNeedOutput(codetype, ptEnd, Feed))
                {
                    sw.WriteLine(ArcCodeStr(codetype, ptEnd, ptCenterOffset, Feed));
                    return true;
                }
                else
                    return false;
            }
            else
            {
                //コード異常
                return false;
            }
        }


    }
}
