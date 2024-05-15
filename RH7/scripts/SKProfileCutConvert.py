# -*- coding: utf-8 -*-
# 曲線を直線に変換する ProfileCut用
# Parameters
# 単純化 = はい (_S=_Yes)
# 元のオブジェクトを削除 (_D=_Yes) --> GUIDが変わらない

import math, os, copy
import rhinoscriptsyntax as rs

__ExecFromPlugIn=False

def ObjectByName(name):
	objs= rs.AllObjects()
	for obj in objs:
		obj_name=rs.ObjectName(obj)
		if obj_name==name:
			return obj
	return None


#デバッグ用の表示
def debug(msg):
	print(msg)
	
def input_id(msg,filter=rs.filter.curve,is_plugin=True):
	"""
	IDを入力する。プラグインから渡されたときはコンソールからGUIDの文字列を取得してチェックをして返す。
	オブジェクトがない場合はNoneを返す。
	テスト実行されたときは、マウスでピックしてGUIDを取得する
	"""
	if (is_plugin):
		debug("input_msg : "+ msg)
		obj_id=rs.GetString(msg)
		debug("obj_id : "+ obj_id)
		if obj_id==NO_OBJECT:
			debug("指定なし.")
			return None
		if (obj_id!=None) and (rs.IsObject(obj_id)):
			debug("文書内に発見. ")
			return obj_id
		else:
			debug("文書内にみつからない.")
			return None
	else:
		return rs.GetObject(msg,filter)

def ExecConvert(params):
	prefix = params[0]
	prec = params[1]
	id_list = []
	id_list.extend( rs.ObjectsByName(prefix + "_R_PATH") )
	id_list.extend( rs.ObjectsByName(prefix + "_Z_PATH") )
	id_list.extend( rs.ObjectsByName(prefix + "_IN") )
	id_list.extend( rs.ObjectsByName(prefix + "_OUT") )
	#Lineで変換 (コマンドを１回だけにして高速化 ＆　続く指定のパラメータがはいるように。)
	rs.Command("SelNone")
	rs.CurrentLayer("TOOLPATH")
	num= rs.SelectObjects(id_list)
	if num==0:
		debug("変換するオブジェクトがありません")
		return False
	s= "Convert _Output=_Line _S=_Yes _D=_Yes A 5 T {0} M 0.05 X 0 _Enter".format(prec)
	rs.Command(s)
	
	"""
	rs.Command("SelNone")
	for id in id_list:
		rs.CurrentLayer(rs.ObjectLayer(id))
		s= "Convert SelID {0} _Enter _Output=_Line _S=_Yes _D=_Yes A 5 T {1} M 0.05 X 0 _Enter".format(id, prec)
		rs.Command(s)
	"""
	rs.Command("SelNone")
	return True

def input_params():
	rs.Prompt()
	prefix = rs.GetString("Prefix")
	prec = rs.GetReal("Prec")
	if (prec==None):
		debug("prec:入力がキャンセルされました。")
	else:
		return [prefix,prec]
	return None

def Exec():
	__ExecFromPlugIn=True
	if __ExecFromPlugIn==True:
		debug("プラグイン実行モード")
		rs.EnableRedraw(False);
	else:
		debug("対話モード")
	save_view=rs.CurrentView()
	save_layer=rs.CurrentLayer()
	rs.CurrentView("Top")

	ret=input_params()
	if ret:
		debug("入力処理 正常終了")
		if ExecConvert(ret):
			debug("正常終了")
		else:
			debug("異常終了")
	else:
		debug("入力処理がキャンセルされました。")
	rs.CurrentView(save_view)
	rs.CurrentLayer(save_layer)
	rs.EnableRedraw(True);


if( __name__ == '__main__' ):
	Exec()
