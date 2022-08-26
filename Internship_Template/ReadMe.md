# インターンシップテンプレ　README

インターンシップ用テンプレートプログラムのREADME

インターンシップ中の場当たり的指導による非効率な作業、成果物の具体例の提示がないゆえのインターン生の開発イメージの不足、指導者の技術習熟度の差による負担の大きさ等課題があったため本テンプレートを作成した。<br>インターン生および指導者が成果物作成までのおおよその流れをつかむこと、指導者がどのレベルであってもこれに目を通してもらい技術的仕様を理解し、負担の軽減と属人化の脱却を目的としている。



# 概要

テンプレートとして以下の要件を実装している。

- BootStrapを用いた最低限の画面デザイン(具体的なアプリケーションをイメージし、作成)
- ログイン認証
- CRUD処理



## 技術仕様

 ### 依存するもの、言語、フレームワーク

- .NET Framework4.8
- ASP.NET MVC 5
- Entity Framework
- Oracle Managed Driver
- BootStrap4.1
- jQuery 3.4.1

### フォルダ階層

```
├─App_Start //各種.NET MVC用のコンフィグ
├─Common //ログインのフィルター機能と開発補助用の機能が入っている
├─Content //cssファイル
├─Controllers //各画面のコントローラ
├─fonts
├─image //プロフィール初回設定時のNoImageアイコンだけ入っている
├─Models
│  ├─Entity //ビューモデルのパーツとなる各モデルはここ
│  └─VM //各画面に渡すビューモデル
├─Scripts //Javascriptファイル
└─Views //各画面のビュー
```



## 管理者モード

ログインを省略し、管理者として捜査できるよう管理者モードを用意してます。

利用する場合`web.config`内の設定を有効化してください。

```xml
 <appSettings>
    <!--開発時にログインをスキップできるよう管理者モードを追加-->
    <add key="DebugMode" value="Admin" />
  </appSettings>
```



## ログイン情報

ログイン時にセッション情報を作成し、各Controllerの呼び出し時にUserプロパティに設定してあります。また、ビューモデル側にもログイン情報格納用のLoginedUserプロパティを用意してあります。ActionResult毎にLoginedUserプロパティにUserプロパティの値を設定することでログイン情報の管理をしています。

```c#
 public ActionResult Index()
        {
            TempData.Remove("model");
            ユーザー画面 model = new ユーザー画面();
            model.Users = _db.T_USER.ToList() ?? new List<T_USER>();
            model.LoginedUser = User; //コントローラのセッション情報をビューモデルに設定
            TempData["model"] = model;

            return View(model);

        }
```



## データの扱いについて

今回はDBファーストでDbContextクラスを作成しているため、DbContextに対し各種メソッドを実行することでデータの登録更新削除が行えます。そのためSQLを記述する必要はありません。データのアクセスについてはLINQを用いて行います。

```c#
                // エンティティを追加＆データソースに反映
                _db.T_USER.Add(user.TargetUser); //_dbがDbContextクラスのオブジェクト。Addメソッドで引数のデータを追加
                _db.SaveChanges(); //SaveChangesメソッドで変更を反映

```

