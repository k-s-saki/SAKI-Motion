# -*- coding: utf-8 -*-
# 計算オブジェクトを削除する ProfileCut用

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
	

def ExecDelete():
	rs.LayerVisible("TOOLPATH",True,True)
	rs.LayerVisible("TOOLPATH_INFO",True,True)
	rs.LayerVisible("TOOLPATH_CALC",True,True)
	rs.Command("_Show")
	objs = []
	objs.extend( rs.ObjectsByLayer("TOOLPATH") )
	objs.extend( rs.ObjectsByLayer("TOOLPATH_INFO") )
	objs.extend( rs.ObjectsByLayer("TOOLPATH_CALC") )
	rs.DeleteObjects(objs)
	return True

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

	if ExecDelete():
		debug("正常終了")
	else:
		debug("異常終了")
	rs.CurrentView(save_view)
	rs.CurrentLayer(save_layer)
	rs.EnableRedraw(True);


if( __name__ == '__main__' ):
	Exec()
