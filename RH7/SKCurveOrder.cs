using System;
using System.Diagnostics;
using System.Collections.Generic; //ジェネリック型
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input.Custom;
using Rhino.Display;
using Rhino.Input;

namespace ProfileCut7
{
    class SKCurveOrder
    {
        static public void Nearest2CrvPoint(Curve Crv1, Curve Crv2, out bool r1, out bool r2)
        {
            r1 = false;
            r2 = false;
            // 2つのカーブの近い端点を検出する
            // return [true: if Crv1 start is near , true: if Crv2 start is near] 
            var p1s = Crv1.PointAtStart;
            var p1e = Crv1.PointAtEnd;
            var p2s = Crv2.PointAtStart;
            var p2e = Crv2.PointAtEnd;
            //PrintPoint2("crv1=", p1s, p1e)
            //PrintPoint2("crv2=", p2s, p2e)
            List<double> distances = new List<double>();
            distances.Add((p1s - p2s).Length);
            distances.Add((p1e - p2s).Length);
            distances.Add((p1s - p2e).Length);
            distances.Add((p1e - p2e).Length);

            var min_val = distances[0];
            var min_idx = 0;

            for (int i = 1; i < distances.Count; i++)
            {
                if (distances[i] < min_val)
                {
                    min_idx = i;
                    min_val = distances[i];
                }
            }
            switch (min_idx)
            {
                case 0:
                    r1 = true;
                    r2 = true;
                    break;
                case 1:
                    r1 = false;
                    r2 = true;
                    break;
                case 2:
                    r1 = true;
                    r2 = false;
                    break;
                case 3:
                    r1 = false;
                    r2 = false;
                    break;
            }
        }

        static public bool DirByTwoCrvsOrder(Curve crv1, Curve crv2, bool ChangeCrv2Only)
        {
            /*
	        crv0 と crv1の端点(s,e)で最も近いペアを検出する
	        0-s と 1-s ならば、0を反転
	        0-e と 1-e ならば、1を反転
	        0-s と 1-e ならば、0と1を反転
            */
            bool b1, b2;
            Nearest2CrvPoint(crv1, crv2, out b1, out b2);
            //debug(str(ret[0]) + "," + str(ret[1]) )
            if (b1)
            {
                if (ChangeCrv2Only)
                {
                    // 閉曲線の場合は反転しても同じなのでTrue
                    if (crv1.IsClosed)
                        return true;
                    else
                        return false;
                }
                crv1.Reverse();
            }

            if (!b2)
            {
                crv2.Reverse();
            }
            return true;
        }

        static public bool CurveDirByOrder(List<Curve> Curves)
        {
            for (int i = 0; i < Curves.Count; i++)
            {
                if (i > 0)
                {
                    if (!DirByTwoCrvsOrder(Curves[i - 1], Curves[i], i > 1))
                    {
                        SKUtil.DebugPrint("DirByTwoCrvsOrder 計算異常");
                        return false;
                    }
                }
            }
            return true;
        }

        static public bool CurveObjectDirByOrder(List<RhinoObject> Objs)
        {
            List<Curve> curves = new List<Curve>();
            foreach (var obj in Objs)
            {
                Curve crv = obj.Geometry as Curve;
                if (crv == null)
                {
                    //
                }
                curves.Add(crv);
            }
            bool ret = CurveDirByOrder(curves);
            foreach (var obj in Objs)
            {
                obj.CommitChanges();
            }
            return ret;
        }
    }
}
