object LoadStringResourceMainForm: TLoadStringResourceMainForm
  Left = 0
  Top = 0
  Caption = 'LoadStringResourceMainForm'
  ClientHeight = 301
  ClientWidth = 568
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 568
    Height = 41
    Align = alTop
    TabOrder = 0
    object DemoButton: TButton
      Left = 8
      Top = 10
      Width = 75
      Height = 25
      Caption = '&Demo'
      TabOrder = 0
      OnClick = DemoButtonClick
    end
  end
  object LogMemo: TMemo
    Left = 0
    Top = 41
    Width = 568
    Height = 260
    Align = alClient
    TabOrder = 1
  end
end
