# SAKI-Motion
Rhinoceros Plugin Motion Creator

### SAKI Motion とは
SAKI Motion Pathは Rhinoceros 7のプラグインです。

3D-CAD ( Rhinoceros )上に自由にロボットやCNCなどの加工用の奇跡を描くことができます。

![1](https://github.com/k-s-saki/SAKI-Motion/assets/30764977/bd3bd2a4-3cf2-4743-b480-7d498a01a51c)

SAKI MotionのUIのパネルはタブになっています。他のRhinocerosのパネルと同じように、CAD操作の邪魔にならないようにスタックすることができます。

![2](https://github.com/k-s-saki/SAKI-Motion/assets/30764977/0a09065b-ae05-45df-8eb3-96c2470c00e0)

必要に応じて、工具や加工の選択、数値の入力画面（ダイアログ）が表示されます。

### Rhinoceros とは

いろんなデータファイル形式があつかえる、マルチな3D-CADソフトです。

工業用のNURBSサーフェースと、CG業界のSUB-Dの両方の3Dデータが使えます。

![image](https://github.com/k-s-saki/SAKI-Motion/assets/30764977/d4dea538-9e80-41b1-8ed0-48a5ffb72988)

建築物から、超精密な金型まで、デザイナーとエンジニアの間をとりもってくれる、比較的安価な3D-CADです。


### 入出力機能について

様々な機械にあわせたGコード、ロボット言語に対応したコードを出力することができます。
レアなところでは、ローランドDGのRML1というコードにも対応しています。
（EGXという機械で眼鏡のレンズパターンを削り出したり、マーキングできるようにしています）

測定した点などをCSVファイル等で入力するとプロットする機能を付ける予定です。
今後（SAKI-CNC (CODESYS上のソフトウエアCNC)との運用がシームレスに）

### 開発について

Visual Studio 2019 (C#) + Rhinoceros 7で開発しています。

開発画面
![image](https://github.com/k-s-saki/SAKI-Motion/assets/30764977/9837a59d-ffd7-47c2-8c4c-92aae6916e65)

今後（新しいVisual Studio + Rh8へのコンバートを予定しています、Rhinoceros 7のサポートも継続します。）



